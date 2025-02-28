using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputsManager : MonoBehaviour {

    public Vector2 move;
    public Vector2 look;
    public bool sprint;
    public bool jump;
    
    void OnMove(InputValue value) {
        move = value.Get<Vector2>();
    }

    void OnLook(InputValue value) {
        look = value.Get<Vector2>();
    }
    
    void OnSprint(InputValue value) {
        sprint = value.isPressed;
    }
    
    void OnJump(InputValue value) {
        jump = value.isPressed;
    }
}
