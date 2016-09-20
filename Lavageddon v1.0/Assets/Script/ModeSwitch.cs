using UnityEngine;
using System.Collections;

public class ModeSwitch : MonoBehaviour {

    public bool construction = true;

    //test having an array of bools that only change this bool if all others have been changed. maybe a function with a playerID, with the bool of the spawn block.

    bool []ConstructionReady;

    void Update()
    {
        //if(ConstructionReady[0])
        //{
        //    construction = false;
        //}
    }

    public void setBool(int playerID, bool SpawnBlock)
    {
        //switch(playerID)
        //{
        //    case 0:
        //        if(SpawnBlock)
        //        {
        //            ConstructionReady[]
        //        }
        //        break;
        //    case 1:
        //        break;
        //    case 2:
        //        break;
        //    case 3:
        //        break;
        //}
        ConstructionReady[playerID] = SpawnBlock; 
    }


}
