using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuggetScript : MonoBehaviour
{
    int goldValue = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InGameController.Instance.GainGold(goldValue);
            Destroy(gameObject);
        }
    }
}
