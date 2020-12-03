using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public KeyScript getKeyHandler;
    
    // Start is called before the first frame update
    void Start()
    {
        getKeyHandler = GameObject.FindWithTag("Key").GetComponent<KeyScript>();
        getKeyHandler.playerGetKey += RemoveObstacle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RemoveObstacle()
    { 
        Destroy(gameObject);
    }
}
