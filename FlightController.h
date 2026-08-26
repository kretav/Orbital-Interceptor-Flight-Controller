#pragma once

// Standard macro to export functions to Unity
#define DLLEXPORT extern "C" __declspec(dllexport)

// We define a structure to hold PID values for 3D stabilization
struct PIDController {
    float proportionalGain;
    float integralGain;
    float derivativeGain;
    float integrationStored;
    float kd;
};

// This function will be called by Unity every physics frame
DLLEXPORT float CalculateThrust(float currentVelocity, float targetVelocity, float gain, float kd);

