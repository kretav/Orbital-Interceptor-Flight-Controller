using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
public class SpaceDroneController : MonoBehaviour
{
    [DllImport("SpaceDronePhysics")]
    private static extern float CalculateThrust(float currentVelocity, float neededVelocity, float gain, float kd);
    public float targetVelocity;
    public float engineGain;
    public Rigidbody rb; 
    public Transform targetDebris;
    public float brakeKd;


    // Update is called once per frame
    
    void FixedUpdate()
    {
        // 1. Calculate positions and required speed 
        // Increase safety distance to 1.5 meters to account for object radiuses
float distanceX = (targetDebris.position.x - transform.position.x) - 1.5f;

        float currentX_Velocity = rb.velocity.x;
        float neededVelocity = distanceX * 1.0f; // Speed depends on distance now!

        // 2. Pass the dynamic 'neededVelocity' into your C++ engine
        float thrustForce = CalculateThrust(currentX_Velocity, neededVelocity, engineGain, brakeKd);


        // 3. Apply the calculated force to the drone
        rb.AddForce(new Vector3(thrustForce, 0, 0), ForceMode.Force);
    }


}
