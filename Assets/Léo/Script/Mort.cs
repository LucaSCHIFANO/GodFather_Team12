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
    }
    void OnTriggerEnter2D(Collider2D truc)
    {
        if (truc.tag == "Player") Death();
    }

    public void Death()
    {
        DeathScreen.SetActive(true);
        Time.timeScale = 0;
    }

}