using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col){

        Debug.Log(col.gameObject.tag);
        
        if (col.gameObject.tag == "BreakableWall")
        {
            col.gameObject.GetComponent<HPBlock>().removeHP(10f); 
        }
    }
}
