using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEvent : MonoBehaviour
{
    public SwitchController switchController;
    
    // Start is called before the first frame update
    void Start()
    {
        switchController = GameObject.FindWithTag("Switch").GetComponent<SwitchController>();
        switchController.switchPressed += TurnLight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TurnLight()
    {
        gameObject.GetComponent<Light>().enabled = !gameObject.GetComponent<Light>().enabled;
    }
}
