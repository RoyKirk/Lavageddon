using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class RestartMenu : MonoBehaviour {

    public GameObject menu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        for(int i = 0; i < 4; i++)
        {
            if (Controller.prevState[i].Buttons.Start == ButtonState.Released && Controller.state[i].Buttons.Start == ButtonState.Pressed)
            {
                menu.SetActive(!menu.activeSelf);
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
