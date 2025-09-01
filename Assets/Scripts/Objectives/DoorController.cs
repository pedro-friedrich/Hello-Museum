using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int id;

    private GameObject door;
    private bool isOpen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        door = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameState >= id && !isOpen)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        door.transform.eulerAngles += new Vector3(0, 90, 0);
        isOpen = true;
    }
}
