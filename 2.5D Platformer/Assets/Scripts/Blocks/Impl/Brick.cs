using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : BlockHandler
{
    public override bool CanInteract()
    {
        return PlayerInformation.instance.CanDestroyBricks();
    }

    public override void HandleCollision()
    {
        //Creates 10 small versions of the brick and make them spawn to different
        for (int i = 0; i < 10; i++)
        {
            gameObject.transform.TransformPoint(0, -100, 0);
            //Spawns a small brick
            GameObject clone = Instantiate(reward.prefab, gameObject.transform.position, Quaternion.identity);

            //Grabs the rigidbody of the spawned little brick
            Rigidbody rb = clone.GetComponent<Rigidbody>();

            //Adds force to the right and up with a random range
            rb.AddForce(Vector3.right * Random.Range(-100, 50));
            rb.AddForce(Vector3.up * Random.Range(50, 150));
        }

        Destroy(gameObject);

        gameManager.IncreaseScore(50);

        //Play the block breaking sound
        soundPlayer.PlaySound(reward.sound);
    }
}
