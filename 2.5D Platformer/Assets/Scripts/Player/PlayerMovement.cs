using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [HideInInspector] public bool disableMovement = false;

    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!GameManager.instance.gameStarted || disableMovement) return;
        Move();
    }

    private void Move()
    {
        if (PipeHandler.instance != null && PipeHandler.instance.usingThePipe || TeleportPipe.instance != null && TeleportPipe.instance.usingThePipe) return;

        rigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed, rigidbody.velocity.y);
    }
}
