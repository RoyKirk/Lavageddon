using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using XInputDotNetPure;
using UnityEngine.UI;

public class Continue : MonoBehaviour {

    public Button backbtn;
    public Button continuebtn;
    public Button resetbtn;

    public DynamicPlayerCount DPC;
    public GameObject[] players;
    public GameObject[] playerPrefabs;
    public ModeSwitch MS;
    public Canvas gameover;
    //GameObject[] BoatParents;

    public int select = 0;

    // Use this for initialization
    void Start()
    {
        //players = new GameObject[4];
        DPC = FindObjectOfType<DynamicPlayerCount>();
    }

    // Update is called once per frame
    void Update()
    {
        

        for (int i = 0; i < 4; i++)
        {
            //buttons to toggle menu
            if (Controller.prevState[i].Buttons.Start == ButtonState.Released && Controller.state[i].Buttons.Start == ButtonState.Pressed)
            {
                backbtn.gameObject.SetActive(!backbtn.gameObject.activeSelf);
                continuebtn.gameObject.SetActive(!continuebtn.gameObject.activeSelf);
                resetbtn.gameObject.SetActive(!resetbtn.gameObject.activeSelf);
            }
            if(Controller.prevState[i].Buttons.B == ButtonState.Released && Controller.state[i].Buttons.B == ButtonState.Pressed)
            {
                backbtn.gameObject.SetActive(false);
                continuebtn.gameObject.SetActive(false);
                resetbtn.gameObject.SetActive(false);
            }


            //NAVIGATING MENUS WITH BUTTONS
            if(Controller.state[i].DPad.Up == ButtonState.Pressed && Controller.prevState[i].DPad.Up == ButtonState.Released)
            {
                select--;
            }
            else if(Controller.state[i].DPad.Down == ButtonState.Pressed && Controller.prevState[i].DPad.Down == ButtonState.Released)
            {
                select++;
            }
        }
        if(select < 0)
        {
            select = 2;
        }
        else if(select > 2)
        {
            select = 0;
        }

        switch(select)
        {
            case 0:
                backbtn.Select();
                break;
            case 1:
                continuebtn.Select();
                break;
            case 2:
                resetbtn.Select();
                break;
        }
    }

    public void ContinueGame()
    {
        players = GameObject.FindGameObjectsWithTag("MainCamera");
        //SceneManager.LoadScene(1);
        //for(int i = 0; i < players.Length; i++)
        //{
        //    Instantiate(playerPrefabs[i]);
        //}
        for(int i = 0; i < players.Length; i++)
        {
            Destroy(players[i]);
        }
        for (int i = 0; i < players.Length; i++)
        {
            GameObject player = Instantiate(playerPrefabs[i]);
        }
        MS.construction = true;
        gameover.enabled = false;
        backbtn.gameObject.SetActive(false);
        continuebtn.gameObject.SetActive(false);
        resetbtn.gameObject.SetActive(false);
        
        //need to reset boats to their floating state
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void Back()
    {
        backbtn.gameObject.SetActive(false);
        continuebtn.gameObject.SetActive(false);
        resetbtn.gameObject.SetActive(false);
    }
}
