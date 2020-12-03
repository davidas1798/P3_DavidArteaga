using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventScript : MonoBehaviour
{
    public delegate void delegateFunction();
    public event delegateFunction aEvent;
    public event delegateFunction bEvent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "A")
            aEvent();
        else if (collision.gameObject.tag == "B")
            bEvent();
    }
}