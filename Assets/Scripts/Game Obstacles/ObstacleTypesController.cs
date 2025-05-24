using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTypesController : MonoBehaviour
{
    public static ObstacleTypesController Instance { get; private set; }

    [SerializeField] public GameObject wall_Obstacle;
    [SerializeField] public GameObject left_Obstacle;
    [SerializeField] public GameObject right_Obstacle;
    [SerializeField] public GameObject up_Obstacle;
    [SerializeField] public GameObject down_Obstacle;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
