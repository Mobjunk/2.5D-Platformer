using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Singleton for the gamemanager
    /// </summary>
    public static GameManager instance;
    /// <summary>
    /// Reference to the player information
    /// </summary>
    private PlayerInformation playerInformation => PlayerInformation.instance;
    /// <summary>
    /// Reference to the information panel
    /// </summary>
    private InformationPanel informationPanel => InformationPanel.instance;
    /// <summary>
    /// Reference to the player invincibility
    /// </summary>
    private PlayerInvincibility playerInvincibility => PlayerInvincibility.instance;
    /// <summary>
    /// Reference to the sound player
    /// </summary>
    private SoundPlayer soundPlayer => SoundPlayer.instance;
    /// <summary>
    /// The game object for the player
    /// </summary>
    [SerializeField] public GameObject playerObject;
    /// <summary>
    /// The reference to the text for Score, Coins and Time
    /// </summary>
    [SerializeField] private Text scoreText, coinText, timeText;
    /// <summary>
    /// Checks if the game has been started
    /// </summary>
    [HideInInspector] public bool gameStarted = false, levelEnding = false, levelFinished;
    /// <summary>
    /// The time of the map
    /// </summary>
    [HideInInspector] public float mapTimer = 400f;
    /// <summary>
    /// The current score of the player and how many coins he has collected
    /// </summary>
    private int currentScore = 0, currentCoins;
    /// <summary>
    /// A check to see if the player has been warned for the time
    /// </summary>
    private bool playedWarning = false;
    /// <summary>
    /// A array of all the spawned enemies
    /// </summary>
    [HideInInspector] public GameObject[] spawnedEnemies;
    /// <summary>
    /// A list of all the positions of the enemies
    /// </summary>
    public List<Vector3> enemiesPositions = new List<Vector3>();
    /// <summary>
    /// A array of all interactable objects
    /// </summary>
    private GameObject[] interactableObjects;

    void Start()
    {
        //Sets the singleton
        instance = this;
        //Grabs all the game objects with the tag 'Interactable'
        interactableObjects = GameObject.FindGameObjectsWithTag("Interactable");
        //Grabs all the game objects with the tag 'Enemy'
        spawnedEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        //Loops though all the objects and adds there positions to a list
        foreach(GameObject enemy in spawnedEnemies)
            enemiesPositions.Add(enemy.transform.position);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        
        if (!gameStarted) return;

        if (!levelEnding)
        {
            UpdateTime(Time.deltaTime);
            if (mapTimer < 100 && !playedWarning)
            {
                soundPlayer.PlaySound(Sounds.TIME_WARNING);
                playedWarning = true;
            }   
        }
    }

    /// <summary>
    /// Handles updating the time and the UI
    /// </summary>
    public void UpdateTime(float time)
    {
        if (levelFinished) return;
        
        mapTimer -= time;
        if (mapTimer < 0) mapTimer = 0;
        timeText.text = $"Time\n{Math.Floor(mapTimer)}";
    }

    /// <summary>
    /// Increase the score of the player
    /// </summary>
    /// <param name="score">The amount it has to increase the score</param>
    public void IncreaseScore(int score)
    {
        currentScore += score;
        scoreText.text = $"{currentScore}";
    }

    /// <summary>
    /// Increase the coins
    /// </summary>
    /// <param name="coinAmount">The amount of coins to add</param>
    public void IncreaseCoins(int coinAmount)
    {
        currentCoins += coinAmount;
        coinText.text = $"X {currentCoins}";
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Handles the death of the player
    /// </summary>
    public void HandleDeath()
    {
        //Checks if the player is invincible
        if (playerInvincibility.IsInvincible()) return;
        
        //Checks if the player isn't mini
        if (!playerInformation.playerState.Equals(PlayerState.MINI))
        {
            //Scale down the player
            playerObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            //The transform of the player
            Transform trans = playerObject.transform;
            //Sets the player back on the ground
            trans.position = new Vector3(trans.position.x, trans.position.y - 0.25f, trans.position.z);
            //Sets the state of the player
            playerInformation.playerState = PlayerState.MINI;
            //Starts the invincibility process
            playerInvincibility.StartInvincibility();
            return;
        }

        soundPlayer.PlaySound(Sounds.PLAYER_DIES);
        //Sets the time text
        timeText.text = "Time";
        //Reset the map time
        mapTimer = 400f;
        //Reset the velocity of the player
        playerObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //Sets the players position back to the starting position
        playerObject.transform.position = playerInformation.startingPosition;
        //Decrease the lives of the player
        playerInformation.DecreaseLives();
        //Checks if the player has no lives left
        if (playerInformation.playerLives <= 0)
        {
            playerObject.SetActive(false);
            informationPanel.GameOver();
        }
        else
            informationPanel.DisplayPanel();
    }

    /// <summary>
    /// Handles respawning the enemies when the information panel closes
    /// </summary>
    public void RespawnEnemies()
    {
        //Loops though all the enemies
        for (int index = 0; index < spawnedEnemies.Length; index++)
        {
            //Grabs the enemy object
            GameObject enemy = spawnedEnemies[index];
            //Grabs the start position of the enemy
            Vector3 position = enemiesPositions[index];
            //Sets the position
            enemy.transform.position = position;
            //Activates the enemy again
            enemy.SetActive(true);
            //Reset the enemy controller
            enemy.GetComponent<EnemyController>().Reset();
            //Reset the koopa's
            if (enemy.GetComponent<ApookApoort>() != null) enemy.GetComponent<ApookApoort>().Reset();
        }
    }

    /// <summary>
    /// Handles respawning all the interactable objects when the information panel closes
    /// </summary>
    public void RespawnObjects()
    {
        //Loops though all the interactable objects
        foreach (GameObject bricks in interactableObjects)
        {
            //Enables the block again
            bricks.SetActive(true);
            //Reset the question mark blocks
            if(bricks.GetComponent<QuestionMark>() != null) bricks.GetComponent<QuestionMark>().Reset();
            //Reset the coin bricks
            if(bricks.GetComponent<Coins>() != null) bricks.GetComponent<Coins>().Reset();
        }
    }
}
