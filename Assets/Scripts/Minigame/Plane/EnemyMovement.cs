using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Vector3 direction = new Vector3(0, 0, 0);
    public float speed = 50f;

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
