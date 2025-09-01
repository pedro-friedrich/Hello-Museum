using UnityEngine;

public class ShapeMovement : MonoBehaviour
{
    public Vector3 direction = new Vector3(0, 0, 0);
    public int id;
    private float speed = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TankMinigame minigame = GameObject.Find("Minigame").GetComponent<TankMinigame>();

        if (minigame.targetIndex == id)
        {
            minigame.Hit(true);
        }
        else
        {
            minigame.Hit(false);
        }

        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
