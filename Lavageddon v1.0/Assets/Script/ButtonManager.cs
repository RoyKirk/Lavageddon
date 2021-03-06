﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    public DynamicVariables values;

    public Button[] MainBtns;
    public Button[] VariableBtns;
    public Button[] PlayerRelatedBtns;
    public Button[] WeaponRelatedBtns;

    public Text[] BlockRelatedtext;
    public Text[] PlayerRelatedtext;
    public Text[] WeaponRelatedtext;

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

    //public float NoInput;


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
            case 0://BLOCK RELATED
                //BUTTON RELATED
                for (int i = 0; i < PlayerRelatedBtns.Length; i++)
                {
                    PlayerRelatedBtns[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < VariableBtns.Length; i++)
                {
                    VariableBtns[i].gameObject.SetActive(true);
                }
                for (int i = 0; i < WeaponRelatedBtns.Length; i++)
                {
                    WeaponRelatedBtns[i].gameObject.SetActive(false);
                }

                //TEXT RELATED
                for (int i = 0; i < BlockRelatedtext.Length; i++)
                {
                    BlockRelatedtext[i].gameObject.SetActive(true);
                }
                for (int i = 0; i < PlayerRelatedtext.Length; i++)
                {
                    PlayerRelatedtext[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < WeaponRelatedtext.Length; i++)
                {
                    WeaponRelatedtext[i].gameObject.SetActive(false);
                }
                break;

            case 1://PLAYER RELATED
                //BUTTON RELATED
                for (int i = 0; i < PlayerRelatedBtns.Length; i++)
                {
                    PlayerRelatedBtns[i].gameObject.SetActive(true);
                }
                for (int i = 0; i < VariableBtns.Length; i++)
                {
                    VariableBtns[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < WeaponRelatedBtns.Length; i++)
                {
                    WeaponRelatedBtns[i].gameObject.SetActive(false);
                }

                //TEXT RELATED
                for (int i = 0; i < BlockRelatedtext.Length; i++)
                {
                    BlockRelatedtext[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < PlayerRelatedtext.Length; i++)
                {
                    PlayerRelatedtext[i].gameObject.SetActive(true);
                }
                for (int i = 0; i < WeaponRelatedtext.Length; i++)
                {
                    WeaponRelatedtext[i].gameObject.SetActive(false);
                }
                break;

            case 2://WEAPON RELATED
                   //BUTTON RELATED
                for (int i = 0; i < PlayerRelatedBtns.Length; i++)
                {
                    PlayerRelatedBtns[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < VariableBtns.Length; i++)
                {
                    VariableBtns[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < WeaponRelatedBtns.Length; i++)
                {
                    WeaponRelatedBtns[i].gameObject.SetActive(true);
                }

                //TEXT RELATED
                for (int i = 0; i < BlockRelatedtext.Length; i++)
                {
                    BlockRelatedtext[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < PlayerRelatedtext.Length; i++)
                {
                    PlayerRelatedtext[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < WeaponRelatedtext.Length; i++)
                {
                    WeaponRelatedtext[i].gameObject.SetActive(true);
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
        //if(NoInput >= 4)
        //{
        //    Xinput = 0;
        //    Yinput = 0;
        //    NoInput = 0;
        //}
        
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
            for (int i = 0; i < WeaponRelatedtext.Length; i++)
            {
                WeaponRelatedtext[i].text = values.WeaponRelated[i].ToString();
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
                        Apressed = false;
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
                        Apressed = false;
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
                        dropDown.Hide();
                        Apressed = false;
                    }
                }
                else
                {
                    if (selected < 1)
                    {
                        selected = WeaponRelatedBtns.Length;
                    }
                    else if (selected > WeaponRelatedBtns.Length)
                    {
                        selected = 0;
                    }
                    if (selected != 0)
                    {
                        WeaponRelatedBtns[selected - 1].Select();
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

                //if (DpadR > 0)
                //{
                //    values.Increment(dropDown.value, selected - 1, 1);
                //}

                //if (DpadL < 0)
                //{
                //    values.Increment(dropDown.value, selected - 1, -1);
                //}

                values.Increment(dropDown.value, selected - 1, DpadR);
                values.Increment(dropDown.value, selected - 1, DpadL);

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
            for (int i = 0; i < PlayerRelatedBtns.Length; i++)
            {
                PlayerRelatedBtns[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < VariableBtns.Length; i++)
            {
                VariableBtns[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < WeaponRelatedBtns.Length; i++)
            {
                WeaponRelatedBtns[i].gameObject.SetActive(false);
            }

            //TEXT RELATED
            for (int i = 0; i < BlockRelatedtext.Length; i++)
            {
                BlockRelatedtext[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < PlayerRelatedtext.Length; i++)
            {
                PlayerRelatedtext[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < WeaponRelatedtext.Length; i++)
            {
                WeaponRelatedtext[i].gameObject.SetActive(false);
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
                Apressed = false;
            }
        }
        
        //this navigates the menu with thumbstick, making sure you can only go through one button at a time. no scrolling
        if (Yinput < 0.1 && Yinput > -0.1)
        {
            reset = false;
        }
        //reset = false;
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
        Xinput = 0;
        Yinput = 0;
    }

    public bool reset = false;

    public void PressedButton(int selectedbtn)
    {
        values.Increment(dropDown.value, 0, 1);
    }
}
