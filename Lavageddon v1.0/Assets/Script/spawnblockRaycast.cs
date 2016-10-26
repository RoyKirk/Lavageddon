using UnityEngine;
using System.Collections;

public class spawnblockRaycast : MonoBehaviour
{
    int playerOwner;
    GUImanager CM;

    void Start()
    {
        playerOwner = GetComponent<BuildingBlock>().playerOwner;
        CM = GameObject.Find("Player" + playerOwner + "(Clone)").GetComponentInChildren<GUImanager>();
    }

	// Update is called once per frame
	void Update ()
    {
        if (CM.turnOffUI == false)
        {
            Vector3 up = transform.TransformDirection(Vector3.up);

            RaycastHit hit;

            if (Physics.Raycast(transform.position, up, out hit, 3))
            {
                if (hit.collider.name != "Body")
                {
                    CM.spawnPosGood = false;
                }
                else
                {
                    //object hit is player so ignore.
                }

            }
            else
            {
                CM.spawnPosGood = true;
            }
        }
        
	}
}
