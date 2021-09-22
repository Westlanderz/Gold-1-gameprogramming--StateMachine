using UnityEngine;

public class MovingState : IStates {
    private Cube cube;

    private float moveSpeed = 2f;            // Player's speed when walking.

    public void Action() {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        if(hAxis != 0 || vAxis != 0) {
            Vector3 movement = new Vector3(hAxis, 0f, vAxis);
            cube.rb.position += movement * moveSpeed * Time.deltaTime;   
            if (Input.GetKey(KeyCode.Space) && cube.rb.velocity.y <= 0f) {
                JumpingState state = new JumpingState();
                state.SetContext(cube);
                cube.UpdateState(state);
                state.UpdateUI();
            }
        } else {
            if(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E)) {
                RotatingState state = new RotatingState();
                state.SetContext(cube);
                cube.UpdateState(state);
                state.UpdateUI();
            } else if(hAxis == 0 || vAxis == 0 && cube.rb.velocity.z == 0f && cube.rb.velocity.x == 0f) {
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
        cube.GetComponent<Renderer>().material.color = Color.green;
    }
}