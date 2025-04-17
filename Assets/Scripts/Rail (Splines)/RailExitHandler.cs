using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailExitHandler : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        SetPlayerEnter();
    }

    private void SetPlayerEnter()
    {
        RailScript[] railList = transform.parent.GetComponentsInChildren<RailScript>();
        foreach (RailScript rail in railList)
        {
            rail.playerExit = true;
        }
    }
}
