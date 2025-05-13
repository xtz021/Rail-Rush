using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDistanceCalculator : MonoBehaviour
{
    public static LevelDistanceCalculator Instance { get; private set; }

    public Text distanceText;
    public int distanceRun = 0;
    public bool addingDistance = false;
    public float updateInterval = 0.25f;

    private float actualDistance = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Multiple instances of LevelDistanceCalculator detected. Destroying duplicate.");      
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        addingDistance = false;
        distanceText.text = "0";
    }

    private void Update()
    {
        AddingDistance();
    }


    private void AddingDistance()
    {
        PlayerStatus playerStatus = PlayerStatusController.Instance.playerCurrentStatus;
        if (!addingDistance && playerStatus != PlayerStatus.Dead && playerStatus != PlayerStatus.OffRail)
        {
            addingDistance = true;
            StartCoroutine(AddDistance());
        }
    }

    private IEnumerator AddDistance()
    {
        actualDistance += PlayerCartMovement.Instance._PlayerCartSpeed * updateInterval;
        distanceRun = (int)actualDistance;
        distanceText.text = "" + distanceRun.ToString();
        yield return new WaitForSeconds(updateInterval);
        addingDistance = false;
        //Debug.Log("Distance: " + distanceRun.ToString());
        yield break;
    }
}
