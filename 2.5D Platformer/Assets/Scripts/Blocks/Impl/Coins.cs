using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Coins : Rewards
{
    /// <summary>
    /// The amount of coins left in this brick
    /// </summary>
    [SerializeField] int coinsLeft;
    /// <summary>
    /// The start amount of the coins
    /// </summary>
    [HideInInspector] private int startCoins;

    public override void Start()
    {
        startCoins = coinsLeft;
        base.Start();
    }

    /// <summary>
    /// Handles resetting the data of the block
    /// </summary>
    public override void Reset()
    {
        coinsLeft = startCoins;
        base.Reset();
    }

    /// <summary>
    /// Handles the coins within the block
    /// </summary>
    protected override void HandleRewards()
    {
        coinsLeft--;
        GameManager.instance.IncreaseCoins(1);
        if(coinsLeft < 1)
            //Sets the material of the object to the empty one
            meshRenderer.material = empty;
    }
}
