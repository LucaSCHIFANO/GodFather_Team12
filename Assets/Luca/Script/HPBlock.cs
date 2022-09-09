using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBlock : HP
{
    [SerializeField] private GameObject deathObject;
    
    override public void removeHP(float hpToRemove)
    {
        curretnHP -= hpToRemove;
        if (curretnHP <= 0)
        {
            if (deathObject != null) Instantiate(deathObject, transform.position, transform.rotation);
            
            Destroy(gameObject);
        }
    }
}
