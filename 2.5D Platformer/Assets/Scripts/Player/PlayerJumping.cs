using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJumping : MonoBehaviour
{
    [SerializeField] float jumpForce = 10;

    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleJumping();
    }

    public void HandleJumping()
    {
        if (PipeHandler.instance != null && PipeHandler.instance.usingThePipe) return;

        bool pressingJumping = Input.GetKeyDown(KeyCode.Space);
        //Checks if the player is grounded or not and pressed the space button
        if (GroundDetection.instance.isGrounded && pressingJumping)
        {
            //Handles adding force to the player in an upwards direction
            rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);

            SoundPlayer.instance.PlaySound(Sounds.JUMPING);
        }
    }
}
