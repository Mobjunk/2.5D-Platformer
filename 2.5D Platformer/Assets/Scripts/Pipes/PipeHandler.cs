using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeHandler : MonoBehaviour
{
    public static PipeHandler instance;

    [SerializeField] Transform[] positions;
    [HideInInspector] public bool canUseThePipe;
    [HideInInspector] public bool usingThePipe;
    [SerializeField] KeyCode keyUsed;

    void Start()
    {
        instance = this;
    }
    
    void Update()
    {
        canUseThePipe = PipeDetection.instance.insideThePipe;
        HandlePipeDown();
        if(usingThePipe)
        {
            GameObject player = GameObject.Find("Player");
            Transform transform = player.GetComponent<Transform>();
            Rigidbody rigidbody = player.GetComponent<Rigidbody>();
            BoxCollider boxCollider = player.GetComponent<BoxCollider>();

            rigidbody.velocity = Vector3.zero;
            rigidbody.useGravity = false;
            boxCollider.isTrigger = true;

            if(keyUsed == KeyCode.S)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.015f, transform.position.z);
                if(transform.position.y < positions[1].position.y)
                    Reset(rigidbody, boxCollider);
            }
            else if (keyUsed == KeyCode.W)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.015f, transform.position.z);
                if (transform.position.y > positions[0].position.y)
                    Reset(rigidbody, boxCollider);
            }
        }
        //print($"canUseThePipe: {canUseThePipe}");
    }

    private void HandlePipeDown()
    {
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W)) && canUseThePipe && !usingThePipe)
        {
            keyUsed = Input.GetKeyDown(KeyCode.S) ? KeyCode.S : KeyCode.W;
            usingThePipe = true;
            SoundPlayer.instance.PlaySound(Sounds.PIPE_TRAVEL);
        }
    }

    private void Reset(Rigidbody rigidbody, BoxCollider boxCollider)
    {
        usingThePipe = false;
        rigidbody.useGravity = true;
        boxCollider.isTrigger = false;
        rigidbody.velocity = Vector3.zero;
        keyUsed = KeyCode.None;
    }

}
