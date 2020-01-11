using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    public static PlayerInformation instance;

    public Vector3 startingPosition;
    
    public PlayerState playerState = PlayerState.MINI;

    public int playerLives = 3;

    void Start()
    {
        instance = this;
        startingPosition = transform.position;
    }

    public bool CanDestroyBricks()
    {
        return !playerState.Equals(PlayerState.MINI);
    }

    public void DecreaseLives()
    {
        playerLives--;
    }

    public void IncreaseLives()
    {
        playerLives++;
    }
}

public enum PlayerState
{
    MINI,
    BIG,
    BANANA_MODE
}
