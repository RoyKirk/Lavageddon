using UnityEngine;
using System.Collections;

public class FireStickyWeight : MonoBehaviour {

    public GameObject projectile;
    public GameObject FiringPoint;

    public float speed = 100;

    public void Fire(int owner)
    {
        GameObject bullet = Instantiate(projectile, FiringPoint.transform.position, FiringPoint.transform.rotation) as GameObject;
        bullet.GetComponent<StickyWeightScript>().playerOwner = owner;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * speed);
    }

    GameObject playerManager;

    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Manager");
        DynamicVariables DV = playerManager.GetComponent<DynamicVariables>();

        speed = DV.WeaponRelated[6];
    }
}
