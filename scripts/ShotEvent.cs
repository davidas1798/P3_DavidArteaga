using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEvent : MonoBehaviour
{
    public SceneController sceneController;
    private GameObject player;
    private Transform playerTransform;
    private Transform selfTransform;
    private Rigidbody selfRigidbody;
    public float destroyDistance = 2.0f;
    public float pushDistance = 6.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        sceneController = GameObject.FindWithTag("MainCamera").GetComponent<SceneController>();
        sceneController.shot += PlayerShot;
    }

    // Update is called once per frame
    void PlayerShot()
    {
        player = GameObject.FindWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        selfTransform = GetComponent<Transform>();

        Vector3 pushDirection = selfTransform.position - playerTransform.position;

        float distance = Vector3.Distance(selfTransform.position, playerTransform.position);
        if (distance <= destroyDistance)
        {
            Destroy(gameObject);
            sceneController.shot -= PlayerShot;
        }

        else if (distance <= pushDistance)
        {
            selfRigidbody = GetComponent<Rigidbody>();
            selfRigidbody.AddForce(pushDirection, ForceMode.Impulse);
        }
    }
}
