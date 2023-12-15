using System;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    
    [SerializeField] private float mouseSensitivity = 50f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float airControl = 0.4f;
    
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Animator animator = null;

    private bool IsGrounded => Physics.CheckSphere(groundCheck.position, 0.4f, groundMask);
    
    private CharacterController controller = null;
    private PlayerInputsManager playerInputsManager = null;
    
    private float xRotation = 0f;
    private float yRotation = 0f;

    public float gravity = -10f;
    
    [SerializeField] public float maxJump = 16f;
    [SerializeField] public float minJump = 1f;
    [SerializeField] public float jumpChargeRate = 8f;
    private float jumpCharge = 0f;
    private Vector3 velocity;

    private void Start()
    {
        Cursor.visible = false; //hide cursor
        Cursor.lockState = CursorLockMode.Locked; // cursor in middle

        controller = GetComponent<CharacterController>();
        playerInputsManager = GetComponent<PlayerInputsManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // shows cursor and unlocks it 
        {
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
        }

        PlayerMovement();
        JumpAndGravity();

        UpdateAnimator();
    }

    private void LateUpdate() {
        CameraRotation();
    }

    private void PlayerMovement() {
        var inputDirection = new Vector3(playerInputsManager.move.x, 0, playerInputsManager.move.y);
        var targetRotation = 0f;
        var moveSpeed = 0f;
        
        if (playerInputsManager.move != Vector2.zero) {
            moveSpeed = playerInputsManager.sprint ? sprintSpeed : walkSpeed;
            targetRotation = Quaternion.LookRotation(inputDirection).eulerAngles.y + mainCamera.transform.rotation.eulerAngles.y;

            var rotation = Quaternion.Euler(0f, targetRotation, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 12 * Time.deltaTime);
        }
        
        var targetDir = Quaternion.Euler(0, targetRotation, 0) * Vector3.forward;
        
        if (!IsGrounded) {
            targetDir.x *= airControl;
            targetDir.z *= airControl;
        }
        
        controller.Move(targetDir * (moveSpeed * Time.deltaTime));
    }

    private void CameraRotation() {
        var sensitivity = mouseSensitivity / 100f;
        
        xRotation += playerInputsManager.look.y * sensitivity;
        yRotation += playerInputsManager.look.x * sensitivity;
        xRotation = Mathf.Clamp(xRotation, -30f, 30f);
        
        cameraTarget.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    private void JumpAndGravity() {
        velocity.y += gravity * Time.deltaTime;

        if (playerInputsManager.jump && IsGrounded) 
        {
            if (jumpCharge < maxJump)
            {
                jumpCharge += jumpChargeRate * Time.fixedDeltaTime;
            }
        }
        else if (jumpCharge > minJump && !playerInputsManager.jump && IsGrounded) 
        {
            velocity.y = Mathf.Sqrt(jumpCharge * -2f * gravity);
            playerInputsManager.jump = false;
            jumpCharge = minJump;
        }
        else {
            jumpCharge = minJump;
        }
        
        controller.Move(velocity * Time.deltaTime);
    }

    private void UpdateAnimator() {
        animator.SetFloat("Speed", playerInputsManager.move.magnitude);
        animator.SetBool("IsGrounded", IsGrounded);
    }
}
