using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    private Transform keyTransform;
    private GameObject player;
    public delegate void delegateHandler();
    public event delegateHandler playerGetKey;

    // Start is called before the first frame update
    void Start()
    {
        keyTransform = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        keyTransform.Rotate(0, 0, 3);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerGetKey();
            Destroy(gameObject);
        }
    }


}
