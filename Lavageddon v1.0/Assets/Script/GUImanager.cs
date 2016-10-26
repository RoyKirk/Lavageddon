using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUImanager : MonoBehaviour
{
    //get component references
    public managerscript MS;
    public ModeSwitch modeSwitch;

    //text box ref
    //Construction UI elements
    public Text spawnblockWarning;
    public Text readyText;
    public Text pressToReady;
    public Text testBoat;

    //Crosshairs constr

    
    //Combat UI elements;
    public Text playerinvincible;
    
    //other
    public bool spawnPosGood;
    int player;
    public bool readystate;
    public bool Hitplayer;

    public bool turnOffUI = false;
    /* UI things that still need to be done:
     *  - LB and RB buttons appearing if they havnt pressed them after 30 seconds
     *  - Replace the spawn block to a new placed one when confirmed.
     *  
     *  - Crosshair's have to be managed to whichever weapon you are using 
     * */


    // Use this for initialization
    void Awake ()
    {
        MS = GetComponent<managerscript>();
        modeSwitch = GameObject.Find("Controller").GetComponent<ModeSwitch>();
	}

    void Start()
    {
       player = MS.player;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //player is hitting someone with lazer
        if(Hitplayer)
        {
            playerinvincible.text = "Players are Bullet Proof!!";
        }

        //PLAYER HAS PLACED SPAWN BLOCK
        if (MS.spawnblock && spawnPosGood)
        {
            spawnblockWarning.text = "";
        }
        else
        {
            modeSwitch.setBool(player, false);
            readystate = false;
        }

        //PLAYER IS NOT READY
        if (readystate == false)
        {
            readyText.text = "";
        }
        else
        {
            readyText.text = "Ready!";
        }

        //PLAYER HAS USED ALL BLOCKS
        if (MS.numberOfBlocks == MS.maxNumberOfBlocks && MS.spawnblock == false)
        {
            pressToReady.text = "Spawn block needed to Ready";
        }
        else if (MS.numberOfBlocks == MS.maxNumberOfBlocks && readystate == false)
        {
            pressToReady.text = "Press Back to Ready";
        }
        else
        {
            pressToReady.text = "";
        }

        if (MS.resetboatcheck)
        {
            testBoat.text = "Press Left Thumbstick to reset boat!";
        }
        else if (MS.numberOfBlocks == 0 && MS.spawnblock == false)
        {
            testBoat.text = "Left Thumbstick to spawn a block";
        }
        else if (MS.numberOfBlocks >= MS.maxNumberOfBlocks / 2 && MS.theyHaveTestedBoat == false)
        {
            testBoat.text = "Right Thumbstick to test your boat";
        }
        else
        {
            testBoat.text = "";
        }

        if (spawnPosGood == false)
        {
            spawnblockWarning.text = "something is obstructing the spawn block!";
        }

        if(MS.testNewSpawnBlock)
        {
            spawnblockWarning.text = "Press Right trigger to replace Spawn block";
        }

        //print the countdown timer to start combat phase.
        if (modeSwitch.CDhappening)
        {
            readyText.text = ((int)modeSwitch.countdown + 1).ToString();
        }
    }

    public void TurnOffConstructionUI()
    {
        turnOffUI = true;
        spawnblockWarning.text = "";
        readyText.text = "";
        pressToReady.text = "";
        testBoat.text = "";
    }
}
