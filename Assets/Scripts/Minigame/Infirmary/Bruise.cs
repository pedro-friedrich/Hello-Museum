using UnityEngine;
using UnityEngine.EventSystems;

public class Bruise : MonoBehaviour, IDropHandler
{
    public string bruiseID;
    
    private InfirmaryMinigame minigame;

    private void Start()
    {
        minigame = FindFirstObjectByType<InfirmaryMinigame>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            string objID = eventData.pointerDrag.gameObject.GetComponent<DragMedicine>().objID;

            if (objID == bruiseID)
            {
                minigame.score++;
                Destroy(gameObject);
            }
            else
            {
                minigame.Hit();
            }
        }
    }
}
