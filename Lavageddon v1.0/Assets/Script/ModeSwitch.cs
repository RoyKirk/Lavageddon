using UnityEngine;
using System.Collections;

public class ModeSwitch : MonoBehaviour {

    public bool construction = true;

    //test having an array of bools that only change this bool if all others have been changed. maybe a function with a playerID, with the bool of the spawn block.

    public bool []ConstructionReady;

    public Continue contScript;
    int playerCount;

    float countdownMAX = 3.0f;
    public float countdown;

    public bool CDhappening = false;

    void Start()
    {
        countdown = countdownMAX;
        playerCount = GameObject.FindGameObjectsWithTag("MainCamera").Length;
        //Debug.Log(playerCount);
        ConstructionReady = new bool[4];
    }

    void Update()
    {
        int counter = 0;//count how many people are ready
        for(int i = 0; i < 4; i++)
        {
            if(ConstructionReady[i] == true)
            {
                counter++;
            }
        }

        //make a counter so it starts a X second timer before it actually changes to the next phase.
        //if everyone in the game is ready then start the combat phase
        if(counter == playerCount && countdown >= -0.1)
        {
            CDhappening = true;
            countdown -= Time.deltaTime;
        }
        if(countdown <= 0)
        {
            CDhappening = false;
            construction = false;
        }
    }

    public void setBool(int playerID, bool ready)
    {
        ConstructionReady[playerID] = ready;
    }


}
