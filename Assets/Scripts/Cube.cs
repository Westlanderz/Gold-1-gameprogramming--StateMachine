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
        state.UpdateUI();
    }

    void FixedUpdate()
    {
        state.Action();
    }
}
