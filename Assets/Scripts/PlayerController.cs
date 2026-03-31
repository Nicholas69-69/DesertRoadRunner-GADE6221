using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Lane Settings")]
    public float leftLane = -4f;
    public float middleLane = 0f;
    public float rightLane = 4f;

    [Header("Movement")]
    public float forwardSpeed = 15f;
    public float laneSwitchSpeed = 10f;
    public float jumpForce = 8f;

    private int currentLane = 1;
    private float[] lanePositions;
    private Rigidbody rb;
    private bool isGrounded = true;

    void Start()
    {
        // Gets the Rigidbody attached to the player
        rb = GetComponent<Rigidbody>();

        // Stores the three lane positions in an array
        lanePositions = new float[] { leftLane, middleLane, rightLane };
    }

    void Update()
    {
        // Moves the player left between lanes
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            if (currentLane > 0) currentLane--;

        // Moves the player right between lanes
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            if (currentLane < 2) currentLane++;

        // Makes the player jump if on the ground
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // Calculates the target position for the current lane
        Vector3 targetPosition = new Vector3(
            lanePositions[currentLane],
            transform.position.y,
            transform.position.z
        );

        // Smoothly moves the player to the selected lane
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * laneSwitchSpeed);

        // Constantly moves the player forward
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Resets jumping when the player touches the ground
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    public void SpeedBoost(float duration)
    {
        // Starts the speed boost effect
        StartCoroutine(SpeedBoostCoroutine(duration));
    }

    public void SlowMotion(float duration)
    {
        // Starts the slow motion effect
        StartCoroutine(SlowMotionCoroutine(duration));
    }

    IEnumerator SpeedBoostCoroutine(float duration)
    {
        // Increases player speed for a short time
        forwardSpeed *= 1.5f;
        yield return new WaitForSeconds(duration);
        forwardSpeed /= 1.5f;
    }

    IEnumerator SlowMotionCoroutine(float duration)
    {
        // Slows the whole game down for a short time
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }
}