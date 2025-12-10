using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 10f;
    public float jumpForce = 8f;
    public float gravity = -20f;

    float laneDistance = 2f;

    private int currentLane = 1;
    private CharacterController controller;
    private float verticalVelocity = 0f;
    private Vector3 targetPosition;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentLane = 1;
        UpdateTargetPosition();
    }

    void Update()
    {
        float currentForwardSpeed = (GameSpeedManager.Instance != null)
            ? GameSpeedManager.Instance.CurrentSpeed
            : forwardSpeed;

        Vector3 move = Vector3.forward * currentForwardSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            ChangeLane(-1);
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            ChangeLane(1);

        float newX = Mathf.MoveTowards(transform.position.x, targetPosition.x, 10f * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && (controller.isGrounded || verticalVelocity == 0f))
        {
            verticalVelocity = jumpForce;
        }

        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -1f;

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 finalMove = new Vector3(newX - transform.position.x, verticalVelocity * Time.deltaTime, move.z);
        controller.Move(finalMove);
    }

    void ChangeLane(int dir)
    {
        currentLane = Mathf.Clamp(currentLane + dir, 0, 2);
        UpdateTargetPosition();
    }

    void UpdateTargetPosition()
    {
        float x = (currentLane - 1) * laneDistance;
        targetPosition = new Vector3(x, transform.position.y, transform.position.z);
    }
}