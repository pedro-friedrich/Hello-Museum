using UnityEngine;

public class Plane : MonoBehaviour
{
    public float moveSpeed;

    private Vector3 targetPosition;
    private bool isMoving = false;
    private PlaneMinigame minigame;
    private Animator anim;

    void Start()
    {
        targetPosition = transform.position;
        minigame = FindFirstObjectByType<PlaneMinigame>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0) && minigame.Playing)
        {
            SetTargetPosition(Input.mousePosition);
        }
#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && minigame.Playing)
        {
            SetTargetPosition(Input.GetTouch(0).position);
        }
#endif
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Se chegou perto o suficiente, para de mover
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f || !Input.GetMouseButton(0) || Input.touchCount == 0)
            {
                isMoving = false;
            }
        }
    }

    void SetTargetPosition(Vector3 screenPosition)
    {
        float zDist = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        screenPosition.z = zDist;

        targetPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        isMoving = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            anim.SetTrigger("Hit");
            minigame.Hit();
        }
    }
}
