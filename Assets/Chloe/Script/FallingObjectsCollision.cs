using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectsCollision : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider){

        
        if (collider.gameObject.tag == "Player"){   
            Destroy(gameObject);
            collider.GetComponent<PlayerController>().BounceBack();
        }
    }
}
