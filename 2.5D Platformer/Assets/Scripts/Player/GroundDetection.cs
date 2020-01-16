using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public static GroundDetection instance;

    /// <summary>
    /// The boolean to check if the player is on the ground
    /// </summary>
    [HideInInspector] public bool isGrounded;
    [SerializeField] float distanceToFloor = 0.5f, distToGround;
    private BoxCollider collider;

    void Start()
    {
        collider = GetComponent<BoxCollider>();
        instance = this;
        distToGround = collider.bounds.extents.y;
    }

    void Update()
    {
        //Checks if the player is grounded or not
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

}
