// 12/25/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class RacingGameController : MonoBehaviour
{
    [Header("Car Settings")]
    public float acceleration = 10f;
    public float maxSpeed = 50f;
    public float brakeForce = 20f;
    //public float driftFactor = 0.95f;
    public float deceleration = 0.98f;
    public float depthAccel = 10;

    [Header("Steering")]
    public float turnSpeed = 50f;
    public Transform[] steeringRotators;
    public float steeringRotationAmount = 15f;

    // [Header("Physics")]
    // public float gravity = 9.81f;

    private float currentSpeed = 0f;
    private float turnInput;
    private float currentTurn;
    private float preDepthTurn;
    private float currentDepthTurn;
    private float accelerationInput;
    private float depthInput;
    private Rigidbody rb;

    [Header("Road Collision Settings")]
    public LayerMask roadLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleTurning();
        //HandleDrifting();
        //CheckRoadCollision();
    }

    private void HandleInput() {
        accelerationInput = Input.GetAxis("Vertical"); // W/S or Up/Down Arrow keys
        turnInput = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow keys
        depthInput = Input.GetAxis("Depth");
    }

    private void HandleMovement() {
        if (accelerationInput > 0 && currentSpeed >= 0) {
            currentSpeed += acceleration * Time.fixedDeltaTime;
            //Debug.Log("Accelerating forward");
        } else if (accelerationInput < 0 && currentSpeed > 0) {
            //Debug.Log("Braking forward");
            currentSpeed -= brakeForce * Time.fixedDeltaTime;
        } else if (accelerationInput > 0 && currentSpeed < 0) {
            //Debug.Log("Braking backward");
            currentSpeed += brakeForce * Time.fixedDeltaTime;
        } else if (accelerationInput < 0) {
            //Debug.Log("Accelerating backwards");
            currentSpeed -= acceleration * Time.fixedDeltaTime;
        } else {
            currentSpeed *= deceleration; // Gradual deceleration
        }

        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed / 2, maxSpeed);
        //Debug.Log(currentSpeed);
        rb.linearVelocity = transform.forward * currentSpeed; // + Vector3.down * gravity;
    }

    private void HandleTurning() {

            currentTurn = Mathf.Lerp(currentTurn, turnInput, 5 * Time.fixedDeltaTime);
        if (currentSpeed != 0) {
            transform.Rotate(0, currentTurn * turnSpeed * Time.fixedDeltaTime * Math.Abs(currentSpeed), 0);
        }

        for (int i = 0; i < steeringRotators.Length; i++) {
            steeringRotators[i].localEulerAngles = new Vector3(0, -90, 90) + new Vector3(0, currentTurn * steeringRotationAmount, 0);
        }

        // VERTICAL up & down movement
        preDepthTurn = depthInput * depthAccel * Time.fixedDeltaTime;
        currentDepthTurn = Mathf.Lerp(currentDepthTurn, preDepthTurn, 0.8f * Time.fixedDeltaTime);
        transform.Rotate(currentDepthTurn, 0, 0);
        Debug.Log(turnInput);
    }

    // private void HandleDrifting() {
    //     if (Input.GetKey(KeyCode.Space)) {
    //         rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, transform.forward * currentSpeed, driftFactor * Time.fixedDeltaTime);
    //     }
    // }

    private void CheckRoadCollision() {
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f, roadLayer))
        {
            currentSpeed *= 0.5f; // Reduce speed if off-road
        }
    }
}
