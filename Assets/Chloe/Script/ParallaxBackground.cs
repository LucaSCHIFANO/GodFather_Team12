using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float parallaxEffectMultiplier;
    private float length, startPos,currentPos;
    [SerializeField] private GameObject cam;
    private SpriteRenderer spriteRenderer;
    private float screenWidth;
    private float spriteRightBound;

    void Start()
    {
        spriteRenderer =  GetComponent<SpriteRenderer>();
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        startPos = transform.position.x;
    }

    void Update()
    {
        float temp = cam.transform.position.x * (1 - parallaxEffectMultiplier);
        currentPos = transform.position.x;
        transform.position = new Vector3(currentPos - parallaxEffectMultiplier, transform.position.y, transform.position.z);

        if (temp > currentPos + length){
            currentPos += length;
        }
    }
}
