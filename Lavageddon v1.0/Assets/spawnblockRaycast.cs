using UnityEngine;
using System.Collections;

public class spawnblockRaycast : MonoBehaviour
{
    int playerOwner;
    CameraMovement CM;

    void Awake()
    {
        playerOwner = GetComponent<BuildingBlock>().playerOwner;
        CM = GameObject.Find("Player" + playerOwner + "(Clone)").GetComponentInChildren<CameraMovement>();
    }

	// Update is called once per frame
	void Update ()
    {
        Vector3 up = transform.TransformDirection(Vector3.up);

	    if(Physics.Raycast(transform.position, up))
        {
            CM.spawnPosGood = false;
        }
        else
        {
            CM.spawnPosGood = true;
        }
	}
}
