using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectableWords : MonoBehaviour, IPointerClickHandler
{
    public string word;
    
    private GameObject answerSlot;

    public void OnPointerClick(PointerEventData eventData)
    {
        answerSlot.GetComponent<TextMeshProUGUI>().text += word + " ";
        Destroy(gameObject);
    }

    void Awake()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = word;
        answerSlot = GameObject.Find("Answer Slot");
    }
}
