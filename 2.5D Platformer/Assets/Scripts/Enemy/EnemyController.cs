using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : ObjectMovement
{
    /// <summary>
    /// Reference to the game manager script
    /// </summary>
    GameManager gameManager => GameManager.instance;
    
    SoundPlayer soundPlayer => SoundPlayer.instance;

    /// <summary>
    /// The scriptable object attached to this enemy
    /// </summary>
    [HideInInspector]
    public Enemy enemy;
    
    /// <summary>
    /// The name of the enemy
    /// </summary>
    string name;

    /// <summary>
    /// The score the enemy gives when defeated
    /// </summary>
    int score;

    /// <summary>
    /// Check to see if the enemy has been stomped on the head already
    /// </summary>
    [HideInInspector]
    public bool stompedHead;

    /// <summary>
    /// Check to see if the enemy is being kicked
    /// </summary>
    [HideInInspector]
    public bool kickedEnemy;

    public override void Start()
    {
        base.Start();
        this.name = enemy.name;
        this.score = enemy.score;
    }

    public void Reset()
    {
        stompedHead = false;
        kickedEnemy = false;
        pauseMovement = defaultMovement;
        direction = -1;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (collision.contacts.Length > 0)
            {
                ContactPoint contact = collision.contacts[0];
                if (Vector3.Dot(contact.normal, Vector3.down) > 0.5f)
                {
                    //Grabs the player jumping script from the player the enemy made collision with
                    PlayerJumping jump = collision.gameObject.GetComponent<PlayerJumping>();
                    //Makes the player jump up
                    jump.Jump(5f);
                    soundPlayer.PlaySound(Sounds.KICKING);
                    if (!stompedHead)
                    {
                        stompedHead = true;
                        gameManager.IncreaseScore(score);

                        //Checks if the enemy has to been deleted from the scene
                        if (enemy.deleted) gameObject.SetActive(false); //Destroy(gameObject);
                        //Pauses the movement of the enemy
                        else pauseMovement = true;
                    }
                    else kickedEnemy = true;
                }
                //Checks if the player hits the enemy while its not moving anymore
                else if (stompedHead && pauseMovement)
                {
                    //Grabs the new direction of the enemy
                    direction = (int) Input.GetAxisRaw("Horizontal");
                    kickedEnemy = true;
                    soundPlayer.PlaySound(Sounds.KICKING);
                }
                else gameManager.HandleDeath();
            }
        }
        else if (collision.gameObject.tag.Equals("Floor") || collision.gameObject.tag.Equals("Interactable"))
        {
            //Switch direction
            direction = direction == 1 ? -1 : 1;
            //Checks if the enemy hit a interactable and is in shell form (aka been stomped and moving)
            if (collision.gameObject.tag.Equals("Interactable") && stompedHead)
            {
                BlockHandler blockHandler = collision.gameObject.GetComponent<BlockHandler>();
                blockHandler.HandleCollision();
            }
        }
    }
}
