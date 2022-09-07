using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private List<CheckStart> listController = new List<CheckStart>();
    private List<bool> playerReady = new List<bool>();
    [SerializeField] private List<GameObject> playerReadyVisu = new List<GameObject>();
    [SerializeField] private Animator fadeOut;

    private void Start()
    {
        for (int i = 0; i < listController.Count; i++)
        {
            listController[i].setManager(this);
            playerReady.Add(false);
        }
    }

    public void setPlayerReady(bool playerOne)
    {
        if (playerOne) playerReady[0] = true;
        else playerReady[1] = true;

        
        showController();
    }

    private void showController()
    {
        var number = 0;
        if (playerReady[0])
        {
            playerReadyVisu[0].SetActive(true);
            number++;
        }

        if (playerReady[1])
        {
            playerReadyVisu[1].SetActive(true);
            number++;
        }

        if (number == 2) StartCoroutine(LoadLaScene());
    }

    IEnumerator LoadLaScene()
    {
        fadeOut.Play("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Luca/SceneLuca", LoadSceneMode.Single);
    }
}
