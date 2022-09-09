using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectsCollision : MonoBehaviour
{
    [SerializeField] private GameObject deathObject;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider){

        
        if (collider.gameObject.tag == "Player")
        {

            if (deathObject != null) Instantiate(deathObject, transform.position, transform.rotation);
            
            collider.GetComponent<PlayerController>().BounceBack();
            Destroy(gameObject);
            
        }
    }
}
