using UnityEngine;

public class IdleState : IStates {
    private Cube cube;

    public void Action() {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        if(hAxis != 0 || vAxis != 0) {
            MovingState state = new MovingState();
            state.SetContext(cube);
            cube.UpdateState(state);
            state.UpdateUI();
        } else if(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E)) {
            RotatingState state = new RotatingState();
            state.SetContext(cube);
            cube.UpdateState(state);
            state.UpdateUI();
        }
    }

    public void SetContext(Cube cube) {
        this.cube = cube;
    }

    public void UpdateUI() {
        cube.GetComponent<Renderer>().material.color = Color.white;
    }
}