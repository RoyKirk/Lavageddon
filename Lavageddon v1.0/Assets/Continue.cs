using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class Continue : MonoBehaviour {

    public GameObject menu;
    public GameObject resetbtn;

    public DynamicPlayerCount DPC;
    public GameObject[] players;
    public GameObject[] playerPrefabs;
    public ModeSwitch MS;
    public Canvas gameover;
    GameObject[] BoatParents;

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
            if (Controller.prevState[i].Buttons.Start == ButtonState.Released && Controller.state[i].Buttons.Start == ButtonState.Pressed)
            {
                menu.SetActive(!menu.activeSelf);
            }
        }

    }

    public void ContinueGame()
    {
        //SceneManager.LoadScene(1);
        players = GameObject.FindGameObjectsWithTag("MainCamera");

        for(int i = 0; i < players.Length; i++)
        {
            Destroy(players[i]);
        }
        MS.construction = true;
        gameover.enabled = false;
        menu.SetActive(!menu.activeSelf);
        resetbtn.SetActive(false);
        //BoatParents[0] = (GameObject)Resources.Load("player1parent");
        for (int i = 0; i < DPC.ready.Length; i++)
        {
            if(DPC.ready[i] == true)
            {
                Instantiate(playerPrefabs[i]);
                //instantiate boats
            }
        }
        //Instantiate(BoatParents[0]);
    }
}
