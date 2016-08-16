using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class Continue : MonoBehaviour {

    public GameObject menu;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.prevState[0].Buttons.Start == ButtonState.Released && Controller.state[0].Buttons.Start == ButtonState.Pressed)
        {
            menu.SetActive(!menu.activeSelf);
        }

    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(0);
    }
}
