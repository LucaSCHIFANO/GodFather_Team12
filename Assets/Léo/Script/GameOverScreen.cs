using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject DeathScreen;
    public bool Chrono;

    public SoundTransmitter st;
    private bool one;
    
    public PlayerController pc;

    public void Start()
    {
        Chrono = false;
    }
    public void Update()
    {
        if (Time.timeScale == 0)
        {
            Chrono = true;
            if (!one)
            {
                pc.isDamaged = false;
                pc.isDashing = false;
                one = true;
                st.Stop("Main");
                st.Play("GO");
            }
        }
    }

}
