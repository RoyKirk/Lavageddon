﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class PlayerMovement : MonoBehaviour {

    public enum Weapon
    {
        BOMB,
        LASER,
        STICKY,
    };

    int numberOfWeapons = 3;
    public Weapon weapon = Weapon.BOMB;
    public int laserDamage = 1;
    public float movementSpeed;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    public float frictionCast = 1.0f;
    public float jumpForce = 1000.0f;
    public float laserForce = 1000.0f;
    float rotationY = 0F;

    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    int lengthOfLineRenderer = 10;

    public float laserMinTime = 0.5f;
    public float laserResidual = 0.02f;
    float laserTimer = 0.0f;
    public float bombMinTime = 0.5f;
    float bombTimer = 0.0f;
    public float stickyMinTime = 0.5f;
    float stickyTimer = 0.0f;

    public int player = 0;

    public bool alive = true;

    public float lavaHeight = 1.0f;

    public float submergedMinTime = 1.0f;
    float submergedTimer = 0.0f;
    public bool submergeAccumulate = true;

    public GameObject redScreen;

    public GameObject body;

    //public float shootDistance = 1000.0f;
    void Update()
    {
        if (alive)
        {



                RaycastHit hit;

                if (Physics.Raycast(body.transform.position+ new Vector3(0, 1, 0), new Vector3(0, -1, 0), out hit, frictionCast))
                {

                    Debug.DrawLine(body.transform.position, hit.point);
                    Debug.Log(hit.collider.name);
                    if (Controller.prevState[player].Buttons.A == ButtonState.Released && Controller.state[player].Buttons.A == ButtonState.Pressed)
                    {
                         GetComponent<Rigidbody>().AddForce(0, jumpForce, 0);
                    //if (hit.collider.tag == "Block")
                    //{

                    //}
                    }
            }

            //controller look
            //float rotationX = transform.localEulerAngles.y + Controller.state[player].ThumbSticks.Right.X*(sensitivityX/10);

            //rotationY += Controller.state[player].ThumbSticks.Right.Y * (sensitivityY/10);
            //rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            //transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);

            float rotationX = Controller.state[player].ThumbSticks.Right.X * (sensitivityX / 10);
            rotationY = Controller.state[player].ThumbSticks.Right.Y * (sensitivityY / 10);

            transform.RotateAround(body.transform.position, body.transform.up, rotationX);
            transform.RotateAround(body.transform.position, body.transform.right, -rotationY);

            //transform.RotateAround(body.transform.position, transform.up, rotationX);
            //transform.RotateAround(body.transform.position, transform.right, -rotationY);


            transform.position += Controller.state[player].ThumbSticks.Left.Y * new Vector3(transform.forward.normalized.x + transform.up.normalized.x, 0, transform.forward.normalized.z + transform.up.normalized.z) * movementSpeed;

            transform.position += Controller.state[player].ThumbSticks.Left.X * transform.right.normalized * movementSpeed;

            laserTimer += Time.deltaTime;
            bombTimer += Time.deltaTime;
            stickyTimer += Time.deltaTime;

            if (laserTimer >= laserResidual)
            {
                LineRenderer lineRenderer = GetComponent<LineRenderer>();
                lineRenderer.enabled = false;
            }

            if (laserTimer < laserMinTime)
            {
                RaycastHit shot;
                if (Physics.Raycast(transform.position, transform.forward, out shot))
                {

                    LineRenderer lineRenderer = GetComponent<LineRenderer>();
                    int i = 0;
                    while (i < lengthOfLineRenderer)
                    {
                        //Vector3 pos = (transform.position + transform.right.normalized*(lengthOfLineRenderer-i+1)/20) + (transform.forward.normalized * i - transform.right.normalized / (lengthOfLineRenderer - i + 1) / 20);
                        //Vector3 pos = transform.position + transform.forward.normalized * i;
                        //Vector3 pos = (transform.position - transform.up.normalized * (lengthOfLineRenderer - i + 1) / 20) + (transform.forward.normalized * i + transform.up.normalized / (lengthOfLineRenderer - i + 1) / 20);

                        Vector3 pos = (body.transform.position + new Vector3(0,1,0)) + i * ((shot.point - body.transform.position) / lengthOfLineRenderer);
                        lineRenderer.SetPosition(i, pos);
                        i++;
                    }
                }
            }

            //if (Controller.prevState[player].Triggers.Right < 0.1 && Controller.state[player].Triggers.Right > 0.1)
            if (Controller.state[player].Triggers.Right > 0.1)
            {

                if (weapon == Weapon.BOMB && bombTimer >= bombMinTime)
                {
                    bombTimer = 0.0f;
                    GetComponent<FiringScript>().Fire();
                }
                if (weapon == Weapon.STICKY && stickyTimer >= stickyMinTime)
                {
                    stickyTimer = 0.0f;
                    GetComponent<FireStickyWeight>().Fire();
                }
                if (weapon == Weapon.LASER && laserTimer >= laserMinTime)
                {
                    laserTimer = 0.0f;
                    //laser.enabled = true;
                    //int i = 0;
                    //while (i < lengthOfLineRenderer)
                    //{
                    //    //Vector3 pos = new Vector3(i * 0.5F, Mathf.Sin(i + t), 0);
                    //    Vector3 pos = transform.position + transform.forward.normalized * i;
                    //    i++;
                    //}
                    //laser.SetPosition(0, transform.position);
                    //laser.SetPosition(1, transform.position + transform.forward.normalized * laserLength);
                    RaycastHit shot;
                    if (Physics.Raycast(transform.position, transform.forward, out shot))
                    {
 
                        LineRenderer lineRenderer = GetComponent<LineRenderer>();
                        lineRenderer.enabled = true;
                        int i = 0;
                        while (i < lengthOfLineRenderer)
                        {
                            //Vector3 pos = (transform.position + transform.right.normalized*(lengthOfLineRenderer-i+1)/20) + (transform.forward.normalized * i - transform.right.normalized / (lengthOfLineRenderer - i + 1) / 20);
                            //Vector3 pos = transform.position + transform.forward.normalized * i;
                            //Vector3 pos = (transform.position - transform.up.normalized * (lengthOfLineRenderer - i + 1) / 20) + (transform.forward.normalized * i + transform.up.normalized / (lengthOfLineRenderer - i + 1) / 20);
                            //lineRenderer.SetPosition(i, pos);
                            Vector3 pos = (body.transform.position + new Vector3(0, 1, 0)) + i*((shot.point - body.transform.position)/lengthOfLineRenderer);
                            lineRenderer.SetPosition(i, pos);
                            i++;
                        }
                        Debug.DrawLine(transform.position, shot.point, Color.green);
                        if (shot.collider.tag == "Block")
                        {

                            shot.collider.GetComponent<BlockDamage>().Damage(laserDamage);
                            shot.collider.GetComponent<Rigidbody>().AddForceAtPosition(transform.forward.normalized * laserForce, shot.point);

                        }
                    }
                }
            }


            if (Controller.prevState[player].Buttons.RightShoulder == ButtonState.Released && Controller.state[player].Buttons.RightShoulder == ButtonState.Pressed)
            {
                weapon++;
            }
            if (Controller.prevState[player].Buttons.LeftShoulder == ButtonState.Released && Controller.state[player].Buttons.LeftShoulder == ButtonState.Pressed)
            {
                weapon--;
            }

            if ((int)weapon == numberOfWeapons)
            {
                weapon = (Weapon)0;
            }
            else if ((int)weapon < 0)
            {
                weapon = (Weapon)(numberOfWeapons - 1);
            }


            //if (Controller.prevState[player].Buttons.Start == ButtonState.Released && Controller.state[player].Buttons.Start == ButtonState.Pressed)
            //{
            //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //}
        }

    }

    void LateUpdate()
    {
        if (alive)
        {


            RaycastHit hit;

            //if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit, frictionCast))
            //{
            //    if (hit.collider.tag == "Block")
            //    {
            //        Vector3 vel = hit.collider.GetComponent<Rigidbody>().velocity;
            //        body.GetComponent<Rigidbody>().velocity = new Vector3(vel.x, body.GetComponent<Rigidbody>().velocity.y, vel.z);
            //        //GetComponent<Rigidbody>().angularVelocity = hit.collider.GetComponent<Rigidbody>().angularVelocity;
            //    }
            //    else
            //    {
            //        //GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
            //        //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            //    }
            //}
            //else
            //{
            //    //GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
            //    //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            //}

            if (body.transform.position.y < lavaHeight)
            {
                PlayerDead();

            }
            if (body.transform.position.y > lavaHeight && !submergeAccumulate)
            {
                submergedTimer = 0;
            }
        }
        else
        {
            //controller look
            //float rotationX = transform.localEulerAngles.y + Controller.state[player].ThumbSticks.Right.X;

            //rotationY += Controller.state[player].ThumbSticks.Right.Y;
            //rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            //transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            float rotationX = Controller.state[player].ThumbSticks.Right.X * (sensitivityX / 10);
            rotationY = Controller.state[player].ThumbSticks.Right.Y * (sensitivityY / 10);
            transform.RotateAround(body.transform.position, body.transform.up, rotationX);
            transform.RotateAround(body.transform.position, body.transform.right, -rotationY);

            transform.position += Controller.state[player].ThumbSticks.Left.Y * transform.forward.normalized * movementSpeed;

            transform.position += Controller.state[player].ThumbSticks.Left.X * transform.right.normalized * movementSpeed;

            transform.position += Controller.state[player].Triggers.Right * transform.up.normalized * movementSpeed;

            transform.position -= Controller.state[player].Triggers.Left * transform.up.normalized * movementSpeed;

        }


        if (transform.position.y < lavaHeight)
        {
            redScreen.SetActive(true);
            GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>().enabled = true;
            GetComponent<UnityStandardAssets.ImageEffects.GlobalFog>().enabled = true;
        }
        if (transform.position.y > lavaHeight)
        {
            redScreen.SetActive(false);
            GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>().enabled = false;
            GetComponent<UnityStandardAssets.ImageEffects.GlobalFog>().enabled = false;
        }

    }

    //used for setting variables set in menu
    GameObject playerManager;

    void Start()
    {
        //Cursor.visible = false;
        GetComponent<CameraMovement>().enabled = false;
        //GetComponent<WhirlpoolCurrent>().enabled = true;
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().freezeRotation = true;
            GetComponent<Rigidbody>().isKinematic = false;
        }

        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetColors(c1, c2);
        lineRenderer.SetWidth(0.3F, 0.3F);
        lineRenderer.SetVertexCount(lengthOfLineRenderer);
        lineRenderer.enabled = false;
        alive = true;


        //setting the menu variables to the player variables
        playerManager = GameObject.FindGameObjectWithTag("Manager");
        DynamicVariables DV = playerManager.GetComponent<DynamicVariables>();
        Rigidbody rb = GetComponent<Rigidbody>();

        //PLAYER RELATED VALUES
        jumpForce = (DV.PlayerRelated[0] * 10);
        sensitivityX = DV.PlayerRelated[1];
        sensitivityY = DV.PlayerRelated[2];
        rb.mass = DV.PlayerRelated[3];
        submergedMinTime = DV.PlayerRelated[4];
        if(DV.PlayerRelated[5] == 1)
        {
            submergeAccumulate = true;
        }
        else
        {
            submergeAccumulate = false;
        }
        movementSpeed = DV.PlayerRelated[7] / 100;

        //WEAPON RELATED VALUES
        //cannon isnt referenced here
        bombMinTime = DV.WeaponRelated[2];

        laserDamage = (int)DV.WeaponRelated[3];
        laserMinTime = DV.WeaponRelated[4];
        laserForce = DV.WeaponRelated[5];

        stickyMinTime = DV.WeaponRelated[7];

    }

    void PlayerDead()
    {

        submergedTimer += Time.deltaTime;
        if (submergedTimer >= submergedMinTime)
        {
            alive = false;
            GameObject.Find("PlayerManager").GetComponent<DynamicPlayerCount>().playerDeath();
            transform.position = new Vector3(5, 50, -65);
            transform.eulerAngles = new Vector3(45, 0, 0);
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            submergedTimer = 0;

            body.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}
