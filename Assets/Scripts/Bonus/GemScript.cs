using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    int gemValue = 0;
    string gemName;
    AudioSource audioSource;

    private void Start()
    {
        GetGemValue();
        audioSource = GetComponent<AudioSource>();
    }

    private void GetGemValue()
    {
        foreach(Gem gem in GemValueHandler.Instance.GetGemList())
        {
            if(gameObject.name.Contains(gem.Name))
            {
                gemValue = gem.Value;
                gemName = gem.Name;
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
