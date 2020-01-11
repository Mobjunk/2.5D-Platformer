using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    private PlayerInformation playerInformation => PlayerInformation.instance;
    private InformationPanel informationPanel => InformationPanel.instance;
    private PlayerInvincibility playerInvincibility => PlayerInvincibility.instance;
    
    [SerializeField] public GameObject playerObject;
    [SerializeField] private Text scoreText, coinText, timeText;

    [HideInInspector] public bool gameStarted = false;
    private float mapTimer = 400f;
    private int currentScore = 0, currentCoins;
    private bool playedWarning = false;
    
    private GameObject[] spawnedEnemies;
    public List<Vector3> enemiesPositions = new List<Vector3>();

    private GameObject[] interactableObjects;

    void Start()
    {
        instance = this;

        interactableObjects = GameObject.FindGameObjectsWithTag("Interactable");

        //Grabs all the game objects with the tag 'Enemy'
        spawnedEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        //Loops though all the objects and adds there positions to a list
        foreach(GameObject enemy in spawnedEnemies)
            enemiesPositions.Add(enemy.transform.position);
    }

    void Update()
    {
        if (!gameStarted) return;
        
        UpdateTime();
        if (mapTimer < 100 && !playedWarning)
        {
            SoundPlayer.instance.PlaySound(Sounds.TIME_WARNING);
            playedWarning = true;
        }
    }

    void UpdateTime()
    {
        mapTimer -= Time.deltaTime;
        timeText.text = $"Time\n{Math.Floor(mapTimer)}";
    }

    public void IncreaseScore(int score)
    {
        currentScore += score;
        scoreText.text = $"{currentScore}";
    }

    public void IncreaseCoins(int coinAmount)
    {
        currentCoins += coinAmount;
        coinText.text = $"X {currentCoins}";
    }

    public void HandleDeath()
    {
        if (playerInvincibility.IsInvincible()) return;
        
        if (!playerInformation.playerState.Equals(PlayerState.MINI))
        {
            playerObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Transform trans = playerObject.transform;
            trans.position = new Vector3(trans.position.x, trans.position.y - 0.25f, trans.position.z);
            playerInformation.playerState = PlayerState.MINI;
            playerInvincibility.StartInvincibility();
            return;
        }
        
        timeText.text = "Time";
        mapTimer = 400f;
        playerObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerObject.transform.position = playerInformation.startingPosition;
        playerInformation.DecreaseLives();
        //Checks if the player has no lives left
        if(playerInformation.playerLives <= 0)
            informationPanel.GameOver();
        else
            informationPanel.DisplayPanel();

    }

    public void RespawnEnemies()
    {
        for (int index = 0; index < spawnedEnemies.Length; index++)
        {
            GameObject enemy = spawnedEnemies[index];
            Vector3 position = enemiesPositions[index];
            enemy.transform.position = position;
            enemy.SetActive(true);
            enemy.GetComponent<EnemyController>().Reset();
            if (enemy.GetComponent<ApookApoort>() != null) enemy.GetComponent<ApookApoort>().Reset();
        }
    }

    public void RespawnObjects()
    {
        foreach (GameObject bricks in interactableObjects)
        {
            bricks.SetActive(true);
            if(bricks.GetComponent<QuestionMark>() != null) bricks.GetComponent<QuestionMark>().Reset();
        }
    }
}
