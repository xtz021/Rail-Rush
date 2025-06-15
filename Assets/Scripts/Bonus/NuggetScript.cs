using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuggetScript : MonoBehaviour
{
    int goldValue = 1;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InGameController.Instance.GainGold(goldValue * InventoryManager.Instance.NuggetBonusMultiplier);
            if (audioSource != null)
            {
                audioSource.Play();
            }
            Destroy(gameObject);
        }
    }
}
