using System.Collections.Generic;
using UnityEngine;

public class MyPool
{
    private Stack<GameObject> poolStack = new Stack<GameObject>();
    private GameObject baseGO;
    private GameObject tmp;
    private ReturnToMyPool returnPool;

    public MyPool(GameObject baseGO)
    {
        this.baseGO = baseGO;
    }

    public GameObject Get()
    {
        if (poolStack.Count > 0)
        {
            tmp = poolStack.Pop();
            tmp.SetActive(true);
        }
        else
        {
            tmp = GameObject.Instantiate(baseGO);
            returnPool = tmp.AddComponent<ReturnToMyPool>();
            returnPool.myPool = this;
        }
        return tmp;
    }

    public void AddToPool(GameObject go)
    {
        go.SetActive(false);
        poolStack.Push(go);
    }
}