using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : ObjectMovement
{
    GameManager gameManager => GameManager.instance;

    [SerializeField] Enemy enemy;
    private string name;
    private int score;

    public override void Start()
    {
        base.Start();
        this.name = enemy.name;
        this.score = enemy.score;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            
            if (collision.contacts.Length > 0)
            {
                ContactPoint contact = collision.contacts[0];
                if (Vector3.Dot(contact.normal, Vector3.down) > 0.5f)
                {
                    gameManager.IncreaseScore(score);
                    Destroy(gameObject);
                }
                else
                    gameManager.HandleDeath();
            }
        }
        else if (!collision.gameObject.tag.Equals("NoCollision"))
            direction = direction == 1 ? -1 : 1;
    }
}
