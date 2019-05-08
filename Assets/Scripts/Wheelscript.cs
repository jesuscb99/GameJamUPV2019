using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheelscript : MonoBehaviour
{
    public GameObject wheel;
    public GameObject wheel2;
    public Map_generator generator;
    private WheelJoint2D joint;

    public float minforce = 500;
    public float force = 700;
    public float maxforce = 800;

    // Start is called before the first frame update
    void Start()
    {
        wheel = transform.GetChild(0).gameObject;
        wheel2 = transform.GetChild(1).gameObject;

        generator = transform.GetComponent<Car_control>().generator;
    }

    // Update is called once per frame
    void Update()
    {
        JointMotor2D new_motor;
        if (wheel.GetComponent<Touching>().touching && !wheel2.GetComponent<Touching>().touching)
        {
            new_motor = wheel.GetComponent<WheelJoint2D>().motor;
            new_motor.motorSpeed = minforce;
            wheel.GetComponent<WheelJoint2D>().motor = new_motor;
        }
        else if (!wheel.GetComponent<Touching>().touching && wheel2.GetComponent<Touching>().touching)
        {
            new_motor = wheel2.GetComponent<WheelJoint2D>().motor;
            new_motor.motorSpeed = maxforce;
            wheel2.GetComponent<WheelJoint2D>().motor = new_motor;
        }
        else
        {
            new_motor = wheel2.GetComponent<WheelJoint2D>().motor;
            new_motor.motorSpeed = force;
            wheel2.GetComponent<WheelJoint2D>().motor = new_motor;
            new_motor = wheel.GetComponent<WheelJoint2D>().motor;
            new_motor.motorSpeed = force;
            wheel.GetComponent<WheelJoint2D>().motor = new_motor;
        }
    }
}
