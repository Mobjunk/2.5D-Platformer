using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCutscene : MonoBehaviour
{
    public static BasicCutscene instance;

    [SerializeField] private Transform startPosition, endPosition;
    public bool finishedCutscene = false;
    
    void Start()
    {
        instance = this;
        startPosition = transform;
    }

    void Update()
    {
        bool pressingSpace = Input.GetKeyDown(KeyCode.Space);
        if (!finishedCutscene && transform.position.x < endPosition.position.x && !pressingSpace)
            transform.position = new Vector3(transform.position.x + 0.05f, transform.position.y, transform.position.z);
        else if (!finishedCutscene && (transform.position.x >= endPosition.position.x || pressingSpace))
        {
            InformationPanel.instance.DisplayPanel();
            transform.position = startPosition.position;
            finishedCutscene = true;
        }
    }
}
