using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    [SerializeField] protected float maxHP;
    protected float curretnHP;
    virtual protected void Start()
    {
        curretnHP = maxHP;
    }

    virtual public void removeHP(float hpToRemove)
    {
        curretnHP -= hpToRemove;
        if (curretnHP <= 0)
        {
            Debug.Log("I'm Dead...");
            Destroy(gameObject);
        }
    }
}
