using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private CheckStart controller;

    [SerializeField] private Animator fadeOut;


    private void Start()
    {
        controller.setManager(this);
    }

    public void setPlayerReady(bool playerOne)
    {
        StartCoroutine(LoadLaScene());
    }
    
    IEnumerator LoadLaScene()
    {
        fadeOut.Play("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Luca/SceneLuca", LoadSceneMode.Single);
    }
}
