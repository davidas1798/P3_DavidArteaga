using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittingB : MonoBehaviour
{
    public SceneController sceneController;

    // Start is called before the first frame update
    void Start()
    {
        sceneController = GameObject.FindWithTag("MainCamera").GetComponent<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "B")
            sceneController.BCollision();
    }
}
