using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    int gemValue;
    AudioSource audioSource;

    private void Start()
    {
        GetGemValue();
        audioSource = GetComponent<AudioSource>();
    }

    private void GetGemValue()
    {
        foreach(GemValueHandler.Gem gem in GemValueHandler.Instance.GetGemList())
        {
            if(gameObject.name.Contains(gem.Name))
            {
                gemValue = gem.Value;
                break;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InGameController.Instance.GainGold(gemValue);
            if (audioSource != null)
            {
                audioSource.Play();
            }
            Destroy(gameObject);
        }
    }

}
