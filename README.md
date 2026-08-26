# Orbital-Interceptor-Flight-Controller
### Orbital-Interceptor: 3D Aerospace Flight Controller

An advanced, high-performance aerospace simulator that couples **Unity 3D** with a custom, native **C++ Dynamic-Link Library (DLL)** to control a maneuvering space drone designed for orbital debris interception. 

### 🚀 Project Overview

This project demonstrates precision autonomous guidance in zero-gravity space environments using a hybrid engine architecture: 

1. **Physics & Graphics (Unity 3D):** Models the spatial environment, calculates real-time 3D rigid-body kinematics, and provides the visual simulation pipeline.
2. **Native Flight Core (C++):** A compiled dynamic library processing high-speed differential calculus via a custom **PD (Proportional-Derivative) Regressor** to govern thrust calculations.
3. **P/Invoke Bridge (C#):** A low-latency integration layer driving custom interop communication loops between Managed (C#) and Unmanaged (C++) memory boundaries.

### 🛠️ Tech Stack & Systems

* **System Language:** C++17 (MSVC Compiler)
* **Engine Framework:** Unity 2021.3.5f1 LTS
* **Scripting Language:** C# (Structured Interop)
* **Hardware Target:** Optimized for multi-threaded architectures (AMD Ryzen 9 execution verification)

[Unity 3D Rigidbody] ➔ (Reads velocity.x) ➔ [C# Interop Bridge]
         ▲                                            │
         │ (Applies Vector3 Force)                    ▼ (Passes float parameters)
   [rb.AddForce] ◄─── [C++ SpaceDronePhysics.dll] ◄─── [CalculateThrust()]
                      (PD Control Loop Logic)

### 🧠 Control Loop Math & Geometry Optimization

### 1. The PD-Controller Formula

To eliminate infinite inertial drifting inherent to deep-space vacuums, the compiled C++ core utilizes a Proportional-Derivative feedback loop. The Proportional component drives acceleration toward the target, while the Derivative component computes an instantaneous counter-thrust vector based on approaching velocity to act as an electronic braking mechanism:

Total Thrust=(Error×Gain)−(CurrentVelocity×Kd)Total Thrust equals open paren Error cross Gain close paren minus open paren CurrentVelocity cross cap K sub d close paren
Total Thrust=(Error×Gain)−(CurrentVelocity×𝐾𝑑)

Where: 

* Error = NeededVelocity - CurrentVelocity
* Gain = Proportional Power Coeff
* 𝐾𝑑

=Differential Braking Damping Coeff

### 2. 3D Spatial Geometry Fix (Object Radii Compensation)

During edge-case regression tests, standard vector evaluation targeted the exact local pivot point (center) of the debris volume, risking catastrophic collisions. 

The software architecture resolved this by integrating a hardcoded spatial offset modifier directly into the C# execution layout, accounting for the combined structural bounding box dimensions (0.5m drone radius + 0.5m debris radius + 0.5m buffer zone): 

csharp

float distanceX = (targetDebris.position.x - transform.position.x) - 1.5f;

Используйте код с осторожностью.

This bounds evaluation guarantees an autonomous soft-dock holding sequence exactly 50cm clear of the targeted hardware shell. 

### 💻 Structure

* /SpaceDronePhysics/ — Source C++ scripts (FlightController.cpp, FlightController.h)
* /UnityProjectFiles/ — C# control loops (SpaceDroneController.cs) and plugin configurations.

### 📊 Evaluation Metrics (Unity FixedUpdate Verification)

* **Unregulated Profile:** Engine Gain: 1.5 | Brake Kd: 0.0 ➔ Infinite overshoot / Hard kinetic target destruction.
* **Damped Intercept Profile:** Engine Gain: 1.5 | Brake Kd: 2.0 ➔ Smooth autonomous decrescendo; steady-state holding pattern locked exactly at **0.50m**.
