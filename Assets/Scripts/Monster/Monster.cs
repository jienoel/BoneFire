using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum MonsterStatus
{
    //待机
    Wait,
    //巡逻
    Patrol,
    //吃火源
    EatFireFood,
    //追逐玩家
    ChasePlayer,
    Die
}

public class Monster : MonoBehaviour
{
    public Transform foodTarget;
    NavMeshAgent agent;
    public List<MonsterBody> monsterBodies = new List<MonsterBody>();
    public GameObject diamond;
    public MonsterStatus status;
    public float catchDistance;
    public float visualRange = 10;
    public float waitCD = 0;
    public float waitCDMax = 30;
    public float stayCDWhenChaseBlock = 10;
    public Vector3 patrolPos;
    //人物出现在怪物的视线范围
    public SpriteRenderer hasPlayerRender;

    public void ResetBody()
    {
        monsterBodies.Clear();
        monsterBodies.AddRange(GetComponentsInChildren<MonsterBody>());
        monsterBodies.Sort(
            delegate(MonsterBody p1, MonsterBody p2)
            {
                return p1.bodyID.CompareTo(p2.bodyID);
            }
        );
    }
    // Use this for initialization
    void Start()
    {
        ResetBody();
        agent = GetComponent<NavMeshAgent>();
    }
	
    // Update is called once per frame
    void Update()
    {
//        if (IsDie())
//        {
//            Debug.Log(string.Format("{0} died.", name));
//            Instantiate(diamond, transform.position, Quaternion.identity);
//            Destroy(gameObject);
//            return;
//        }
//        if (foodTarget != null)
//        {
//            agent.SetDestination(foodTarget.transform.position);
//            Debug.Log(Vector3.Distance(transform.position, foodTarget.transform.position));
//            if (Vector3.Distance(transform.position, foodTarget.transform.position) < catchDistance)
//            {
//                Debug.Log("Arrive");
//                foodTarget.GetComponent<IChaseable>().Arrived();
//                foodTarget = null;
//            }
//        }
        CheckStatusUpdate();
    }

    private bool IsDie()
    {
       
//        var c = monsterBodies[0].colorIndex;
        int red = 0;
        int yellow = 0;
        int blue = 0;
        foreach (var m in monsterBodies)
        {
            if (m.colorIndex == ColorTable.Blue)
            {
                blue = 1;
            }
            else if (m.colorIndex == ColorTable.Yellow)
            {
                yellow = 1;
            }
            else
            {
                red = 1;
            }
        }
        int value = red << 2 | yellow << 1 | blue;
//        Debug.Log(value + "  red:" + red + "  yellow:" + yellow + " Blue: " + blue);
        return value == 0 || value == 1 || value == 2 || value == 4;
    }

    public float animationLazy;

    //    float GetAnimatorNormalizedTime(MonsterBody body)
    //    {
    //        float time = 0;
    //        foreach (MonsterBody last in monsterBodies)
    //        {
    //            if (last.bodyID == body.bodyID - 1)
    //            {
    //                time = last.animator.GetCurrentAnimatorStateInfo(0).normalizedTime - animationLazy;
    //            }
    //        }
    //        return time;
    //    }

    public void HitByPlayer(MonsterBody body)
    {
        body.colorIndex = (body.colorIndex + 1) % ColorTable.Max;
        body.ChangeColor(body.colorIndex);
        if (status == MonsterStatus.Die)
        {
            return;
        }
        if (status == MonsterStatus.EatFireFood)
        {
            return;
        }
        if (status == MonsterStatus.Wait || status == MonsterStatus.Patrol)
        {
            status = MonsterStatus.ChasePlayer;
            return;
        }

    }

    public  void SetAnimator(AnimatorParamType paramType, string name, object value)
    {
        foreach (MonsterBody body in monsterBodies)
        {
            Game.SetAnimatorParam(paramType, body.animator, name, value);
        }
    }

    void OnStatusChange(MonsterStatus newStatus)
    {
        if (status == newStatus)
        {
            return;
        }
        MonsterStatus lastStatus = status;
        if (lastStatus == MonsterStatus.ChasePlayer)
        {
            SetAnimator(AnimatorParamType.Bool, AnimatorParam.BoolHasAtkTarget, false);
        }
        status = newStatus;
        if (status == MonsterStatus.Patrol)
        {
            EnterPatrolStatus();
        }
        else if (status == MonsterStatus.Wait)
        {
            
        }
        else if (status == MonsterStatus.ChasePlayer)
        {
            SetAnimator(AnimatorParamType.Bool, AnimatorParam.BoolHasAtkTarget, true);
        }
        else if (status == MonsterStatus.Die)
        {
            EnterDieStatus();
        }
        else if (status == MonsterStatus.EatFireFood)
        {
            
        }
    }

    void CheckStatusUpdate()
    {
        if (status != MonsterStatus.Die && IsDie())
        {
            OnStatusChange(MonsterStatus.Die);
        }
        if (status == MonsterStatus.EatFireFood)
        {
            OnEatFireStatus();
        }
        else if (status == MonsterStatus.Wait)
        {
            OnWaitStatus();
        }
        else if (status == MonsterStatus.Patrol)
        {
            OnPatrolStatus(); 
        }
        else if (status == MonsterStatus.ChasePlayer)
        {
            OnChasePlayerStatus();
        }
        SetAnimator(AnimatorParamType.Float, AnimatorParam.FloatSpeed, agent.velocity.magnitude);
    }

