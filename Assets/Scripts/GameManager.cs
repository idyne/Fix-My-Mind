using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameState gameState = GameState.INIT;
    public GameType gameType = GameType.DIALOGUE;
    public int level = 1;
    public Text debugText;

    private void Awake()
    {
        InstantiateLevelManager();
    }

    public void StartGame()
    {
        print("Level " + level);
        Singleton.TAPTOPLAY.SetActive(false);
        gameState = GameState.STARTED;
        Notebook notebook = Singleton.NOTE;
        if (notebook)
            notebook.Check();
        else
        {
            Singleton.PATIENT.TriggerMoraleBar();
            StartCoroutine(Singleton.DM.ShowSpeechBalloon(0));
        }


    }
    public void EndGame()
    {
        Singleton.PATIENT.React();
    }

    public void ShowCompleteScreen()
    {
        CompleteScreen COMP = Singleton.COMP;
        COMP.gameObject.SetActive(true);
    }

    private void InstantiateLevelManager()
    {
        if (LevelManager.INSTANCE == null)
        {
            GameObject levelManager = new GameObject("LevelManager");
            levelManager.AddComponent<LevelManager>();
        }
    }
}

public enum GameState { INIT, STARTED, PAUSED, ENDED }
public enum GameType { DIALOGUE }