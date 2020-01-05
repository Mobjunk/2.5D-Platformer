using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApookApoort : MonoBehaviour
{
    /// <summary>
    /// The enemy controller attached to this game object
    /// </summary>
    EnemyController enemyController;
    /// <summary>
    /// The mesh renderer attached to this game object
    /// </summary>
    MeshRenderer meshRenderer;

    /// <summary>
    /// The materials to show wich koopa it is
    /// </summary>
    [SerializeField]
    Material regularMaterial, stompedMaterial;

    /// <summary>
    /// To check if the enemy has been stomped on his head
    /// </summary>
    bool hasBeenStomped;

    /// <summary>
    /// To check if the enemy has been kicked
    /// </summary>
    bool hasBeenKicked;

    /// <summary>
    /// The time for the koopa to get out of his shell
    /// </summary>
    float shellTimer;

    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {

        //Checks if the enemy has been kicked and hasnt been kicked before
        if(enemyController.kickedEnemy)
        {
            //Checks if the enemy hasnt been kicked yet
            if (!hasBeenKicked)
            {
                hasBeenKicked = true;
                //Make sure the koopa cannot return while the shell is moving
                shellTimer = 0;
                enemyController.pauseMovement = false;
                //Increase the movement speed times 3
                enemyController.movementSpeed *= 3;
            }
            //If the shell is already moving around
            else
            {
                hasBeenKicked = false;
                //Makes it so the koopa can return
                shellTimer = 7.5f;
                enemyController.pauseMovement = true;
                //Decrease the movement speed devided by 3
                enemyController.movementSpeed /= 3;
            }
            enemyController.kickedEnemy = false;
        }

        //Checks if the enemy has been stomped on his head
        if(enemyController.stompedHead && !hasBeenStomped)
        {
            hasBeenStomped = true;
            shellTimer = 7.5f;
            //Sets the material of the mesh renderer to see the difference
            meshRenderer.material = stompedMaterial;
        }

        //Checks if the enemy has been stomped and the shell timer need to be ran
        if(hasBeenStomped && shellTimer > 0)
        {
            //Decrease the shell timer
            shellTimer -= Time.deltaTime;
            //Checks if the enemy has to return
            if (shellTimer <= 0)
            {
                //Reset the variables from this class
                shellTimer = 0;
                hasBeenStomped = false;
                //Handles reseting variables in the enemy controller
                enemyController.stompedHead = false;
                enemyController.pauseMovement = false;
                //Reset the material
                meshRenderer.material = regularMaterial;
            }
        }
    }
}
