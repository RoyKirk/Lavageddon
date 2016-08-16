using UnityEngine;
using System.Collections;

public class FireStickyWeight : MonoBehaviour {

    public GameObject projectile;
    public GameObject FiringPoint;

    public float speed = 100;

    public void Fire()
    {
        GameObject bullet = Instantiate(projectile, FiringPoint.transform.position, FiringPoint.transform.rotation) as GameObject;
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
