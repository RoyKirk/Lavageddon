using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class startButton : MonoBehaviour {

    public bool ready = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void ClickStart()
    {
        if(ready)
        {
            SceneManager.LoadScene(1);
        }
     }

    //void OnGUI()
    //{
    //    if(GUI.Button(new Rect(10, 70, 50, 30), "Start"))
    //    {
    //        //Debug.Log("Clicked the button with text");
    //        //Application.LoadLevel(1);
    //        SceneManager.LoadScene(1);
    //    }
    //}
}
