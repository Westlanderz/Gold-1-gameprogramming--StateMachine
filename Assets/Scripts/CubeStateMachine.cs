using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeStateMachine : MonoBehaviour
{
    [SerializeField] private CubeState currentState;
    
    Rigidbody rb;
    [SerializeField] private float moveSpeed = 2f;            // Player's speed when walking.
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float jumpHeight = 5f;         // How high Player jumps

    Vector3 moveDirection;

    // Using the Awake function to set the references
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
        Rotate();
        JumpMove();
        UpdateUI();
        if(rb.position.y <= 0.6f)
            currentState = CubeState.Idle;
        else
            currentState = CubeState.Jumping;
    }

    void Move ()
    {
        if(currentState != CubeState.Jumping) {
            float hAxis = Input.GetAxisRaw("Horizontal");
            float vAxis = Input.GetAxisRaw("Vertical");

            if(hAxis != 0 || vAxis != 0 && currentState != CubeState.Jumping)
                currentState = CubeState.Moving;

            Vector3 movement = new Vector3(hAxis, 0f, vAxis);
            rb.position += movement * moveSpeed * Time.deltaTime;   
        }
    }

    // Rotate the cube left if q is pressed and right when e is pressed
    void Rotate() {
        if(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E)) {
            if(Input.GetKey(KeyCode.Q))
                transform.Rotate(new Vector3(rotationSpeed, 0f, 0f));
            else if(Input.GetKey(KeyCode.E))
                transform.Rotate(new Vector3(-rotationSpeed, 0f, 0f));
            currentState = CubeState.Rotating;
        }
    }

    // check if the player is on the ground and moving, when space is pressed jump
    void JumpMove() {
        if (currentState == CubeState.Moving && Input.GetKey(KeyCode.Space) && rb.velocity.y <= 0f) {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            currentState = CubeState.Jumping;
        }
    }

    void UpdateUI() {
        switch(currentState) {
            case CubeState.Idle:
                GetComponent<Renderer>().material.color = Color.white;
                break;
            case CubeState.Moving:
                GetComponent<Renderer>().material.color = Color.green;
                break;
            case CubeState.Rotating:
                GetComponent<Renderer>().material.color = Color.black;
                break;
            case CubeState.Jumping:
                GetComponent<Renderer>().material.color = Color.blue;
                break;
        }
    }
}

public enum CubeState {
    Idle,
    Moving,
    Rotating,
    Jumping,
}
