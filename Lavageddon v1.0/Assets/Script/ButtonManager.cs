using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    public Button[] btns;

    public int selected = 0;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(selected < 0)
        {
            selected = btns.Length - 1;
        }
        else if(selected > btns.Length - 1)
        {
            selected = 0;
        }
        btns[selected].Select();
	}
}
