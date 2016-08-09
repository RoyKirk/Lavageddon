using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerReady : MonoBehaviour {
    public DynamicPlayerCount DPC;
    public ButtonManager BM;
    public int player = 0;
    public bool pressed = false;

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
             }
             else if (Controller.prevState[player].Buttons.Back == ButtonState.Pressed && Controller.state[player].Buttons.Back == ButtonState.Pressed && DPC.ready[player] == true && !pressed)
             {
                 //Instantiate(players[i]);
                 DPC.ready[player] = false;
                 pressed = true;
             }


            //navigating with thumbsticks
            BM.Xinput = Controller.state[player].ThumbSticks.Left.X;
            BM.Yinput = Controller.state[player].ThumbSticks.Left.Y;

            //navigate with dpad
            if(Controller.state[player].DPad.Up == ButtonState.Pressed && Controller.prevState[player].DPad.Up == ButtonState.Released)
            {
                BM.selected--;
            }
            else if (Controller.state[player].DPad.Down == ButtonState.Pressed && Controller.prevState[player].DPad.Down == ButtonState.Released)
            {
                BM.selected++;
            }

            //selects the current button
            if (Controller.state[player].Buttons.A == ButtonState.Pressed && Controller.prevState[player].Buttons.A == ButtonState.Released)
            {
                BM.Apressed = true;
            }
            else
            {
                BM.Apressed = false;
            }

            if (Controller.state[player].Buttons.B == ButtonState.Pressed && Controller.prevState[player].Buttons.B == ButtonState.Released)
            {
                BM.Bpressed = true;
            }
            else
            {
                BM.Bpressed = false;
            }


        }
        
    }
}
