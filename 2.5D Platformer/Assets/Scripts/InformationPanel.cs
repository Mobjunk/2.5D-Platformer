using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InformationPanel : MonoBehaviour
{
    /// <summary>
    /// Creates an singleton for the information panel
    /// </summary>
    public static InformationPanel instance;
    /// <summary>
    /// Reference to the game manager
    /// </summary>
    GameManager gameManager => GameManager.instance;
    /// <summary>
    /// All the game objects for all the panel options
    /// </summary>
    [SerializeField] private GameObject infoPanel, playerSprite, playerLivesObject, worldObject, gameOverObject;
    /// <summary>
    /// All the Text components for the lives, world and game over
    /// </summary>
    private Text playerLivesText, worldText, gameOverText;
    /// <summary>
    /// The time in wich the info panel will be shown
    /// </summary>
    private float displayTimer = 2.5f;

    private void Start()
    {
        instance = this;
        playerLivesText = playerLivesObject.GetComponent<Text>();
        worldText = worldObject.GetComponent<Text>();
        gameOverText = gameOverObject.GetComponent<Text>();
    }

    void Update()
    {
        if (displayTimer > 0)
        {
            displayTimer -= Time.deltaTime;
            //Hide the panel
            if (displayTimer < 0)
            {
                infoPanel.SetActive(false);
                gameManager.gameStarted = true;
                displayTimer = 0;
                //Respawn the enemies and objects
                gameManager.RespawnEnemies();
                gameManager.RespawnObjects();
            }
        }
    }
    
    /// <summary>
    /// Display the default panel
    /// </summary>
    public void DisplayPanel()
    {
        gameManager.gameStarted = false;
        infoPanel.SetActive(true);
        playerLivesText.text = $"x  {PlayerInformation.instance.playerLives}";
        displayTimer = 2.5f;
    }

    /// <summary>
    /// Display the game over panel
    /// </summary>
    public void GameOver()
    {
        infoPanel.SetActive(true);
        playerSprite.SetActive(false);
        playerLivesObject.SetActive(false);
        worldObject.SetActive(false);
        gameOverObject.SetActive(true);
    }
}
