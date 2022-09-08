using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mort : MonoBehaviour
{
    public GameObject DeathScreen;
    private bool dcd;

    public void Start()
    {
        DeathScreen.SetActive(false);
        dcd = false;
    }
    void OnTriggerEnter2D(Collider2D truc)
    {
        if (truc.tag == "Player")
        {
            DeathScreen.SetActive(true);
            dcd = true;
            Time.timeScale = 0;
        }
    }
    private void Update()
    {
        if ()
        {

        }
    }

}