using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;

public class TankControll : MonoBehaviour
{
    public float moveSpeed;
    public GameObject cannon;
    public GameObject shootPrefab;
    public GameObject canvas;
    public Transform shootSpawn;
    public LayerMask targetLayer;

    private bool isMoving = false;
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject())
        {
            SetTargetPosition(Input.mousePosition);
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
#else
    if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject())
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        {
            SetTargetPosition(touch.position);
            isMoving = true;
        }
        else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            isMoving = false;
        }
    }
    else
    {
        isMoving = false;
    }
#endif
        RaycastHit2D hit = Physics2D.Raycast(targetPosition, Vector3.zero, 0f, targetLayer);
        if (isMoving && !hit.collider)
        {
            Vector3 dirToTarget = (targetPosition - transform.position).normalized;
            float angleToTarget = Vector2.SignedAngle(transform.up, dirToTarget);

            if (angleToTarget > 0f)
            {
                transform.Rotate(0f, 0f, moveSpeed * Time.deltaTime);
            }
            else if (angleToTarget < 0f)
            {
                transform.Rotate(0f, 0f, -moveSpeed * Time.deltaTime);
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

    public void Shoot()
    {
        Instantiate(shootPrefab, shootSpawn.position, Quaternion.identity, canvas.transform);
    }
}
