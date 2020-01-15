using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            GameManager.instance.HandleDeath();
        }
        else if (other.tag.Equals("Enemy"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
