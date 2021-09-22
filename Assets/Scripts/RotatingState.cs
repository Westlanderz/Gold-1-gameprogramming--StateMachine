using UnityEngine;

public class RotatingState : IStates {
    private Cube cube;

    private float rotationSpeed = 2f;

    public void Action() {
        if(Input.GetKey(KeyCode.Q))
            cube.transform.Rotate(new Vector3(rotationSpeed, 0f, 0f));
        else if(Input.GetKey(KeyCode.E))
            cube.transform.Rotate(new Vector3(-rotationSpeed, 0f, 0f));
        else {
            float hAxis = Input.GetAxisRaw("Horizontal");
            float vAxis = Input.GetAxisRaw("Vertical");

            if(hAxis != 0 || vAxis != 0){
                MovingState state = new MovingState();
                state.SetContext(cube);
                cube.UpdateState(state);
                state.UpdateUI();
            } else {
                IdleState state = new IdleState();
                state.SetContext(cube);
                cube.UpdateState(state);
                state.UpdateUI();
            }
        }
        
    }

    public void SetContext(Cube cube) {
        this.cube = cube;
    }

    public void UpdateUI() {
        cube.GetComponent<Renderer>().material.color = Color.black;
    }
}
