using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private bool isJumping;
    private PlayerInput playerInput; // Cambia a la clase generada
    public float jumpForce = 5f;
    public Rigidbody rb;

    public PlayerJoystick joystick;
    private Vector2 MoveInput2d;


    [SerializeField] private Animator animator;
    private void Awake()
    {
        playerInput = new PlayerInput(); // Usa la clase generada
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
        playerInput.Player.Jump.performed += OnJump;
        playerInput.Player.Movement.performed += OnMove;
        playerInput.Player.Movement.canceled += OnMoveCanceled;


    }

    private void OnDisable()
    {
        playerInput.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetInteger("MoveAnimatorVariable", 2);
            isJumping = true;
            StartCoroutine(JumpRoutine());
        }
    }

    private void Update()
    {
        MovePlayer();
        Move();

        if (isJumping)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = false;
        }
    }

    private void MovePlayer()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    public void ButtonJump()
    {
        StartCoroutine(JumpRoutine());
        Debug.Log("saltó");
    }

    IEnumerator JumpRoutine()
    {
        animator.SetInteger("JumpVariable", 2);
        yield return new WaitForSeconds(1);
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isJumping = true;
        yield return new WaitForSeconds(1);
        animator.SetInteger("JumpVariable", 0);

    }

    public void Move()
    {
        animator.SetInteger("MoveAnimatorVariable", (int)joystick.inputVector.x);
        Vector3 movement = new Vector3(joystick.inputVector.x, 0, joystick.inputVector.y) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    public void OnJoystickMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
