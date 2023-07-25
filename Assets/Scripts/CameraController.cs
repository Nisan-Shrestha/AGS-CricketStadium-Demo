using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.PlayerLoop;
using Etouch = UnityEngine.InputSystem.EnhancedTouch;
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Vector2 JoystickSize = new Vector2(100  , 100);
    //[SerializeField]
    //private FloatingJoystick Joystick;
    [SerializeField]
    public float moveSpeed = 1; 
    [SerializeField]
    public float rotSpeed = 2;

    [SerializeField]
    public Joystick MoveJoystick;
    [SerializeField]
    public Joystick RotateJoystick;

    float pitchAmount = 0f;
    float yawAmount = 0f;

    float forwardMove = 0f;
    float sideMove = 0f;


    private void Start()
    {
      
    }

    // Update is called once per frame

    void Update()
    {
        move();
        rotate();
        bounder();
    }
    private void bounder()
    {
        var t = transform.position;
        t.x = Mathf.Clamp(t.x, -225, 225);
        t.y = Mathf.Clamp(t.y, 11, 140);
        t.z = Mathf.Clamp(t.z, -225, 225);
        transform.position = t;

    }

    private void rotate()
    {
        pitchAmount = RotateJoystick.Vertical * rotSpeed;
        yawAmount = RotateJoystick.Horizontal * rotSpeed;
        var p = transform.rotation;
        //transform.Rotate(new Ve ctor3(-pitchAmount, yawAmount,0));
        //Debug.Log(RotateJoystick.Horizontal + "    " + RotateJoystick.Vertical);
        if (Mathf.Abs(RotateJoystick.Vertical) > .5f)
            transform.Rotate(new Vector3(-pitchAmount,0,0));
        if (Mathf.Abs( RotateJoystick.Horizontal )> .5f)
            transform.Rotate(new Vector3(0, yawAmount, 0));

        
        var r = transform.rotation.eulerAngles;
        float clampAngle = 85.0f;
        if (r.x> clampAngle && r.x < (360.0f- clampAngle))
        {
            if (r.x>180.0f)
                r.x = 360- clampAngle;
            else
                r.x = clampAngle;
        }
        Debug.Log(r.x);
        transform.rotation = Quaternion.Euler(r.x, r.y, 0f);
        //transform.rotation = Quaternion.Euler(0f, rotX, 0f);



        //r.x = Mathf.Clamp(r.x, -90.0f, 90.0f);
        //transform.localRotation = Quaternion.Euler( new Vector3(r.x,r.y,0));
        //p += transform.up * pitchAmount;
        //p += transform.right * yawAmount;
        //transform.position = p;

    }

    private void move()
    {
        forwardMove = MoveJoystick.Vertical * moveSpeed;
        sideMove = MoveJoystick.Horizontal * moveSpeed;
        var p = transform.position;
        p += transform.forward * forwardMove;
        p += transform.right * sideMove;
        transform.position = p;
    }
}
