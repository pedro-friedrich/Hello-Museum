using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public List<GhostNPC> NpcList;

    // Update is called once per frame
    void Update()
    {
        foreach (GhostNPC Npc in NpcList)
        {
            if (GameManager.Instance.gameState >= Npc.id)
            {
                Npc.gameObject.SetActive(true);
            }
        }
    }
}
