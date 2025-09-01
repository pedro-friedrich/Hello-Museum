using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private new GameObject camera;

    private void Start() {
        camera = FindAnyObjectByType<Camera>().gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, camera.transform.rotation.eulerAngles.y, 0f);
    }
}
