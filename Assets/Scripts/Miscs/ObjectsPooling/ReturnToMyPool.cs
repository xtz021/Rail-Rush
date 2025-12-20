using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMyPool : MonoBehaviour
{
    public MyPool myPool;

    private void Start()
    {
        if (gameObject.tag == "Bonus" && gameObject.name.Contains("Nugget"))
        {
            myPool = PoolManager.Instance.nuggetPool;
        }
        if (myPool == null)
        {
            Debug.LogError("MyPool reference is missing in ReturnToMyPool component.");
        }
    }

    public void OnDisable()
    {
        if (myPool != null)
        {
            myPool.AddToPool(gameObject);
        }
    }

}
