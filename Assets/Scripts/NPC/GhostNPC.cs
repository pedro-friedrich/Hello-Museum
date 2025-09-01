using UnityEngine;

public class GhostNPC : MonoBehaviour
{
    public float floatSpeed;
    public float floatHeight;
    public int id;
    private Vector3 startLocalPosition;
    private float timeCounter = 0;

    void Start()
    {
        startLocalPosition = transform.localPosition;   
    }

    void Update()
    {
        moveUpDown();
    }

    private void moveUpDown() {
        timeCounter += Time.deltaTime * floatSpeed;
        float newY = startLocalPosition.y + (Mathf.Sin(timeCounter) * 0.5f + 0.5f) * floatHeight;
        transform.localPosition = new Vector3(startLocalPosition.x, newY, startLocalPosition.z);
    }
}
