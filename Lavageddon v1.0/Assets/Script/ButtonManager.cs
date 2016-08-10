using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    public DynamicVariables values;

    public Button[] MainBtns;
    public Button[] VariableBtns;
    public Text[] text;
    public Image[] variablesBackground;

    public int selected = 0;
    public bool Apressed = false;
    public bool Bpressed = false;

    public bool options = false;

    public float Xinput;
    public float Yinput;
    public float DpadR;
    public float DpadL;

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

            variablesBackground[0].gameObject.SetActive(true);
            for(int i = 0; i < VariableBtns.Length; i++)
            {
                VariableBtns[i].gameObject.SetActive(true);
            }
            for (int i = 0; i < text.Length; i++)
            {
                text[i].gameObject.SetActive(true);
            }

            if (selected < 0)
            {
                selected = VariableBtns.Length - 1;
            }
            else if (selected > VariableBtns.Length - 1)
            {
                selected = 0;
            }
            VariableBtns[selected].Select();

            if(Xinput > 0.3)
            {
                values.Increment(selected, 1);
            }
            else if(Xinput < -0.3)
            {
                values.Increment(selected, -1);
            }
            if (Xinput > 0.8)
            {
                values.Increment(selected, 1);
            }
            else if (Xinput < -0.8)
            {
                values.Increment(selected, -1);
            }

            if (DpadR > 0)
            {
                values.Increment(selected, 1);
            }
            else if(DpadL < 0)
            {
                values.Increment(selected, -1);
            }
            for (int i  = 0; i < text.Length; i++)
            {
                text[i].text = values.BlockRelated[i].ToString();
            }
            
            if (Bpressed == true)
            {
                options = false;
                Bpressed = false;
                selected = 2;
            }
            //use this when you want to call an onClick function
            //OptionBtns[selected].onClick.AddListener(delegate () { PressedButton(selected); });
        }
        else
        {
            variablesBackground[0].gameObject.SetActive(false);
            for (int i = 0; i < VariableBtns.Length; i++)
            {
                VariableBtns[i].gameObject.SetActive(false);
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

            if (Apressed == true && selected == 2)
            {
                options = !options;
                selected = 0;
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
        values.Increment(0, 1);
        
    }
}
