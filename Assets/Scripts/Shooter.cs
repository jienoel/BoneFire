using System;
using UnityEngine;
using UnityEngine.AI;


public class Shooter : MonoBehaviour
{
    [Header("Input")]
    public float thresholdTime = 0.1f;
    // 防误触，每次攻击要按下一定时间
    public LayerMask groundLayer;

    [Header("Attack")]
    public float maxDistance = 10f;
    // 最大手指/鼠标拖动范围
    public float maxDegree = 45;
    public float force = 10f;
    public GameObject bulletPrefab;

    [Header("Hint")]
    public LineRenderer line;
    public float width;
    public float length;

    private bool isClicked = false;
    private float clickTime = 0f;
    private Vector3 clickPos = Vector3.zero;

    private NavMeshAgent agent;
    public Animator animator;
    public SpriteRenderer render;

    public int hp;
    public int hpMax = 100;
    public bool inSafeArea;

    public bool isDie
    {
        get
        {
            return hp <= 0;
        }
    }

    //出生点
    public Transform startPos;

    Vector3 lastPosition;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        startPos = GameObject.FindGameObjectWithTag("PlayerStartPos").transform;
    }

    private void Start()
    {
        hp = hpMax;
        lastPosition = transform.position;
    }

    private Vector3 GetGroundPoint()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, groundLayer))
        {
            return hit.point;
        }
        return Vector3.down;
    }

    private void OnMouseDown()
    {
        if (!isClicked)
        {
            animator.SetTrigger(AnimatorParam.TrigerAttackPre);
        }
        isClicked = true;
        clickTime = Time.time;
        clickPos = GetGroundPoint();
    }

    void LateUpdate()
    {
        if (lastPosition != transform.position)
        {
            FlipSprite(transform.position - lastPosition);
            lastPosition = transform.position;
        }
    }

    void FlipSprite(Vector3 forward)
    {
//        Vector3 v = Vector3.Project(forward, Vector3.right);
//        if (render.flipX != (v.x < 0))
//            render.flipX = v.x < 0;
        Game.FlipSprite(render, forward);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            isClicked = false;
        }
        if (isClicked)
        {
            StopMove();
          
            var currPoint = GetGroundPoint();
            if (clickPos != Vector3.down && currPoint != Vector3.down)
            {
//                transform.forward = clickPos - currPoint;
                FlipSprite(clickPos - currPoint);
            }
            var currDir = CalculateDirection(currPoint - clickPos);
            DisplayLine(true, currDir);
            if (Input.GetMouseButtonUp(0))
            {
                var releaseTime = Time.time;
                if (releaseTime - clickTime < thresholdTime)
                {
                }
                else
                {
                    if (clickPos != Vector3.down && currPoint != Vector3.down)
                    {
                        animator.SetTrigger(AnimatorParam.TriggerAttack);
                        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                        bullet.GetComponent<Rigidbody>().AddForce(currDir * force, ForceMode.Impulse);
                        DisplayLine(false, currDir);
                    }
                }
                isClicked = false;
                transform.rotation = Quaternion.identity;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Move();
            }
        }
        animator.SetFloat(AnimatorParam.FloatSpeed, agent.velocity.magnitude);

        Test();
    }

    void StopMove()
    {
        agent.isStopped = true;
        agent.ResetPath();
    }

    void Move()
    {
        var dest = GetGroundPoint();
        if (dest != Vector3.down)
        {
            agent.isStopped = false;
            agent.SetDestination(dest);
        }
    }

    private Vector3 CalculateDirection(Vector3 dir)
    {
        var direction = -dir.normalized;
        var magnitude = Mathf.Clamp(dir.magnitude, 0, maxDistance);
        var anotherVec = Mathf.Lerp(0, Mathf.Tan(Mathf.Deg2Rad * maxDegree), magnitude / maxDistance) * Vector3.up;
        return (direction + anotherVec).normalized;
    }

    private void DisplayLine(bool canDisplay, Vector3 dir)
    {
        if (canDisplay)
        {
            var v0 = dir.y * force;
            var x0 = transform.position.y;
            var g = Physics.gravity.y;
            var t = (-v0 - Mathf.Sqrt(v0 * v0 - 2 * g * x0)) / g;
            int count = (int)(t / Time.fixedDeltaTime);
            var positions = new Vector3[count];
            Vector3 startPos = transform.position;
            Vector3 vel = new Vector3(dir.x * force, v0, dir.z * force);
            for (int i = 0; i < count; i++)
            {
                startPos += vel * Time.fixedDeltaTime;
                vel += Physics.gravity * Time.fixedDeltaTime;
                positions[i] = startPos;
            }
            //line.material.mainTextureScale = new Vector2(count / 4f, 1);
            line.positionCount = count;
            line.SetPositions(positions);
            line.startWidth = width;
            line.endWidth = width;
        }
        else
        {
            line.positionCount = 0;
        }
    }

    void OnPlayerWasAttack(int damage)
    {
        if (damage <= 0)
            return;
        isClicked = false;
        animator.SetTrigger(AnimatorParam.TriggerHurt);
        hp -= damage;
        if (hp <= 0)
        {
            OnPlayerDie();
            return;
        }
    }

    void OnPlayerDie()
    {
        hp = 0;
        animator.SetBool(AnimatorParam.BoolDie, true);
        StopMove();
        GameSignals.InvokeAction(GameSignals.onPlayerDie);
    }

    public void RelivePlayer()
    {
        hp = hpMax;
        this.transform.position = startPos.position;
        animator.SetBool(AnimatorParam.BoolDie, false);
    }


    void Test()
    {
        if (relivePlayer)
        {
            TestRelivePlayer();
        }
        if (attackPlayer)
        {
            TestPlayerWasAttack();
        }
    }

    public bool attackPlayer;
    public int damage = 50;

    void TestPlayerWasAttack()
    {
        if (attackPlayer)
        {
            attackPlayer = false;
            OnPlayerWasAttack(damage);
        }
    }

    public bool relivePlayer;

    void TestRelivePlayer()
    {
        if (relivePlayer)
        {
            relivePlayer = false;
            RelivePlayer();
        }
    }
}
