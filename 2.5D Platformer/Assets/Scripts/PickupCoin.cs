using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoin : MonoBehaviour
{
    /// <summary>
    /// Reference to the game manager script
    /// </summary>
    private GameManager gameManager => GameManager.instance;
    
    private void OnTriggerEnter(Collider other)
    {
        //Checks if the collision is the player
        if (other.tag.Equals("Player"))
        {
            //Disable the coin
            gameObject.SetActive(false);
            //Increase coins
            gameManager.IncreaseCoins(1);
            //Increase score
            gameManager.IncreaseScore(100);
        }
    }
}
