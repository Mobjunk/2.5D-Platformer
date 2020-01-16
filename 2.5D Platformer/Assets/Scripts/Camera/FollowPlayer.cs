using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform target;

    void Update()
    {
        if(BasicCutscene.instance.finishedCutscene)
            //Makes the camera follow the player
            transform.position = new Vector3(target.position.x, target.position.y + 2, transform.position.z);
    }
}
