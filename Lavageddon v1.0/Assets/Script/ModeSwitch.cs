using UnityEngine;
using System.Collections;

public class ModeSwitch : MonoBehaviour {

    public bool construction = true;

    //test having an array of bools that only change this bool if all others have been changed. maybe a function with a playerID, with the bool of the spawn block.

    public bool []ConstructionReady;

    public Continue contScript;
    int playerCount;
    void Start()
    {
        playerCount = GameObject.FindGameObjectsWithTag("MainCamera").Length;
        Debug.Log(playerCount);
        ConstructionReady = new bool[4];
    }

    void Update()
    {
        //if(ConstructionReady[0])
        //{
        //    construction = false;
        //}
        int counter = 0;
        for(int i = 0; i < 4; i++)
        {
            if(ConstructionReady[i] == true)
            {
                counter++;
            }
        }
        if(counter == playerCount)
        {
            construction = false;
        }
    }

    public void setBool(int playerID, bool SpawnBlock)
    {
        ConstructionReady[playerID] = true;
        //switch (playerID)
        //{
        //    case 0:
        //        ConstructionReady[playerID] = true;
        //        //construction = false;
        //        break;
        //    case 1:
        //        break;
        //    case 2:
        //        break;
        //    case 3:
        //        break;
        //}
        //ConstructionReady[playerID] = SpawnBlock; 
    }


}
