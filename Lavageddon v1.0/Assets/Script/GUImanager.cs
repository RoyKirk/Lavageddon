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

    //size of these must = var of clip size. change SetActive as number changes.
    public GameObject[] CannonAmmoUI;
    public GameObject[] StickyAmmoUI;

    PlayerMovement PM;

    // Use this for initialization
    void Awake ()
    {
        PM = GetComponent<PlayerMovement>();
        MS = GetComponent<managerscript>();
        modeSwitch = GameObject.Find("Controller").GetComponent<ModeSwitch>();
	}

    void Start()
    {
        //CannonAmmoUI = new GameObject[PM.bombClipSize];
        //StickyAmmoUI = new GameObject[PM.stickyClipSize];
       player = MS.player;
        RESET_AMMO_UI();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //need to set up a change of which UI element it is showing on screen, to only show current weapon.
        //for (int i = 0; i < (PM.bombClipSize - PM.bombsLeft); i++)
        //{
        //    Debug.Log("removing a bullet");
        //    CannonAmmoUI[i].SetActive(false);
        //}
        //
        //if (PM.bombsLeft == PM.bombClipSize)//reloaded
        //{
        //    for (int i = 0; i < PM.bombClipSize; i++)
        //    {
        //        Debug.Log("reseting bullets");
        //        CannonAmmoUI[i].SetActive(true);
        //    }
        //}


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

        if (spawnPosGood == false && MS.spawnblock)
        {
            spawnblockWarning.text = "something is obstructing the spawn block!";
        }

        if (MS.testNewSpawnBlock)
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

    public void ChangeWeaponUI(int currentWeapon)
    {
        if (currentWeapon == 0)//bomb is being used
        {
            RESET_AMMO_UI();
            for (int i = 0; i < PM.bombsLeft; i++)
            {
                CannonAmmoUI[i].SetActive(true);
            }
        }
        else if (currentWeapon == 1)//sticky
        {
            RESET_AMMO_UI();
            for (int i = 0; i < PM.stickiesLeft; i++)
            {
                StickyAmmoUI[i].SetActive(true);
            }
        }
        else//laser
        {
            RESET_AMMO_UI();
        }

    }

    //this gets called everytime stickyLeft / bombLeft changes. can be used to incr / decr.
    void RESET_AMMO_UI()
    {
        //set both to false
        for (int i = 0; i < PM.bombClipSize; i++)
        {
            CannonAmmoUI[i].SetActive(false);
        }

        for (int i = 0; i < PM.stickyClipSize; i++)
        {
            StickyAmmoUI[i].SetActive(false);
        }
    }


}
