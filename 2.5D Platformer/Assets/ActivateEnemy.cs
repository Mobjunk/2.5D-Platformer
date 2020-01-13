using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemy : MonoBehaviour
{
    /// <summary>
    /// The enemies that need to start walking
    /// </summary>
    [SerializeField] private GameObject[] changeEnemies;
    private void OnTriggerEnter(Collider other)
    {
        string ends = gameObject.name.Substring(gameObject.name.Length - 3);//gameObject.name.
        //print($"name: {ends}");
        if (other.tag.Equals("Player"))
        {
            foreach(GameObject enemy in changeEnemies)
            {
                EnemyController controller = enemy.GetComponent<EnemyController>();
                if (controller.pauseMovement)
                    controller.pauseMovement = false;
            }
        }
    }
}
