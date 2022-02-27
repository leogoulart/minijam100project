using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerController player;
    public UIManager uiManager;

    public enum GameStates
    {
        GameRunning,
        IsPaused,
        Death,
        OutBreaking
    };
    public GameStates currentState = GameStates.GameRunning;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (player.bloodOverloaded)
        {
            uiManager.gameOverUI.SetActive(true);
            currentState = GameStates.Death;
        }

        if (player.isOutebreaking)
        {
            uiManager.outbreakUI.SetActive(true);
            currentState = GameStates.OutBreaking;
        }
        else
        {
            uiManager.outbreakUI.SetActive(false);
            currentState = GameStates.GameRunning;
        }
    }
}
