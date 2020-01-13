using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New reward", menuName = "Reward")]
public class Reward : ScriptableObject
{
    public GameObject prefab;
    public bool powerup;
    public Sounds sound;
}
