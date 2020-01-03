using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float mapTimer = 400f;
    [SerializeField] Text scoreText, coinText, timeText;
    private int currentScore = 0, currentCoins;
    private bool playedWarning = false;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        UpdateTime();
        if ((mapTimer - Time.time) < 100 && !playedWarning)
        {
            SoundPlayer.instance.PlaySound(Sounds.TIME_WARNING);
            playedWarning = true;
        }
    }

    void UpdateTime()
    {
        timeText.text = $"Time\n{Math.Floor(mapTimer - Time.time)}";
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
        print("player loses a live bla bla");
    }
}
