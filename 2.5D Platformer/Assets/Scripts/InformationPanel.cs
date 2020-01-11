using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InformationPanel : MonoBehaviour
{
    public static InformationPanel instance;
    GameManager gameManager => GameManager.instance;
    
    [SerializeField] private GameObject infoPanel, playerSprite, playerLivesObject, worldObject, gameOverObject;
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
            if (displayTimer < 0)
            {
                //if (GroundDetection.instance.isGrounded)
                //{
                    
                //}
                infoPanel.SetActive(false);
                gameManager.gameStarted = true;
                displayTimer = 0;
                gameManager.RespawnEnemies();
                gameManager.RespawnObjects();
            }
        }
    }
    
    public void DisplayPanel()
    {
        gameManager.gameStarted = false;
        infoPanel.SetActive(true);
        playerLivesText.text = $"x  {PlayerInformation.instance.playerLives}";
        displayTimer = 2.5f;
    }

    public void GameOver()
    {
        infoPanel.SetActive(true);
        playerSprite.SetActive(false);
        playerLivesObject.SetActive(false);
        worldObject.SetActive(false);
        gameOverObject.SetActive(true);
    }
}
