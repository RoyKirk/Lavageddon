using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    public DynamicVariables values;

    public Button[] MainBtns;
    public Button[] OptionBtns;
    public Text[] text;

    public int selected = 0;
    public bool Apressed = false;
    public bool Bpressed = false;

    public bool options = false;

    public float Xinput;
    public float Yinput;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        


        

        //they have selected the options menu
        if(options == true)
        {
            
            for(int i = 0; i < OptionBtns.Length; i++)
            {
                OptionBtns[i].gameObject.SetActive(true);
            }
            for (int i = 0; i < text.Length; i++)
            {
                text[i].gameObject.SetActive(true);
            }

            if (selected < 0)
            {
                selected = OptionBtns.Length - 1;
            }
            else if (selected > OptionBtns.Length - 1)
            {
                selected = 0;
            }
            OptionBtns[selected].Select();

            values.Increment(selected, Xinput);
            text[0].text = values.MAXRESOURCES.ToString();
            text[1].text = values.FLOATBLOCKHP.ToString();
            if (Bpressed == true)
            {
                options = false;
                Bpressed = false;
            }
            //use this when you want to call an onClick function
            //OptionBtns[selected].onClick.AddListener(delegate () { PressedButton(selected); });
        }
        else
        {
            for (int i = 0; i < OptionBtns.Length; i++)
            {
                OptionBtns[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < text.Length; i++)
            {
                text[i].gameObject.SetActive(false);
            }


            if (selected < 0)
            {
                selected = MainBtns.Length - 1;
            }
            else if (selected > MainBtns.Length - 1)
            {
                selected = 0;
            }
            MainBtns[selected].Select();

            if (Apressed == true && selected == 1)
            {
                options = !options;
            }
        }
        
        //this navigates the menu with thumbstick, making sure you can only go through one button at a time. no scrolling
        if(Yinput < 0.1 && Yinput > -0.1)
        {
            reset = false;
        }

        if (Yinput < -0.1 && reset == false)
        {
            selected++;
            reset = true;
        }
        else if (Yinput > 0.1 && reset == false)
        {
            selected--;
            reset = true;
        }
    }

    public bool reset = false;

    public void PressedButton(int selectedbtn)
    {
        values.Increment(0, 0);
        
    }
}
