#include "pch.h"
#include "FlightController.h"

// 1. FIX: Added float kd and renamed targetVelocity to neededVelocity
DLLEXPORT float CalculateThrust(float currentVelocity, float neededVelocity, float gain, float kd) {

    // 2. Calculate the physics error using the correct variable name
    float error = neededVelocity - currentVelocity;

    // 3. Perfect PD math that forces the drone to brake before collision
    float totalThrust = (error * gain) - (currentVelocity * kd);

    return totalThrust;
}
