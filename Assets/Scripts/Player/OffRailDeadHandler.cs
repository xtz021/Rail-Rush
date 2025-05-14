using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffRailDeadHandler : MonoBehaviour
{
    public const string CAUSEOFDEATH_OFFRAIL = "EndRail";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && PlayerStatusController.Instance.playerCurrentStatus != PlayerStatus.Dead)
        {
            Debug.Log("Player falls off cliff!!!");
            PlayerStatusController.Instance.playerCurrentStatus = PlayerStatus.Dead;
            CharacterAnimationController.Instance.Dead(CAUSEOFDEATH_OFFRAIL);
            CartAnimationController.Instance.DeadAnimation(CAUSEOFDEATH_OFFRAIL);
        }
    }
}
