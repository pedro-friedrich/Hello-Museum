using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    public int triggerID;
    public bool isActive;

    public UnityEvent onEnter;
    public UnityEvent onExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onExit.Invoke();
        }
    }

    public void NextState()
    {
        GameManager.Instance.gameState++;
    }
}
