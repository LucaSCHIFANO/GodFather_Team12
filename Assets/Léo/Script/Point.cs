using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Point : MonoBehaviour
{
    public TextMeshProUGUI PointText; 
    public int coin; 

    public GameObject effect;

    void Start()
    {
        PointText.text = coin.ToString();
    }
    void OnTriggerEnter2D(Collider2D truc)
    {
        if (truc.tag == "Coin")
        {
            coin++;
            PointText.text = coin.ToString();
            Destroy(truc.gameObject);
            Instantiate(effect, truc.transform.position, Quaternion.identity);
        }
    }

}
