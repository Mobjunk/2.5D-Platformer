using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeDetection : MonoBehaviour
{
    public static PipeDetection instance;

    public bool insideThePipe = false;

    void Start()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Checks if the player enters the trigger
        if (other.tag.Equals("PipeTop"))
            Switch();
    }

    private void OnTriggerExit(Collider other)
    {
        //Checks if the player exits the trigger
        if (other.tag.Equals("PipeTop"))
            Switch();
    }

    private void Switch()
    {
        //Sets the boolean towards the other so true = false and false = true
        insideThePipe = !insideThePipe;
    }
}
