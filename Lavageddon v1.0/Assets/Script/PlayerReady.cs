﻿using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerReady : MonoBehaviour {
    public DynamicPlayerCount DPC;
    public ButtonManager BM;
    public int player = 0;
    public bool pressed = false;
    public GameObject MenuHoverAudio;
    public GameObject MenuPressAudio;
    public GameObject MenuSelectAudio;


    // Use this for initialization
    void Start ()
    {
        DPC = FindObjectOfType<DynamicPlayerCount>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
         if (Controller.connected[player] == true)
         {
             if (Controller.state[player].Buttons.Back == ButtonState.Released && pressed)
             {
                 pressed = false;
            }
             if (Controller.prevState[player].Buttons.Back == ButtonState.Pressed && Controller.state[player].Buttons.Back == ButtonState.Pressed && DPC.ready[player] == false && !pressed)
             {
                 //Instantiate(players[i]);
                 DPC.ready[player] = true;
                 pressed = true;
                Instantiate(MenuPressAudio, transform.position, Quaternion.identity);
            }
             else if (Controller.prevState[player].Buttons.Back == ButtonState.Pressed && Controller.state[player].Buttons.Back == ButtonState.Pressed && DPC.ready[player] == true && !pressed)
             {
                 //Instantiate(players[i]);
                 DPC.ready[player] = false;
                 pressed = true;
                Instantiate(MenuPressAudio, transform.position, Quaternion.identity);
            }


            //navigating with thumbsticks
           

            //navigate with dpad
            if(Controller.state[player].DPad.Up == ButtonState.Pressed && Controller.prevState[player].DPad.Up == ButtonState.Released)
            {
                BM.selected--;
                Instantiate(MenuHoverAudio, transform.position, Quaternion.identity);
            }
            else if (Controller.state[player].DPad.Down == ButtonState.Pressed && Controller.prevState[player].DPad.Down == ButtonState.Released)
            {
                BM.selected++;
                Instantiate(MenuHoverAudio, transform.position, Quaternion.identity);
            }

            if (Controller.state[player].DPad.Right == ButtonState.Pressed && Controller.prevState[player].DPad.Right == ButtonState.Released)
            {
                BM.DpadR = 1;
                Instantiate(MenuHoverAudio, transform.position, Quaternion.identity);
            }

            if (Controller.state[player].DPad.Right == ButtonState.Released & Controller.prevState[player].DPad.Right == ButtonState.Pressed)
            {
                BM.DpadR = 0;
            }

            if (Controller.state[player].DPad.Left == ButtonState.Pressed && Controller.prevState[player].DPad.Left == ButtonState.Released)
            {
                BM.DpadL = -1;
                Instantiate(MenuHoverAudio, transform.position, Quaternion.identity);
            }

            if (Controller.state[player].DPad.Left == ButtonState.Released & Controller.prevState[player].DPad.Right == ButtonState.Pressed)
            {
                BM.DpadL = 0;
            }
            
            if(Controller.state[player].ThumbSticks.Left.X != 0 || Controller.state[player].ThumbSticks.Left.Y != 0)
            {
                BM.Xinput = Controller.state[player].ThumbSticks.Left.X;
                BM.Yinput = Controller.state[player].ThumbSticks.Left.Y;
                //Debug.Log(player + "using left thumbsticks");
            }
            else if(Controller.state[player].ThumbSticks.Right.X != 0 || Controller.state[player].ThumbSticks.Right.Y != 0)
            {
                BM.Xinput = Controller.state[player].ThumbSticks.Right.X;
                BM.Yinput = Controller.state[player].ThumbSticks.Right.Y;
                //Debug.Log(BM.Xinput);
            }
            else
            {
                //BM.NoInput++;
                //BM.Xinput = 0;
                //BM.Yinput = 0;
            }
            

            //selects the current button
            if (Controller.state[player].Buttons.A == ButtonState.Pressed && Controller.prevState[player].Buttons.A == ButtonState.Released)
            {
                BM.Apressed = true;
                Instantiate(MenuSelectAudio, transform.position, Quaternion.identity);
            }
            else
            {
                //BM.Apressed = false;
            }

            if (Controller.state[player].Buttons.B == ButtonState.Pressed && Controller.prevState[player].Buttons.B == ButtonState.Released)
            {
                BM.Bpressed = true;
                Instantiate(MenuPressAudio, transform.position, Quaternion.identity);
            }
            else
            {
                //BM.Bpressed = false;
            }


        }
        
    }
}
