using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private float gameLength = 30f;
    private float gameTimer;
    private int gameHp = 3;

    private bool restarted = false;

    [SerializeField]
    private Text timerText;
    [SerializeField]
    private Text playerHpText;
    [SerializeField]
    private PlayerController player;

    [SerializeField]
    GameObject inputField;

    private void Awake()
    {
        gameTimer = gameLength;
        Enemy.OnAnyReachGoal += EnemyPass;
    }

    private void EnemyPass(Enemy enemy)
    {
        gameHp -= 1;
        if(gameHp == 0)
        {
            Debug.Log("Game Over");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inputField.SetActive(true);
            Time.timeScale = 0;
        }

        if (gameTimer <= 0.00)
        {
            gameTimer = 0;
            Debug.Log("Game Won!");
            EndMinigame();
        }
        else
        {
            gameTimer -= Time.deltaTime;
        }
        timerText.text = Math.Abs(gameTimer).ToString("0.00");
        playerHpText.text = "HP:"  + player.currentHp.ToString();
        if (player.currentHp <= 0 && restarted == false)
            RestartGame();
    }

    public void EndMinigame()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(current);
        FolderType.root.SetActive(true);
    }

    public void EnterCode(string code)
    {
        inputField.SetActive(false);
        Time.timeScale = 1;

        if (code == "AmBigBabyPlsHelp")
        {
            EndMinigame();
        }
    }

    void RestartGame()
    {
        var enemy = GameObject.FindObjectsOfType<Enemy>();
        foreach (var en in enemy)
        {
            Destroy(en.gameObject);
        }

        var enemy2 = GameObject.FindObjectsOfType<StateMachine>();
        foreach (var en in enemy2)
        {
            Destroy(en.gameObject);
        }

        player.Reset();
        gameTimer = gameLength;
    }
}
