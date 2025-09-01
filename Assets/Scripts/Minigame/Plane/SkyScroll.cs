using UnityEngine;
using UnityEngine.UI;

public class SkyScroll : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private float _y;

    void Update()
    {
        _img.uvRect = new Rect(_img.uvRect.position + new Vector2(0, _y) * Time.deltaTime, _img.uvRect.size);
    }
}
