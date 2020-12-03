using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public delegate void delegateHandler();
    public event delegateHandler switchPressed;
    private GameObject player;
    private Transform playerTransform, selfTransform;
    public float minimumDistanceToActivate = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        selfTransform = GetComponent<Transform>();

        if (Vector3.Distance(playerTransform.position, selfTransform.position) <= minimumDistanceToActivate && Input.GetKeyDown("e"))
            switchPressed();
    }
}
