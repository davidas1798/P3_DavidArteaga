using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEvent : MonoBehaviour
{
    public int counter = 0;
    public PlayerEventScript collisionEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        collisionEvent = GameObject.FindWithTag("Player").GetComponent<PlayerEventScript>();
        collisionEvent.aEvent += IncCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IncCount()
    {
        counter++;
        Debug.Log(counter);
    }
}
