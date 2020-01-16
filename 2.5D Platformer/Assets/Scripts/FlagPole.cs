using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FlagPole : MonoBehaviour
{
    GameManager gameManager => GameManager.instance;
    SoundPlayer soundPlayer => SoundPlayer.instance;
    
    [SerializeField] private Transform flag, flagEndPosition, playerEndPosition;
    [HideInInspector] public bool startEndScene = false, flagReachedEndpoint = false, finishedCounting = false, playingStageSound = false, walkedIntoTheCastle = false, fireworksFinished = false;
    private GameObject player;
    
    // Update is called once per frame
    void Update()
    {
        if(player == null) player = gameManager.playerObject;
        
        if (startEndScene)
        {
            if (flag.position.y > flagEndPosition.position.y)
                flag.position = new Vector3(flag.position.x, flag.position.y - 0.025f, flag.position.z);
            else flagReachedEndpoint = true;
        }

        if (flagReachedEndpoint && gameManager.mapTimer > 0)
        {
            gameManager.UpdateTime(1);
            gameManager.IncreaseScore(100);
        }
        else if (flagReachedEndpoint && gameManager.mapTimer <= 0) finishedCounting = true;

        if (finishedCounting && player.transform.position.x < playerEndPosition.position.x)
        {
            if (!playingStageSound)
            {
                soundPlayer.PlaySound(Sounds.STAGE_CLEAR);
                playingStageSound = true;
            }
            Rigidbody rigidbody = player.GetComponent<Rigidbody>();
            rigidbody.velocity = new Vector2(2f, rigidbody.velocity.y);
        } else if (finishedCounting && !walkedIntoTheCastle && player.transform.position.x >= playerEndPosition.position.x)
        {
            soundPlayer.PlaySound(Sounds.FIREWORKS);
            walkedIntoTheCastle = true;
            player.SetActive(false);
        }
        if (walkedIntoTheCastle && !soundPlayer.IsPlaying() && !fireworksFinished)
        {
            InformationPanel.instance.ThanksForPlaying();
            fireworksFinished = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            gameManager.levelEnding = true;
            
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponent<PlayerMovement>().disableMovement = true;
            
            startEndScene = true;
        }
    }
}
