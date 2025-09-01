using System.Collections.Generic;
using UnityEngine;

public class TankGatesController : MonoBehaviour
{
    public List<GameObject> gateParts = new List<GameObject>();

    private bool open;

    void Update()
    {
        if (GameManager.Instance.gameState >= 14 && !open)
        {
            foreach (GameObject gate in gateParts)
            {
                gate.transform.eulerAngles -= new Vector3(45, 0, 0);
                open = true;
            }
        }
    }
}
