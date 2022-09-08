using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Point : MonoBehaviour
{
    public TextMeshProUGUI PointText;
    public int score;

    public GameObject effect;

    void Start()
    {
        PointText.text = score.ToString();
        StartCoroutine(Score());
    }
    private void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D truc)
    {
        if (truc.tag == "Coin")
        {
            score++;
            PointText.text = score.ToString();
            Destroy(truc.gameObject);
            Instantiate(effect, truc.transform.position, Quaternion.identity);
        }
    }
    IEnumerator Score()
    {
        yield return new WaitForSeconds(1f);
        score += 100;
        PointText.text = score.ToString();
        StartCoroutine(Score());
    }
    }
