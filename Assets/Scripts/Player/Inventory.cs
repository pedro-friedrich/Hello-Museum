using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    public bool hasFlashlight;
    public int fuses;
    public Transform fusesParent;

    private GameObject flashLight;
    private List<GameObject> fuseList;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flashLight = transform.Find("Light").gameObject;

        fuseList = new List<GameObject>();
        if (GameManager.Instance != null)
        {
            hasFlashlight = GameManager.Instance.hasFlashlight;
            fuses = GameManager.Instance.fuses;
        }

        foreach (Transform fuse in fusesParent)
        {
            fuseList.Add(fuse.gameObject);
            fuse.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hasFlashlight)
        {
            flashLight.SetActive(true);
        }

        if (GameManager.Instance.gameState >= 5)
        {
            hasFlashlight = true;
        }

        for (int i = 0; i < fuses; i++)
        {
            if (!fuseList[i].activeSelf)
            {
                fuseList[i].SetActive(true);
            }  
        }
    }

    public void NewFuse()
    {
        fuses++;
    }
}
