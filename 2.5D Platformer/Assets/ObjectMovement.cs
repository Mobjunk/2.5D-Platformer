using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectMovement : MonoBehaviour
{
    private Rigidbody rigidbody;
    private float movementSpeed = 3.5f;
    [HideInInspector]
    public int direction = -1;

    public virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public virtual void Update()
    {
        rigidbody.velocity = new Vector2(direction * movementSpeed, rigidbody.velocity.y);
    }
}
