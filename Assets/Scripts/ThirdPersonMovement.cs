using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    private CharacterController controller = null;
    private PlayerInputsManager playerInputsManager = null;
    public Transform cam;

    public float speed = 6f;
    [SerializeField]
    private float sprintSpeed = 10f;
    [SerializeField]
    private float walkSpeed = 6f;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public float gravity = -14f;
    private Vector3 velocity;
    public float jump = 1.5f;

    void Start()
    {
        Cursor.visible = false; //hide cursor
        Cursor.lockState = CursorLockMode.Locked; // cursor in middle

        controller = GetComponent<CharacterController>();
        playerInputsManager = GetComponent<PlayerInputsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // shows cursor and unlocks it 
        {
            Cursor.visible = true; //show cursor
            Cursor.lockState = CursorLockMode.None; // cursor in middle
        }

        var targetDir = new Vector3(playerInputsManager.move.x, 0, playerInputsManager.move.y);
        speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        controller.Move(targetDir * (speed * Time.deltaTime));
        
        speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        //jumping
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jump * -2.0f * gravity);
        }

        controller.Move(velocity * Time.deltaTime);
    }
}
