using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectPool : MonoBehaviour
{
    public GameObject obstacleBarrelPrefab;
    public GameObject obstacleBarrierPrefab;
    public GameObject obstacleStoneWallPrefab;
    public int poolSize = 5;

    private List<GameObject> obstacleBarrelPool;
    private List<GameObject> obstacleBarrierPool;
    private List<GameObject> obstacleStoneWallPool;

    void Awake()
    {
        obstacleBarrelPool = new List<GameObject>();
        obstacleBarrierPool = new List<GameObject>();
        obstacleStoneWallPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            obstacleBarrelPool.Add(CreateNew(obstacleBarrelPrefab));
            obstacleBarrierPool.Add(CreateNew(obstacleBarrierPrefab));
            obstacleStoneWallPool.Add(CreateNew(obstacleStoneWallPrefab));
        }
    }

    GameObject CreateNew(GameObject prefab)
    {
        var obj = Instantiate(prefab);
        obj.SetActive(false);
        return obj;
    }

    public GameObject Acquire(int obstacleType)
    {
        List<GameObject> pool = GetPool(obstacleType);

        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject newObj = CreateNew(GetPrefab(obstacleType));
        newObj.SetActive(true);
        pool.Add(newObj);
        return newObj;
    }

    public void Release(GameObject obstacle, int obstacleType)
    {
        obstacle.SetActive(false);
    }

    List<GameObject> GetPool(int type)
    {
        switch (type)
        {
            case 0: return obstacleBarrelPool;
            case 1: return obstacleBarrierPool;
            case 2: return obstacleStoneWallPool;
        }
        return null;
    }

    GameObject GetPrefab(int type)
    {
        switch (type)
        {
            case 0: return obstacleBarrelPrefab;
            case 1: return obstacleBarrierPrefab;
            case 2: return obstacleStoneWallPrefab;
        }
        return null;
    }

}
