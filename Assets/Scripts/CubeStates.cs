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
        } else if(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E)) {
            RotatingState state = new RotatingState();
            state.SetContext(cube);
            cube.UpdateState(state);
        }
    }

    public CubeState GetState() {
        return CubeState.Idle;
    }

    public void SetContext(Cube cube) {
        this.cube = cube;
    }
}

public class MovingState : IStates {
    private Cube cube;

    private float moveSpeed = 2f;            // Player's speed when walking.

    public void Action() {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(hAxis, 0f, vAxis);
        cube.rb.position += movement * moveSpeed * Time.deltaTime;   

        if (Input.GetKey(KeyCode.Space) && cube.rb.velocity.y <= 0f) {
            JumpingState state = new JumpingState();
            state.SetContext(cube);
            cube.UpdateState(state);
        } else if(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E)) {
            RotatingState state = new RotatingState();
            state.SetContext(cube);
            cube.UpdateState(state);
        } else if(hAxis == 0 || vAxis == 0 && cube.rb.velocity.z <= 0f && cube.rb.velocity.x <= 0f) {
            IdleState state = new IdleState();
            state.SetContext(cube);
            cube.UpdateState(state);
        }
    }

    public CubeState GetState() {
        return CubeState.Moving;
    }

    public void SetContext(Cube cube) {
        this.cube = cube;
    }
}

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
            } else {
                IdleState state = new IdleState();
                state.SetContext(cube);
                cube.UpdateState(state);
            }
        }
        
    }

    public CubeState GetState() {
        return CubeState.Rotating;
    }

    public void SetContext(Cube cube) {
        this.cube = cube;
    }
}

public class JumpingState : IStates {
    private Cube cube;
    private float jumpHeight = 5f;         // How high Player jumps
    private bool jumped = false;           // Has the player jumped?

    public void Action() {
        if(!jumped) {
            cube.rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            jumped = true;
        }
        if(cube.rb.position.y <= 0.6f && cube.rb.velocity.y == 0f) {
            IdleState state = new IdleState();
            state.SetContext(cube);
            cube.UpdateState(state);
            jumped = false;
        }
    }

    public CubeState GetState() {
        return CubeState.Jumping;
    }

    public void SetContext(Cube cube) {
        this.cube = cube;
    }
}