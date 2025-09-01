using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    private Dictionary<TriggerZone, GameObject> triggers = new Dictionary<TriggerZone, GameObject>();

    private void Awake()
    {
        triggers.Clear();

        foreach (Transform child in transform)
        {
            TriggerZone trigger = child.GetComponent<TriggerZone>();
            if (trigger != null)
            {
                triggers[trigger] = child.gameObject;
            }
        }

        DeactivateTriggers();
    }

    public void DeactivateTriggers()
    {
        foreach (var triggerGO in triggers.Values)
        {
            triggerGO.SetActive(false);
        }
    }

    private void Update()
    {
        foreach (var pair in triggers)
        {
            TriggerZone trigger = pair.Key;
            GameObject triggerGO = pair.Value;

            if (GameManager.Instance.gameState == trigger.triggerID)
            {
                triggerGO.SetActive(true);
            }
            else
            {
                triggerGO.SetActive(false);
            }
        }
    }
}
