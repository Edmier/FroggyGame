using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputsManager : MonoBehaviour {

    public Vector2 move;
    
    void OnMove(InputValue value) {
        move = value.Get<Vector2>();
    }
}
