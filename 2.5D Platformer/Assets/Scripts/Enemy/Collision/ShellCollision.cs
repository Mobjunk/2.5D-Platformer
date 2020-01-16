using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellCollision : MonoBehaviour
{
    /// <summary>
    /// A referance to the game manager script
    /// </summary>
    GameManager gameManager => GameManager.instance;

    /// <summary>
    /// The enemy controller attached to this game object
    /// </summary>
    EnemyController enemyController;

    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
    }

    void OnTriggerEnter(Collider other)
    {
        //Checks if a shell is making contact with an enemy & has been stomped
        if (other.gameObject.tag.Equals("Enemy") && enemyController.stompedHead)
        {
            //Increase the score
            gameManager.IncreaseScore(other.GetComponent<EnemyController>().enemy.score);
            //Remove the enemy
            other.gameObject.SetActive(false);
        }
    }
}
