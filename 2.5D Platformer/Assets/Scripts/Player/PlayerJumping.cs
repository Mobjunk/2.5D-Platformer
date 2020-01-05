using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJumping : MonoBehaviour
{
    /// <summary>
    /// The jump force of the player
    /// </summary>
    [SerializeField] float jumpForce = 10;

    /// <summary>
    /// The rigidbody attached to the player
    /// </summary>
    Rigidbody rigidbody;

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
            Jump(jumpForce);
    }

    public void Jump(float force)
    {
        //Handles adding force to the player in an upwards direction
        rigidbody.AddForce(new Vector3(0, force, 0), ForceMode.Impulse);

        SoundPlayer.instance.PlaySound(Sounds.JUMPING);
    }
}
