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
        for (int i = 0; i < 10; i++)
        {
            gameObject.transform.TransformPoint(0, -100, 0);
            GameObject clone = Instantiate(reward.prefab, gameObject.transform.position, Quaternion.identity);

            Rigidbody rb = clone.GetComponent<Rigidbody>();

            rb.AddForce(Vector3.right * Random.Range(-100, 50));
            rb.AddForce(Vector3.up * Random.Range(50, 150));
        }

        Destroy(gameObject);

        gameManager.IncreaseScore(50);

        //Play the block breaking sound
        soundPlayer.PlaySound(reward.sound);
    }
}
