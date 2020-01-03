﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;

    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (PipeHandler.instance != null && PipeHandler.instance.usingThePipe) return;

        rigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed, rigidbody.velocity.y);
    }
}
