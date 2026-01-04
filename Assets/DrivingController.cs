// 12/25/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;

public class RacingGameController : MonoBehaviour
{
    [Header("Car Settings")]
    public float acceleration = 10f;
    public float maxSpeed = 50f;
    public float brakeForce = 20f;
    public float turnSpeed = 50f;
    public float driftFactor = 0.95f;
    public float deceleration = 0.98f;

    private float currentSpeed = 0f;
    private float turnInput;
    private float currentTurn;
    private float accelerationInput;
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
        HandleDrifting();
        //CheckRoadCollision();
    }

    private void HandleInput()
    {
        accelerationInput = Input.GetAxis("Vertical"); // W/S or Up/Down Arrow keys
        turnInput = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow keys
    }

    private void HandleMovement()
    {
        if (accelerationInput > 0) {
            currentSpeed += acceleration * Time.fixedDeltaTime;
        } else if (accelerationInput < 0) {
            currentSpeed -= brakeForce * Time.fixedDeltaTime;
        } else {
            currentSpeed *= deceleration; // Gradual deceleration
        }

        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed / 2, maxSpeed);
        //Debug.Log(currentSpeed);
        rb.linearVelocity = transform.forward * currentSpeed;
    }

    private void HandleTurning()
    {
        if (currentSpeed != 0)
        {
            currentTurn = turnInput * turnSpeed * Time.fixedDeltaTime;
            transform.Rotate(0, currentTurn, 0);
        }
    }

    private void HandleDrifting()
    {
        if (Input.GetKey(KeyCode.Space)) // Space key for drifting
        {
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, transform.forward * currentSpeed, driftFactor * Time.fixedDeltaTime);
        }
    }

    private void CheckRoadCollision()
    {
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f, roadLayer))
        {
            currentSpeed *= 0.5f; // Reduce speed if off-road
        }
    }
}
