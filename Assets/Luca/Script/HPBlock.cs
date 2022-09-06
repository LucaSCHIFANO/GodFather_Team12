using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBlock : HP
{
    override protected void removeHP(float hpToRemove)
    {
        curretnHP -= hpToRemove;
        if (curretnHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
