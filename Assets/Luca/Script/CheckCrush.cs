using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCrush : MonoBehaviour
{
    [SerializeField] private PlayerController pc;
    [SerializeField] private Mort mort;
    private void OnTriggerEnter2D(Collider2D col){

        Debug.Log(col.gameObject.tag);
        
        if (col.gameObject.tag == "BreakableWall" || col.gameObject.tag == "Ground")
        {
            if(pc.onGround) mort.Death();
        }
    }
}
