using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlockHandler : MonoBehaviour
{
    /// <summary>
    /// Reference to the game manager
    /// </summary>
    public GameManager gameManager => GameManager.instance;
    /// <summary>
    /// Reference to the sound player
    /// </summary>
    public SoundPlayer soundPlayer => SoundPlayer.instance;
    /// <summary>
    /// The reward attachted to this block
    /// </summary>
    public Reward reward;

    /// <summary>
    /// The player colliding with the brick
    /// </summary>
    /// <param name="collision">The object colliding with this</param>
    void OnCollisionEnter(Collision collision)
    {
        //Checks if there was collision with the player
        if (collision.gameObject.tag.Equals("Player"))
        {
            //Checks if there was any contact at all
            if (collision.contacts.Length > 0)
            {
                GameObject player = GameObject.Find("Player");
                Rigidbody rigidbody = player.GetComponent<Rigidbody>();

                //Grabs the first contact point
                ContactPoint contact = collision.contacts[0];
                // && rigidbody.velocity.y >= 0
                //print($"rigidbody.velocity.y: {rigidbody.velocity.y}");
                //Checks if the connection point was from the bottom
                if (Vector3.Dot(contact.normal, Vector3.up) > 0.5)
                {
                    //Checks if the player can interact bricks
                    if (CanInteract())
                    {
                        HandleCollision();
                    }
                    else
                    {
                        //Move the block up and down
                        HandleMovement();
                        //Play the block break failure sound
                        SoundPlayer.instance.PlaySound(Sounds.FAILED_BREAK);
                    }
                }
            }
        }
    }

    /// <summary>
    /// The IEnumerator for the brick going upwards and back down
    /// </summary>
    /// <returns></returns>
    public IEnumerator BrickGoingUp()
    {
        //The transform of the game object
        Transform transform = gameObject.transform;
        //The old position of the game object
        Vector3 oldPosition = transform.position;
        //Handles the brick going upwards
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        //Adds a delay
        yield return new WaitForSeconds(0.2f);
        //Sets the brick back to the old position
        transform.position = oldPosition;
    }

    /// <summary>
    /// Handles the brick going upwards
    /// </summary>
    protected void HandleMovement()
    {
        StartCoroutine(BrickGoingUp());
    }

    public abstract void HandleCollision();

    /// <summary>
    /// Check if the brick is interactable
    /// </summary>
    /// <returns>If the brick is interactable</returns>
    public abstract bool CanInteract();

}
