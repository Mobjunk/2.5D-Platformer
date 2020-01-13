using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rewards : BlockHandler
{
    /// <summary>
    /// The materials for this block
    /// </summary>
    [HideInInspector] public Material empty, regular;
    /// <summary>
    /// The position to were the power up has to go to
    /// </summary>
    [HideInInspector] public Transform spawnPoint;
    /// <summary>
    /// The power up prefab
    /// </summary>
    [HideInInspector] public GameObject powerup;
    /// <summary>
    /// If the power up or coin is going upwards
    /// </summary>
    [HideInInspector] public bool sendUpwards = false;
    /// <summary>
    /// Checks if the power up or coin has to be destroyed
    /// </summary>
    [HideInInspector] public bool destroy = false;
    /// <summary>
    /// The mesh renderer of the block
    /// </summary>
    [HideInInspector] public MeshRenderer meshRenderer;

    public virtual void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public virtual void Update()
    {
        //Handles the power up going upwards
        if(powerup != null && sendUpwards && powerup.transform.position.y < spawnPoint.transform.position.y)
            powerup.transform.position = new Vector3(powerup.transform.position.x, powerup.transform.position.y + 0.025f, powerup.transform.position.z);
        else
        {
            sendUpwards = false;
            //Checks if the 'reward' is a power up
            if(!reward.powerup)
            {
                //Checks if the player up has to been destroyed
                if (destroy)
                    Destroy(powerup);
            }
        }
    }

    /// <summary>
    /// Checks if its an interactable object
    /// </summary>
    /// <returns></returns>
    public override bool CanInteract()
    {
        return true;
    }

    /// <summary>
    /// Handles the player collision
    /// </summary>
    public override void HandleCollision()
    {
        //Checks if the block is already empty
        if (meshRenderer.material.name.Equals("Empty Brick (Instance)")) return;

        HandleRewards();
        
        //Makes the brick move up and down
        HandleMovement();

        StartCoroutine(HandleSpawn());
    }

    /// <summary>
    /// Handles resetting the object
    /// </summary>
    public virtual void Reset()
    {
        meshRenderer.material = regular;
    }

    /// <summary>
    /// Handles spawning the power up or coin
    /// </summary>
    /// <returns></returns>
    IEnumerator HandleSpawn()
    {
        yield return new WaitForSeconds(0.2f);

        powerup = Instantiate(reward.prefab, transform.position, Quaternion.identity);

        soundPlayer.PlaySound(reward.sound);

        sendUpwards = true;
    }

    /// <summary>
    /// Handles rewards like coins
    /// </summary>
    protected abstract void HandleRewards();
}
