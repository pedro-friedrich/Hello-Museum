using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveWaypoint : MonoBehaviour
{
    public GameObject waypoint;
    public Transform target;
    public TextMeshProUGUI meter;
    public float minX;
    public float minY;

    private Player player;

    void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameState == 0 || GameManager.Instance.gameState == 26)
        {
            waypoint.SetActive(false);
        }
        else
        {
            waypoint.SetActive(true);
        }
        
        target = ObjectiveManager.Instance.GetObjetivoPorId(GameManager.Instance.gameState);

        float maxX = Screen.width - minX;

        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(target.position);

        if (Vector3.Dot(target.position - transform.position, transform.forward) < 0)
        {
            if (pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }

            if (pos.y < Screen.height / 2)
            {
                pos.y = maxY;
            }
            else
            {
                pos.y = minY;
            }
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        waypoint.transform.position = pos;
        meter.text = (Vector3.Distance(target.position, player.transform.position) / 2.5).ToString("0") + "m";
    }
}
