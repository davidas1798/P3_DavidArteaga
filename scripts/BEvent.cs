using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEvent : MonoBehaviour
{
    public PlayerEventScript collisionEvent = null;
    
    // Start is called before the first frame update
    void Start()
    {
        collisionEvent = GameObject.FindWithTag("Player").GetComponent<PlayerEventScript>();
        collisionEvent.bEvent += PrintMessage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PrintMessage()
    {
        Debug.Log("Colisión");
    }
}
