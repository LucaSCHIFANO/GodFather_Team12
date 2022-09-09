using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float parallaxEffectMultiplier;
    private float length,currentPos,cameraPosition;
    [SerializeField] private GameObject cam;

    void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        cameraPosition = cam.transform.position.x;
    }

    void Update()
    {
        currentPos = transform.position.x;
        float dist = (1 - parallaxEffectMultiplier)/10;
        transform.position = new Vector3(currentPos - dist,transform.position.y,transform.position.z);
        if (cameraPosition > currentPos + length){
            transform.position += new Vector3(length * 2,0,0);
        }
    }

}
