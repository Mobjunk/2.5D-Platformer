using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPipe : MonoBehaviour
{
    public static TeleportPipe instance;
    
    PipeHandler pipeHandler => PipeHandler.instance;

    [SerializeField] private Transform walkingPosition;
    [SerializeField] private Transform teleportPosition;
    [SerializeField] private Transform endPosition;
    [HideInInspector] public bool canUseThePipe, usingThePipe, finishedWalking = false, finishedTeleport;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        canUseThePipe = PipeDetection.instance.insideThePipe;
        HandlePipe();
        if (usingThePipe)
        {
            GameObject player = GameObject.Find("Player");
            Transform transform = player.GetComponent<Transform>();
            Rigidbody rigidbody = player.GetComponent<Rigidbody>();
            BoxCollider boxCollider = player.GetComponent<BoxCollider>();

            rigidbody.velocity = Vector3.zero;
            rigidbody.useGravity = false;
            boxCollider.isTrigger = true;
            
            if(transform.position.x < walkingPosition.position.x && !finishedWalking)
                transform.position = new Vector3(transform.position.x + 0.015f, transform.position.y, transform.position.z);
            else finishedWalking = true;

            if (finishedWalking && !finishedTeleport)
            {
                transform.position = teleportPosition.position;
                finishedTeleport = true;
            } else if (finishedTeleport)
            {
                if(transform.position.y < endPosition.position.y)
                    transform.position = new Vector3(transform.position.x, transform.position.y + 0.015f, transform.position.z);
                else
                {
                    usingThePipe = false;
                    rigidbody.useGravity = true;
                    boxCollider.isTrigger = false;
                    rigidbody.velocity = Vector3.zero;
                }
            }
        }
    }
    
    private void HandlePipe()
    {
        if (Input.GetKeyDown(KeyCode.D) && canUseThePipe && !usingThePipe)
        {
            usingThePipe = true;
            SoundPlayer.instance.PlaySound(Sounds.PIPE_TRAVEL);
        }
    }
}
