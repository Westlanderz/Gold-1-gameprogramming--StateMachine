using UnityEngine;

public class JumpingState : IStates {
    private Cube cube;
    private float jumpHeight = 5f;         // How high Player jumps
    private bool jumped = false;           // Has the player jumped?

    public void Action() {
        if(!jumped) {
            cube.rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            jumped = true;
        }
        if(cube.rb.velocity.y == 0f && cube.rb.velocity.z == 0f && cube.rb.velocity.x == 0f) {
            IdleState state = new IdleState();
            state.SetContext(cube);
            cube.UpdateState(state);
            jumped = false;
            state.UpdateUI();
        }
    }

    public void SetContext(Cube cube) {
        this.cube = cube;
    }

    public void UpdateUI() {
        cube.GetComponent<Renderer>().material.color = Color.blue;
    }
}