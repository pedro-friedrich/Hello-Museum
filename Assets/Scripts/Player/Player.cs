using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public DynamicJoystick joystick;
    public Vector3 moveInput;
    private float initialMoveSpeed;
    private GameObject flashlight;
    private Rigidbody rb;
    private bool _isTalking = false;

    public bool IsTalking { get => _isTalking; set => _isTalking = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        initialMoveSpeed = moveSpeed;
        flashlight = transform.Find("Light")?.gameObject;

        if (GameManager.Instance != null && !GameManager.Instance.firstStart)
        {
            transform.position = GameManager.Instance.playerPosition;
            transform.rotation = Quaternion.Euler(GameManager.Instance.playerRotation);
        }
    }

    private void Update()
    {
        if (IsTalking)
        {
            joystick.gameObject.SetActive(false);
            moveSpeed = 0;
        }
        else
        {
            joystick.gameObject.SetActive(true);
            moveSpeed = initialMoveSpeed;
        }

        float moveX = joystick.Horizontal;
        float moveZ = joystick.Vertical;

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        moveInput = (forward * moveZ + right * moveX).normalized;
        if (moveInput.magnitude <= 0f)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        if (flashlight != null)
        {
            if (flashlight.activeSelf && moveInput.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveInput);
                flashlight.transform.rotation = Quaternion.Slerp(flashlight.transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
    }

    private void FixedUpdate()
    {
        if (moveInput.magnitude > 0.01f && !IsTalking)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * .75f);

            Vector3 adjustedMove = moveInput;

            RaycastHit hit;
            if (Physics.Raycast(rb.position, moveInput, out hit, 0.6f))
            {
                adjustedMove = Vector3.ProjectOnPlane(moveInput, hit.normal);
            }

            Vector3 newPosition = rb.position + adjustedMove * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }
    }

    public void EndGame()
    {
        Destroy(GameManager.Instance);
        SceneManager.LoadScene("Main Menu");
    }
}
