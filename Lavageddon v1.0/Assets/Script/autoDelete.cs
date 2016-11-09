using UnityEngine;
using System.Collections;

public class autoDelete : MonoBehaviour {

    public float deathTime;

    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, deathTime);
    }
}
