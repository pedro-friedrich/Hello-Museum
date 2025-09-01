using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;
    public GameObject endgamePanel;
    public Player player;

    [System.Serializable]
    public class Objetivo
    {
        public int id;
        public Transform target;
    }

    public List<Objetivo> objectiveList;

    private Dictionary<int, Transform> objectives;

    void Awake()
    {
        endgamePanel.SetActive(false);

        Instance = this;

        objectives = new Dictionary<int, Transform>();

        foreach (var obj in objectiveList)
        {
            if (obj.target != null && !objectives.ContainsKey(obj.id))
            {
                objectives.Add(obj.id, obj.target);
            }
        }
    }

    public Transform GetObjetivoPorId(int id)
    {
        objectives.TryGetValue(id, out Transform target);
        return target;
    }

    
    public void TriggerEndgame(bool show)
    {
        endgamePanel.SetActive(show);
        player.IsTalking = show;
        player.joystick.OnPointerUp(null);
    }
}
