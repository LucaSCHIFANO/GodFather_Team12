using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBody : MonoBehaviour
{
    [SerializeField] private float id;
    private Animator anim;
    private void Awake()
    {
        //anim.enabled = false;
        anim = GetComponent<Animator>();
        StartCoroutine(StartAnim());
    }

    IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(0.2f * id);
        anim.enabled = true;
    }
    
}
