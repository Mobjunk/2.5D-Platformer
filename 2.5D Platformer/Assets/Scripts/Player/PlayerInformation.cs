using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    public static PlayerInformation instance;

    public PlayerState playerState = PlayerState.MINI;

    void Start()
    {
        instance = this;
    }

    public bool CanDestroyBricks()
    {
        return !playerState.Equals(PlayerState.MINI);
    }
}

public enum PlayerState
{
    MINI,
    BIG,
    BANANA_MODE
}
