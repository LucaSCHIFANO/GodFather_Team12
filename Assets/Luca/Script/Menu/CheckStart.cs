using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEditor;

public class CheckStart : MonoBehaviour
{
    [SerializeField] private int playerID;
    private  Rewired.Player player;

    private MenuManager mm;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerID);
    }

    public void setManager(MenuManager leMenu)
    {
        mm = leMenu;
    }

    private void Update()
    {
        if (player.GetButtonDown("Start"))
        {
            mm.setPlayerReady(playerID == 0);
        }
    }
}
