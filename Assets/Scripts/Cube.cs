using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Rigidbody rb { get; private set; }
    public IStates state { get; private set; }
    public GameObject cube { get; private set; }

    Cube() {
        IdleState state = new IdleState();
        state.SetContext(this);
        this.state = state;
    }

    public void UpdateState(IStates state) {
        this.state = state;
    }

    // Using the Awake function to set the references
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cube = GetComponent<GameObject>();
    }

    void FixedUpdate()
    {
        state.Action();
    }

    void Update() {
        UpdateUI();
    }

    void UpdateUI() {
        switch(state.GetState()) {
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
