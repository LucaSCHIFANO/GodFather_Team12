using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private List<CheckStart> listController = new List<CheckStart>();
    private List<bool> playerReady = new List<bool>();
    [SerializeField] private List<GameObject> playerReadyVisu = new List<GameObject>();

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
        
        if(playerReady[0]) playerReadyVisu[0].SetActive(true);
        if (playerReady[1]) playerReadyVisu[1].SetActive(true);
    }
}
