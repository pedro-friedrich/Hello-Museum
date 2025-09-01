using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Material lanternMaterial;
    private Vector2 screenSize;

    void Start()
    {
        screenSize = new Vector2(Screen.width, Screen.height);
        lanternMaterial.SetFloat("_AspectRatio", (float)screenSize.x / (float)screenSize.y);
    }

    void Update()
    {
        Vector2 pos;
#if UNITY_EDITOR
        pos = Input.mousePosition;
#else
        if (Input.touchCount > 0)
            pos = Input.GetTouch(0).position;
        else
            return;
#endif

        Vector2 normalizedPos = new Vector2(pos.x / screenSize.x, pos.y / screenSize.y);
        lanternMaterial.SetVector("_MaskPosition", new Vector4(normalizedPos.x, normalizedPos.y, 0, 0));
    }
}
