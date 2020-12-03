using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBEvent : MonoBehaviour

{
    public SceneController sceneController;
    private float randomDecrement;

    // Start is called before the first frame update
    void Start()
    {
        sceneController = GameObject.FindWithTag("MainCamera").GetComponent<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") ;
            sceneController.hitB += Transformation;
    }

    void Transformation()
    {
        GetComponent<Renderer>().material.color = new Color(Random.Range(0F, 1F), Random.Range(0, 1F), Random.Range(0, 1F));
        randomDecrement = Random.Range(0F, 0.1F);
        GetComponent<Transform>().localScale -= new Vector3(randomDecrement, randomDecrement, randomDecrement);
    }
}
