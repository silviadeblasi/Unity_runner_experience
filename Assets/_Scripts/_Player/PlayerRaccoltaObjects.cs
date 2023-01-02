using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaccoltaObjects : MonoBehaviour
{

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider){

        if (collider.gameObject.layer == 7){

            Debug.Log("Player has entered the trigger");
            Destroy (collider.gameObject);

        }

    }
        
}
