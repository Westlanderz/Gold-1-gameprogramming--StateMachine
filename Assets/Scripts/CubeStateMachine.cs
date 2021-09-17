using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeStateMachine : MonoBehaviour
{
    [SerializeField]private CubeState currentState;
    [SerializeField] float speed;
    
    Rigidbody rb;
    void Start() {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update() {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 moveBy = transform.right * x + transform.forward * z;
        rb.MovePosition(transform.position + moveBy.normalized * speed * Time.deltaTime);
    }

    bool isInState(CubeState state) {
        return currentState == state;
    }
}

public enum CubeState {
    Idle,
    Moving,
    Rotating,
    Jumping,
}
