using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject DeathScreen;
    public bool Chrono;

    public void Start()
    {
        Chrono = false;
    }
    public void Update()
    {
        if (Time.timeScale == 0)
        {
            Chrono = true;
        }
    }

}
