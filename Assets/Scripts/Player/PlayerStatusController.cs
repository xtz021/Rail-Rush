using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusController : MonoBehaviour
{
    public PlayerStatus playerCurrentStatus;


    private void Start()
    {
        playerCurrentStatus = PlayerStatus.OffRail;
    }


}

public enum PlayerStatus
{
    Jump, OnRail, OffRail
}
