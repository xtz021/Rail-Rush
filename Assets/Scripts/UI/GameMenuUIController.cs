using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuUIController : MonoBehaviour
{
    public static GameMenuUIController Instance { get; private set; }

    private const string SCENE_ID_MININGCAVE_STRING = "Scenes/MiningCave_Gameplay";

    private GameMenuUIController() { }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SCENE_ID_MININGCAVE_STRING, LoadSceneMode.Single);
        Time.timeScale = 1f;
    }
}
