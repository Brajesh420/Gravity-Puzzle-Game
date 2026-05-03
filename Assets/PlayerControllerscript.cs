using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerControllerscript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    private CharacterController controller;
    public GravityController gravityController;

    private Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float fallTimeLimit = 2f; // seconds before game over
    private float fallTimer = 0f;

    public GameObject gameOverText;

    public Animator animator; // 👈 ADD THIS

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

        Quaternion targetRotation =
            Quaternion.FromToRotation(transform.up, -gravityDir) * transform.rotation;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            10f * Time.deltaTime
        );
    }

    void Move()
    {
        Vector3 gravityDir = gravityController.gravityDirection;

        // Ground check
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundDistance,
            groundMask
        );

        if (!isGrounded)
        {
            fallTimer += Time.deltaTime;

            if (fallTimer >= fallTimeLimit)
            {
                GameOver();
            }
        }
        else
        {
            fallTimer = 0f;
        }

        if (isGrounded && Vector3.Dot(velocity, gravityDir) > 0)
        {
            velocity = gravityDir * 2f;
        }

        // 🔥 WASD Movement
        float x = 0f;
        float z = 0f;

        if (Input.GetKey(KeyCode.A)) x = -1;
        if (Input.GetKey(KeyCode.D)) x = 1;
        if (Input.GetKey(KeyCode.W)) z = 1;
        if (Input.GetKey(KeyCode.S)) z = -1;

        Vector3 forward = Vector3.ProjectOnPlane(transform.forward, gravityDir).normalized;
        Vector3 right = Vector3.Cross(forward, gravityDir).normalized;

        Vector3 move = forward * z + right * x;

        controller.Move(move * moveSpeed * Time.deltaTime);

        // 🔥 Animator speed
        float speed = move.magnitude;
        animator.SetFloat("Speed", speed * 10f);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity = -gravityDir * jumpForce;
        }

        // Gravity
        velocity += gravityDir * 20f * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        // 🔥 Animator jump
        animator.SetBool("IsJumping", !isGrounded);
    }
    void GameOver()
    {
        gameOverText.SetActive(true);

        Time.timeScale = 0f; // freeze game

        enabled = false;
    }
}