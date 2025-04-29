using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class BallmanController : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    [SerializeField]
    private float movementSpeed = 5f, rotationSpeed = 200f, jumpSpeed = 8f, gravity = 20f;

    private Vector3 movementDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Movement Input
        Vector3 inputMovement = transform.forward * movementSpeed * Input.GetAxisRaw("Vertical");
        characterController.Move(inputMovement * Time.deltaTime);

        transform.Rotate(Vector3.up * Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime);

        // Jumping
        if (characterController.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                movementDirection.y = jumpSpeed;
            }
            else
            {
                movementDirection.y = 0f;
            }
        }

        movementDirection.y -= gravity * Time.deltaTime;
        characterController.Move(movementDirection * Time.deltaTime);

        // Animator Parameters
        bool isMoving = Input.GetAxisRaw("Vertical") != 0;
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isJumping", !characterController.isGrounded);
    }
}