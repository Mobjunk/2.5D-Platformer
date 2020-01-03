using UnityEngine;

public class PickupMushroom : ObjectMovement
{
    GameManager gameManager => GameManager.instance;

    public override void Start()
    {
        base.Start();
        direction = 1;
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            GameObject player = GameObject.Find("Player");
            Transform transform = player.GetComponent<Transform>();

            transform.position = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
            transform.localScale = new Vector3(1, 1, 1);

            PlayerInformation.instance.playerState = PlayerState.BIG;
            SoundPlayer.instance.PlaySound(Sounds.PICKUP_POWERUP);

            gameManager.IncreaseScore(1000);

            Destroy(gameObject);
        }
        else if (!collision.gameObject.tag.Equals("NoCollision"))
            direction = direction == 1 ? -1 : 1;
    }
}
