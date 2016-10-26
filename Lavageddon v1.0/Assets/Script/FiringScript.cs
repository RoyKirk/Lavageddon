using UnityEngine;
using System.Collections;

public class FiringScript : MonoBehaviour
{
    public GameObject projectile;
    public GameObject FiringPoint;

    public float speed = 100;

    GameObject playerManager;

    int playerowner;
    // Use this for initialization
    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Manager");
        DynamicVariables DV = playerManager.GetComponent<DynamicVariables>();

        speed = DV.WeaponRelated[0];
    }

    public void Fire(int owner)
    {
        GameObject bullet = Instantiate(projectile, FiringPoint.transform.position, FiringPoint.transform.rotation) as GameObject;
        bullet.GetComponent<ProjectileScript>().playerOwner = owner;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * speed);
    }
}
