using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankManager : MonoBehaviour
{
    public static RankManager Instance {  get; private set; }

    public RankListDataSO ranksData;

    private void Awake()
    {
        Instance = this;
        ranksData.LoadCurrentRankData();
    }

    private void Start()
    {
        
    }
}
