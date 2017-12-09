using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TestRunning : MonoBehaviour
{
    public LayerMask groundLayer;
    public GameObject treePrefab;
    public GameObject campfirePrefab;
    public GameObject MonsterPrefab;
    public SpawnType spawnType;
    public Transform TreeParent;
    public Transform MonsterParent;
    public Transform campfireParent;

    public bool importTree = false;
    public Transform newTreeParent;

    public enum SpawnType
    {
        Tree,
        Monster,
        Campfire,
        None
    }

    void OnEnable()
    {
        importTree = false;
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

    private void Update()
    {
        if (importTree)
        {
            importTree = false;
            ImportTree();
        }
        if (Input.GetMouseButtonDown(0))
        {
            var dest = GetGroundPoint();
            if (dest != Vector3.down)
            {
                switch (spawnType)
                {
                    case SpawnType.Tree:
                        SpawnTree(dest);
                        break;
                    case SpawnType.Campfire:
                        SpawnCampfire(dest);
                        break;
                    case SpawnType.Monster:
                        SpawnMonster(dest);
                        break;
                }
            }
        }
    }

    void ImportTree()
    {
        foreach (Transform tran in TreeParent.transform)
        {
            CreatGameObject(treePrefab, newTreeParent, tran.position).name = tran.name;
        }
    }

    void SpawnTree(Vector3 pos)
    {
        Debug.Log("Spawn Tree " + pos);
        CreatGameObject(treePrefab, TreeParent, pos);
    }

    void SpawnCampfire(Vector3 pos)
    {
        Debug.Log("Spawn CampFire " + pos);
        CreatGameObject(campfirePrefab, campfireParent, pos);
    }

    void SpawnMonster(Vector3 pos)
    {
        Debug.Log("SpawnMonster " + pos);
        CreatGameObject(MonsterPrefab, MonsterParent, pos);
    }

    GameObject CreatGameObject(GameObject prefab, Transform parent, Vector3 pos)
    {
        GameObject obj = GameObject.Instantiate(prefab, parent);
        obj.transform.position = pos;
        obj.name = prefab.name;
        return obj;
    }
}
