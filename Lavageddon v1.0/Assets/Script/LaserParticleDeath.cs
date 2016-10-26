using UnityEngine;
using System.Collections;

public class LaserParticleDeath : MonoBehaviour {

    public float deathTime = 0.2f;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, deathTime);
    }
	

}
