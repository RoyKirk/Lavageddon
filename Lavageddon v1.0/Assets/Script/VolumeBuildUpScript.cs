using UnityEngine;
using System.Collections;

public class VolumeBuildUpScript : MonoBehaviour {

    public float volumeChange = 0.1f;

	// Use this for initialization
	void Awake () {
        AudioListener.volume = 0.0f;	
	}
    void Start()
    {
        AudioListener.volume = 0.0f;
    }

    // Update is called once per frame
    void Update () {        
        if(AudioListener.volume < 1.0f)
        {
            AudioListener.volume += Time.deltaTime * volumeChange;
        }
        else
        {
            AudioListener.volume = 1.0f;
        }
	
	}
}
