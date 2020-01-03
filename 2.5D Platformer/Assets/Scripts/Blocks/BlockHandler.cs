using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlockHandler : MonoBehaviour
{
    public GameManager gameManager => GameManager.instance;
    public SoundPlayer soundPlayer => SoundPlayer.instance;

    public Reward reward;

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
                //print($"rigidbody.velocity.y: {rigidbody.velocity.y}");
                //Checks if the connection point was from the bottom
                if (Vector3.Dot(contact.normal, Vector3.up) > 0.5 && rigidbody.velocity.y >= 0)
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

    public IEnumerator BrickGoingUp()
    {
        Transform transform = gameObject.transform;
        Vector3 oldPosition = transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        yield return new WaitForSeconds(0.2f);
        transform.position = oldPosition;
    }

    public void HandleMovement()
    {
        StartCoroutine(BrickGoingUp());
    }

    public abstract void HandleCollision();

    public abstract bool CanInteract();

}
