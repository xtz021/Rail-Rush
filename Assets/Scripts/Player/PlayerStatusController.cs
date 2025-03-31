using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusController : MonoBehaviour
{
    public PlayerStatus playerCurrentStatus;
    public int playerCurrentRail;


    private void Start()
    {
        playerCurrentStatus = PlayerStatus.OffRail;
        playerCurrentRail = 0;
    }


}


public enum PlayerStatus
{
    Jump, OnRail, OffRail
}
