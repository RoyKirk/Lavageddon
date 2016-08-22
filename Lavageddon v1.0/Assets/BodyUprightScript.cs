using UnityEngine;
using System.Collections;

public class BodyUprightScript : MonoBehaviour {

	// Update is called once per frame
	void LateUpdate () {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
	}
}
