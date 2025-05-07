using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuggetScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InGameController.GoldCount++;
            Destroy(gameObject);
        }
    }
}
