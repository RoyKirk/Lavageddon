using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionManagerScript : MonoBehaviour
{
    public Button[] buttons;
    public Text[] texts;
    //public GameObject[] buttons;
    //public GameObject[] text;

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(this.gameObject.name + " was selected");
    }


    void Start()
    {
        //button[0].OnSelect
    }

    void Update()
    {
        //if()
    }
}
