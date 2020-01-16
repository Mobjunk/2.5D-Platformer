using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectMovement : MonoBehaviour
{
    /// <summary>
    /// Rigidbody attached to the 'object'
    /// </summary>
    private Rigidbody rigidbody;

    /// <summary>
    /// The movement speed of this 'object'
    /// </summary>
    [HideInInspector]
    public float movementSpeed = 3.5f;

    /// <summary>
    /// The direction the 'object' is moving in
    /// </summary>
    //[HideInInspector]
    public int direction = -1;

    /// <summary>
    /// Checks if the movement has to been paused
    /// </summary>
    public bool pauseMovement, defaultMovement;

    public virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        defaultMovement = pauseMovement;
    }

    public virtual void Update()
    {
        if (!GameManager.instance.gameStarted) return;
        
        //Checks if the 'object' is allowed to move
        if (!pauseMovement)
            rigidbody.velocity = new Vector2(direction * movementSpeed, rigidbody.velocity.y);
        //Reset the velocity if its not allowed to move
        else rigidbody.velocity = Vector3.zero;
    }
}
