using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tocar : MonoBehaviour
{
    JointMotor2D jointMotor;

    public WheelJoint2D wheel;
    public WheelJoint2D wheel2;

    // Start is called before the first frame update
    private void FixedUpdate()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hola");

        if (collision.collider.tag.Equals("lateral"))
        {
            jointMotor.motorSpeed = -1 * wheel.motor.motorSpeed; ;
            jointMotor.maxMotorTorque = 100;

            wheel.motor = jointMotor;
            wheel2.motor = jointMotor;

        }
    }
}
