using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBrick : BlockHandler
{
    [SerializeField] private Material empty;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject powerup;
    [SerializeField] public int coinsLeft = 7;
    [SerializeField] bool sendUpwards = false;

    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    
    void Update()
    {
        if(powerup != null && sendUpwards && powerup.transform.position.y < spawnPoint.transform.position.y)
            powerup.transform.position = new Vector3(powerup.transform.position.x, powerup.transform.position.y + 0.025f, powerup.transform.position.z);
        else
        {
            sendUpwards = false;
            if(!reward.powerup)
            {
                Destroy(powerup);
                //powerup = null;
            }
        }
    }
    
    public override bool CanInteract()
    {
        return true;
    }
    
    public override void HandleCollision()
    {
        //Checks if the block is already empty
        if (meshRenderer.material.name.Equals("Empty Brick (Instance)")) return;

        coinsLeft--;
        if(coinsLeft < 1)
            //Sets the material of the object to the empty one
            meshRenderer.material = empty;
        //Makes the brick move up and down
        HandleMovement();

        StartCoroutine(HandleSpawn());
    }

    IEnumerator HandleSpawn()
    {
        yield return new WaitForSeconds(0.2f);

        powerup = Instantiate(reward.prefab, transform.position, Quaternion.identity);

        if (reward.coin)
            gameManager.IncreaseCoins(1);

        soundPlayer.PlaySound(reward.sound);

        sendUpwards = true;
    }
}
