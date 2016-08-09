﻿using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DynamicPlayerCount : MonoBehaviour {

    public GameObject[] players = new GameObject[4];
    public bool[] ready = new bool[4];
    public GameObject[] readyImage;

    Object[] duplicates = new Object[2];
    static DynamicPlayerCount Original;


    int readyCount = 0;
    int playersDead = 0;


    void Awake()
    {


        if (Original)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Original = this;
        }
    }

    void OnLevelWasLoaded(int level)
    {
        duplicates = FindObjectsOfType(GetType());
        if (duplicates.Length > 1)
        {
            Destroy(duplicates[0]);
        }
        if (level > 0)
        {
            for (int i = 0; i < 4; i++)
            {
                if (ready[i])
                {
                    readyCount++;
                    //Instantiate(players[i]);
                }
            }

            InstantiatePlayers(readyCount);
            readyCount = 0;
        }
    }

    public Camera p1C;
    public int spotCount = 0;
    Rect[] spot2 = { new Rect(0, 0.5f, 1, 0.5f), new Rect(0, 0, 1, 0.5f) };
    Rect[] spot4 = { new Rect(0, 0.5f, 0.5f, 0.5f), new Rect(0.5f, 0.5f, 0.5f, 0.5f), new Rect(0, 0, 0.5f, 0.5f), new Rect(0.5f, 0, 0.5f, 0.5f)};

    //once player ready count has been determined, set up each camera and player
    void InstantiatePlayers(int readyCount)
    {
        for(int i = 0; i < 4; i++)
        {
            if(ready[i])
            {
                if (readyCount == 1)
                {
                    p1C = players[i].GetComponent<Camera>();
                    p1C.rect = new Rect(0, 0, 1, 1);
                    Instantiate(players[i]);//set up player 
                }
                else if (readyCount == 2)
                {
                    p1C = players[i].GetComponent<Camera>();
                    p1C.rect = spot2[spotCount++];
                    Instantiate(players[i]);// set up player
                }
                else if (readyCount > 2)
                {
                    p1C = players[i].GetComponent<Camera>();
                    p1C.rect = spot4[spotCount++];
                    Instantiate(players[i]);
                }
            }
        }
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        readyImage = GameObject.FindGameObjectsWithTag("PlayerReady");

        for (int i = 0; i < readyImage.Length; i++)
        {
            if (readyImage[i] != null)
            {
                if (ready[i])
                {
                    //readyImage[i].SetActive(true);
                    readyImage[i].GetComponent<Text>().text = ("Player " + (i + 1) + " Ready");
                    //readyCount++;
                    //Instantiate(players[i]);
                }
                else
                {
                    readyImage[i].GetComponent<Text>().text = "";
                    // readyImage[i].SetActive(false);
                }
            }
        }
        
    }

    public void playerDeath()
    {
        playersDead++;
        int playersIn = 0;
        for(int i = 0; i<ready.Length;i++)
        {
            if(ready[i])
            {
                playersIn++;
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (playersIn <= 1)
            {
                if (playersIn - playersDead <= 0)
                {
                    playersDead = 0;
                    SceneManager.LoadScene(0);
                }
            }
            if (playersIn > 1)
            {
                if (playersIn - playersDead <= 1)
                {
                    playersDead = 0;
                    SceneManager.LoadScene(0);
                }
            }
        }
    }

}