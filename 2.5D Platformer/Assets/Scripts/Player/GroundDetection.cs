using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public static GroundDetection instance;

    /// <summary>
    /// The boolean to check if the player is on the ground
    /// </summary>
    public bool isGrounded;
    [SerializeField] float distanceToFloor = 0.5f;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        //print(Physics.Raycast(transform.position, Vector3.down, distanceToFloor));

        //Checks if the player is grounded or not
        isGrounded = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, 1 << LayerMask.NameToLayer("Solid")).Length > 0;
    }
}
