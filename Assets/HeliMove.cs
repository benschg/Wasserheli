using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliMove : MonoBehaviour
{
    public GameObject playerModel;
    public Joystick movementJoystick;
    public Joystick viewJoystick;

    float straveOn = 0;
    float straveSpeed = 0;
    float straveSpeedMax = 30.0f;

    float moveForwardOn = 0;
    float forwardSpeed = 0;
    float forwardSpeedMax = 30.0f;

    float updownOn = 0;
    float updownSpeed = 0;
    float updownSpeedMax = 10.0f;

    float mouseMovement = 0;
    float travelDirection = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.CheckKeys();
        this.GetMouseMovements();

        straveSpeed = Mathf.Lerp(straveSpeed, straveSpeedMax * straveOn, 5.0f * Time.deltaTime);
        forwardSpeed = Mathf.Lerp(forwardSpeed, forwardSpeedMax * moveForwardOn, 5.0f * Time.deltaTime);
        updownSpeed = Mathf.Lerp(updownSpeed, updownSpeedMax * updownOn, 5.0f * Time.deltaTime);
        travelDirection += mouseMovement;

    
    
        Vector3 movement = new Vector3(forwardSpeed * Time.deltaTime,
                                        updownSpeed * Time.deltaTime,
                                        straveSpeed * Time.deltaTime);
        Quaternion travelDirectionQ = Quaternion.Euler(new Vector3(0, travelDirection, 0));

        this.transform.position += travelDirectionQ * movement;
        this.transform.rotation = Quaternion.Euler(new Vector3(0, travelDirection, 0));

        playerModel.transform.localRotation = Quaternion.Euler(new Vector3(straveSpeed, 0, 0)) 
                                         * Quaternion.Euler(new Vector3(0,0,-forwardSpeed));
                                    
        
        //Debug.Log($" ${Time.deltaTime} - ${straveSpeed} - ${position.z}");
    }

    void CheckKeys()
    {
        //DirectionalKeys(KeyCode.A, KeyCode.D, ref this.straveOn);
        //DirectionalKeys(KeyCode.W, KeyCode.S, ref this.moveForwardOn);
        //DirectionalKeys(KeyCode.F, KeyCode.R, ref this.updownOn);

        this.straveOn = movementJoystick.Horizontal;
        this.moveForwardOn = -movementJoystick.Vertical;
        this.updownOn = viewJoystick.Vertical;
        this.mouseMovement = viewJoystick.Horizontal;
    }

    void GetMouseMovements()
    {
        //this.mouseMovement = Input.GetAxis("Mouse X");
    }


    bool DirectionalKeys(KeyCode minus, KeyCode plus, ref int onOff)
    {
        if (Input.GetKeyDown(minus))
        {
            onOff = -1;
            return true;
        }
        else if (Input.GetKeyDown(plus))
        {
            onOff = 1;
            return true;
        }
        else if ((Input.GetKeyUp(minus) && onOff == -1) ||
            (Input.GetKeyUp(plus) && onOff == 1))
        {   
            onOff = 0;
            return true;
        }
        return false;
    }

}
