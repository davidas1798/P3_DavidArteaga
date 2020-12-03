using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public delegate void delegateHandler();
    public event delegateHandler shot;
    public event delegateHandler hitB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
            if (shot != null)
                shot();
    }

    public void BCollision()
    {
        hitB();
    }

   
}
