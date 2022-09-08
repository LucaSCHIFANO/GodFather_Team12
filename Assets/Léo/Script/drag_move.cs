using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag_move : MonoBehaviour
{


    public int speed = 0;
    [SerializeField, Range(0.1f, 50f)] private float pointA = 5f;
    [SerializeField, Range(0.1f, 50f)] private float pointB = 5f;
    [SerializeField, Range(0f, 360f)] private float RotationPath;
    private Vector2 directionAngle;
    private Vector3 point1Position;
    private Vector3 point2Position;
    public bool retour;


    void Start()
    {
        directionAngle = (Vector2)(Quaternion.Euler(0, 0, RotationPath) * Vector2.right);
        point1Position = transform.position + (Vector3)directionAngle * pointA;
        point2Position = transform.position - (Vector3)directionAngle * pointB;
    }


    void Update()
    {
        if (!retour)
        {
            transform.position = Vector2.MoveTowards(transform.position, point1Position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, point1Position) < 0.05f)
            {
                retour = true;
            }
        }
        if (retour)
        {
            transform.position = Vector2.MoveTowards(transform.position, point2Position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, point2Position) < 0.05f)
            {
                retour = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D truc)
    {
        if (truc.gameObject.tag == "Player")
        {
            truc.transform.parent = transform;
        }
    }
    void OnCollisionExit2D(Collision2D truc)
    {
        if (truc.gameObject.tag == "Player")
        {
            truc.transform.parent = null;
        }
    }

    void OnDrawGizmos()
    {
        if (!Application.IsPlaying(gameObject))
        {
            directionAngle = (Vector2)(Quaternion.Euler(0, 0, RotationPath) * Vector2.right);
            point1Position = transform.position + (Vector3)directionAngle * pointA;
            point2Position = transform.position - (Vector3)directionAngle * pointB;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(point1Position, 0.2f);
        Gizmos.DrawSphere(point2Position, 0.2f);
        Gizmos.DrawLine(point1Position, point2Position);
    }
}