using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [SerializeField] private GameObject nuggetPrefab;
    public MyPool nuggetPool;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        Debug.Log("" + gameObject.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        nuggetPool = new MyPool(nuggetPrefab);
    }

}
