using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    public DynamicVariables values;

    public Button[] MainBtns;
    public Button[] VariableBtns;
    public Button[] PlayerRelatedBtns;
    public Text[] BlockRelatedtext;
    public Text[] PlayerRelatedtext;
    public Image[] variablesBackground;

    public Dropdown dropDown;

    public int selected = 0;
    public bool Apressed = false;
    public bool Bpressed = false;

    public bool options = false;

    public float Xinput;
    public float Yinput;
    public float DpadR;
    public float DpadL;


    /*NOTES!!!!!!!
     * create a reset buttons funtions to clean up code
     * make sure you iterate over different sets of buttons appropriately
     * */


    // Use this for initialization
    void Start ()
    {
        values = FindObjectOfType<DynamicVariables>();
    }

    void HideButtons(int show)
    {
        switch (show)
        {
            case 0:
                for (int i = 0; i < PlayerRelatedBtns.Length; i++)//reset buttons
                {
                    PlayerRelatedBtns[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < VariableBtns.Length; i++)
                {
                    VariableBtns[i].gameObject.SetActive(true);
                }
                for (int i = 0; i < BlockRelatedtext.Length; i++)
                {
                    BlockRelatedtext[i].gameObject.SetActive(true);
                }
                for (int i = 0; i < PlayerRelatedtext.Length; i++)
                {
                    PlayerRelatedtext[i].gameObject.SetActive(false);
                }
                break;
            case 1:
                for (int i = 0; i < PlayerRelatedBtns.Length; i++)//reset buttons
                {
                    PlayerRelatedBtns[i].gameObject.SetActive(true);
                }
                for (int i = 0; i < VariableBtns.Length; i++)
                {
                    VariableBtns[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < BlockRelatedtext.Length; i++)
                {
                    BlockRelatedtext[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < PlayerRelatedtext.Length; i++)
                {
                    PlayerRelatedtext[i].gameObject.SetActive(true);
                }
                break;
            case 2:
                for (int i = 0; i < PlayerRelatedBtns.Length; i++)//reset buttons
                {
                    PlayerRelatedBtns[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < VariableBtns.Length; i++)
                {
                    VariableBtns[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < PlayerRelatedtext.Length; i++)
                {
                    PlayerRelatedtext[i].gameObject.SetActive(false);
                }
                break;
        }
        //for (int i = 0; i < text.Length; i++)
        //{
        //    text[i].gameObject.SetActive(true);
        //}
    }
	
	// Update is called once per frame
	void Update ()
    {
        dropDown.Hide();
        HideButtons(2);

        //highlight 6ABAEAFF
        //normal 1D678CFF
        //text 


        //they have selected the options menu
        if (options == true)
        {
            dropDown.gameObject.SetActive(true);
            variablesBackground[0].gameObject.SetActive(true);

            //set the text to their numbers so they are updating const.
            for (int i = 0; i < BlockRelatedtext.Length; i++)
            {
                BlockRelatedtext[i].text = values.BlockRelated[i].ToString();
            }
            for (int i = 0; i < PlayerRelatedtext.Length - 1; i++)
            {
                PlayerRelatedtext[i].text = values.PlayerRelated[i].ToString();
            }
            //if(dropDown.OnSelect)
            if (dropDown.value == 0)//BLOCK RELATED VARIABLES
            {
                HideButtons(0);

                
                if(selected == 0)
                {
                    dropDown.Select();
                    if(Apressed == true)
                    {
                        dropDown.value++;
                        dropDown.Hide();
                    }
                }
                else
                {
                    if (selected < 1)
                    {
                        selected = VariableBtns.Length;
                    }
                    else if (selected > VariableBtns.Length)
                    {
                        selected = 0;
                    }
                    if(selected != 0)
                    {
                        VariableBtns[selected - 1].Select();
                    }
                }
                
            }
            else if(dropDown.value == 1)//PLAYER RELATED
            {
                HideButtons(1);


                if (selected == 0)
                {
                    dropDown.Select();
                    if (Apressed == true)
                    {
                        dropDown.value++;
                    }
                }
                else
                {
                    if (selected < 1)
                    {
                        selected = PlayerRelatedBtns.Length;
                    }
                    else if (selected > PlayerRelatedBtns.Length)
                    {
                        selected = 0;
                    }
                    if (selected != 0)
                    {
                        PlayerRelatedBtns[selected - 1].Select();
                    }
                }
            }
            else if(dropDown.value == 2)
            {
                HideButtons(2);
                if (selected == 0)
                {
                    dropDown.Select();
                    if (Apressed == true)
                    {
                        dropDown.value = 0;
                        
                    }
                }
            }

            if(selected != 0)
            {
                if (Xinput > 0.3)
                {
                    values.Increment(dropDown.value, selected - 1, 1);
                }
                else if (Xinput < -0.3)
                {
                    values.Increment(dropDown.value, selected - 1, -1);
                }
                if (Xinput > 0.8)
                {
                    values.Increment(dropDown.value, selected - 1, 1);
                }
                else if (Xinput < -0.8)
                {
                    values.Increment(dropDown.value, selected - 1, -1);
                }

                if (DpadR > 0)
                {
                    values.Increment(dropDown.value, selected - 1, 1);
                }
                else if (DpadL < 0)
                {
                    values.Increment(dropDown.value, selected - 1, -1);
                }
                
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
            dropDown.gameObject.SetActive(false);
            variablesBackground[0].gameObject.SetActive(false);
            for (int i = 0; i < VariableBtns.Length; i++)
            {
                VariableBtns[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < BlockRelatedtext.Length; i++)
            {
                BlockRelatedtext[i].gameObject.SetActive(false);
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
        values.Increment(dropDown.value, 0, 1);
    }
}
