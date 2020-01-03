using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveCamera : MonoBehaviour
{
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
