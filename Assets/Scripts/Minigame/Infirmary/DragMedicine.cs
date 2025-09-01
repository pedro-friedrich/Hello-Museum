using UnityEngine;
using UnityEngine.EventSystems;

public class DragMedicine : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Canvas canvas;
    public string objID;

    private RectTransform rectTransform;
    private Vector3 startPos;
    private CanvasGroup canvasGroup;
    private GameObject[] bruises;
    private InfirmaryMinigame minigame;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        minigame = FindFirstObjectByType<InfirmaryMinigame>();
        startPos = rectTransform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .7f;

        bruises = GameObject.FindGameObjectsWithTag("Bruise");

        foreach (GameObject bruise in bruises)
        {
            Bruise bruiseScript = bruise.GetComponent<Bruise>();
            if (objID == bruiseScript.bruiseID)
            {
                GameObject targetBruise = bruiseScript.gameObject;
                string bodyPart = targetBruise.transform.parent.name;

                minigame.missionText.text = bodyPart;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        rectTransform.position = startPos;
        minigame.missionText.text = "";
    }
}
