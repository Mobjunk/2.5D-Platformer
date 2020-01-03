using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public string name;
    public int score;
}
