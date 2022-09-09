using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drake : MonoBehaviour
{


    public int speed = 0;
    public Transform point1Position;
    public Transform point2Position;
    public bool retour;




    void Update()
    {
        if (!retour)
        {
            transform.position = Vector2.MoveTowards(transform.position, point1Position.transform.position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, point1Position.transform.position) < 0.05f)
            {
                retour = true;
            }
        }
        if (retour)
        {
            transform.position = Vector2.MoveTowards(transform.position, point2Position.transform.position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, point2Position.transform.position) < 0.05f)
            {
                retour = false;
            }
        }
    }
}