using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerControllerscript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float gravity = -9.81f;

    private CharacterController controller;
    public GravityController gravityController;
    private Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        AlignToGravity();
        Move();
    }

    void AlignToGravity()
    {
        Vector3 gravityDir = gravityController.gravityDirection;

        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, -gravityDir) * transform.rotation;

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
    }
    void Move()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal"); // A/D
        float z = Input.GetAxis("Vertical");   // W/S

        Vector3 forward = Vector3.Cross(gravityController.gravityDirection, transform.right).normalized;
        Vector3 right = Vector3.Cross(gravityController.gravityDirection, forward).normalized;

        Vector3 move = right * x + forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        velocity += gravityController.gravityDirection * gravityController.gravityStrength * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}