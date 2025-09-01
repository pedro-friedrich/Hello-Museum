using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float floatSpeed;
    public float floatHeight;
    public float dialogueRange;
    public LayerMask playerLayer;
    public bool playerHit;

    private Vector3 startPosition;
    private float timeCounter = 0;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        DetectPlayer();

        if (playerHit)
        {
            timeCounter += Time.deltaTime * floatSpeed;
            float newY = startPosition.y + (Mathf.Sin(timeCounter) * 0.5f + 0.5f) * floatHeight;
            transform.position = new Vector3(startPosition.x, newY, startPosition.z);
        }
    }

    void DetectPlayer()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, dialogueRange, playerLayer);

        if (hit.Length > 0 && !playerHit)
        {
            playerHit = true;
            timeCounter = -Mathf.PI / 2;

            ButtonController.Instance.ActivateBtn("Item", this);
        }
        else if (hit.Length == 0 && playerHit)
        {
            playerHit = false;
            transform.position = startPosition;

            ButtonController.Instance.DeactivateBtn();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}