    void OnWaitStatus()
    {
        if (HasFoodTarget())
        {
            OnStatusChange(MonsterStatus.EatFireFood);
        }
        else if (HasPlayerToChase())
        {
            OnStatusChange(MonsterStatus.ChasePlayer);  
        }
        else
        {
            waitCD += Time.deltaTime;
            if (waitCD >= waitCDMax)
            {
                waitCD = 0;
                OnStatusChange(MonsterStatus.Patrol);
            }
        }
    }

    void OnChasePlayerStatus()
    {
        if (Game.Instance.player.isDie)
        {
            OnStatusChange(MonsterStatus.Wait);
            return;
        }
        if (HasFoodTarget())
        {
            OnStatusChange(MonsterStatus.EatFireFood);
            return;
        }
        if (IsPlayerInSafeArea())
        {
            OnStatusChange(MonsterStatus.Wait);
            waitCD = waitCDMax - stayCDWhenChaseBlock;
            return;
        }
        agent.SetDestination(Game.Instance.player.transform.position);
        ChaseTargetOrFood(Game.Instance.player.gameObject);
    }

    void EnterDieStatus()
    {
        OnStatusChange(MonsterStatus.Die);
        Debug.Log(string.Format("{0} died.", name));
        Instantiate(diamond, transform.position, Quaternion.identity).GetComponentInChildren<Diamond>().SetColor(this.monsterBodies[0].colorIndex);
        Destroy(gameObject);
        return;
    }

    void EnterPatrolStatus()
    {
        Debug.Log("Enter PatrolStatus");
        if (IsPlayerInSafeArea() && InPlayerAttackRange())
        {
            ////TODO @zhuchaojie  离开玩家攻击距离的位置移动,设置PatrolPos
        }
        var degree = Random.Range(0, 360);
        patrolPos = transform.position + new Vector3(Mathf.Sin(degree), 0, Mathf.Cos(degree)) * 10;
    }

    void OnPatrolStatus()
    {
        if (HasFoodTarget())
        {
            OnStatusChange(MonsterStatus.EatFireFood);
            return;
        }
        if (HasPlayerToChase())
        {
            OnStatusChange(MonsterStatus.ChasePlayer);
            return;
        }
        agent.SetDestination(patrolPos);
        if (agent.velocity.magnitude <= 0)
        {
            OnStatusChange(MonsterStatus.Wait);
            return;
        }
    
    }

    void OnEatFireStatus()
    {
        if (NeedExitFromEatStatus())
        {
            if (HasPlayerToChase())
            {
                OnStatusChange(MonsterStatus.ChasePlayer);
            }
            else
            {
                OnStatusChange(MonsterStatus.Wait);
            }
            return;
        }
        agent.SetDestination(foodTarget.transform.position);
        ChaseTargetOrFood(foodTarget.gameObject);
    }

    void ChaseTargetOrFood(GameObject chaseTarget)
    {
//        Debug.Log(Vector3.Distance(transform.position, chaseTarget.transform.position));
        if (Vector3.Distance(transform.position, chaseTarget.transform.position) < catchDistance)
        {
            Debug.Log("Arrive Chase Target");
            chaseTarget.GetComponent<IChaseable>().Arrived();
            chaseTarget = null;
        }
    }

    bool HasPlayerToChase()
    {
        return IsPlayerInVisualRange() && !IsPlayerInSafeArea() && !Game.Instance.player.isDie;
    }

    public bool InPlayerAttackRange()
    {
        return Vector3.Distance(Game.Instance.player.transform.position, this.transform.position) <= Game.Instance.player.maxDistance;
    }

    bool IsPlayerInSafeArea()
    {
        return Game.Instance.player.inSafeArea;
    }

    bool IsPlayerInVisualRange()
    {
        if (Game.Instance == null)
        {
            Debug.Log("1");
        }
        else if (Game.Instance.player == null)
        {
            Debug.Log("2");
        }
        return Vector3.Distance(this.transform.position, Game.Instance.player.transform.position) <= visualRange;
    }

    bool NeedExitFromEatStatus()
    {
        //已被其他怪物吃掉，或者没跑到就已经自己燃完了
        if (foodTarget == null || !foodTarget.gameObject.activeSelf)
        {
            return true;
        }
        return false;
    }

    bool HasFoodTarget()
    {
        return Game.Instance.GetNearestFiredTreeInRange(this.transform.position, visualRange, out foodTarget);
    }


    public void JumpDone(MonsterBody body)
    {
//        Debug.Log("OnJumpGround " + body.gameObject.name + "   " + body.bodyID);
        if (body.bodyID != EBodyID.Zero)
        {
            return;
        }
        if (jumpRender.enabled == false)
            jumpRender.enabled = true;
        jumpEffectAnim.Play("HuiChen", 0, 0);
    }

    public  SpriteRenderer jumpRender;
    public Animator jumpEffectAnim;

   


}
