using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mort : MonoBehaviour
{
    public GameObject DeathScreen;
    public bool dcd;
    private bool death;

    public void Start()
    {
        DeathScreen.SetActive(false);
        death = false;
        dcd = false;
    }
    void OnTriggerEnter2D(Collider2D truc)
    {
        if (truc.tag == "Player")
        {
            DeathScreen.SetActive(true);
            death = true;
            Time.timeScale = 0;
        }
    }
    private void Update()
    {
        if (death == true )
        {
            dcd = true;
        }
    }

}