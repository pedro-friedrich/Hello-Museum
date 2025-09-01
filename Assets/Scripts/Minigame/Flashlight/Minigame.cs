using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class FlashlightMinigame : MonoBehaviour
{
    public List<Transform> slots;
    public List<GameObject> itemPrefabs;
    public GameObject menuPanel;
    public GameObject endgamePanel;
    public TextMeshProUGUI endgameText;
    public Text itemNameText;
    public Transform livesObj;
    public int lives;

    private List<GameObject> instantiatedItems = new List<GameObject>();
    private List<GameObject> lifes = new List<GameObject>();
    private Queue<string> itemQueue = new Queue<string>();
    private string currentItem;
    private bool playing = true;

    public bool Playing { get => playing; set => playing = value; }

    void Start()
    {
        foreach (Transform life in livesObj)
        {
            lifes.Add(life.gameObject);
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Game");
    }

    public void StartGame()
    {
        lives = 3;
        foreach (Transform life in livesObj)
        {
            life.gameObject.SetActive(true);
            lifes.Add(life.gameObject);
        }
        playing = true;
        menuPanel.SetActive(false);
        endgamePanel.SetActive(false);

        List<Transform> availableSlots = new List<Transform>(slots);
        List<GameObject> availableItems = new List<GameObject>(itemPrefabs);

        for (int i = 0; i < 5; i++)
        {
            int slotIndex = Random.Range(0, availableSlots.Count);
            Transform chosenSlot = availableSlots[slotIndex];
            availableSlots.RemoveAt(slotIndex);

            int itemIndex = Random.Range(0, availableItems.Count);
            GameObject itemPrefab = availableItems[itemIndex];
            availableItems.RemoveAt(itemIndex);

            GameObject newItem = Instantiate(itemPrefab, chosenSlot.position, Quaternion.identity);
            newItem.transform.SetParent(chosenSlot);
            instantiatedItems.Add(newItem);

            itemQueue.Enqueue(itemPrefab.name);

            newItem.AddComponent<ItemClickable>().Init(this, itemPrefab.name);
        }

        NextItem();
    }

    public void EndGame(bool victory)
    {
        endgamePanel.SetActive(true);

        if (victory)
        {
            endgameText.text = "Vitória!";
        }
        else{
            endgameText.text = "Derrota";
        }
    }

    public void NextItem()
    {
        if (itemQueue.Count > 0)
        {
            currentItem = itemQueue.Dequeue();
            itemNameText.text = "Find: " + currentItem;
        }
        else
        {
            Win();
        }
    }

    public void ClickItem(string itemName, ItemClickable currentItem)
    {
        if (itemName == this.currentItem)
        {
            Destroy(currentItem.gameObject);
            NextItem();
        }
        else
        {
            lives--;
            lifes[lifes.Count -1].SetActive(false);
            lifes.RemoveAt(lifes.Count - 1);
            if (lives <= 0)
            {
                Lose();
            }
        }
    }

    void Win()
    {
        itemNameText.text = "You Win!";
        playing = false;
        EndGame(true);

        if (GameManager.Instance.gameState == 3)
        {      
            GameManager.Instance.gameState++;
        }
    }

    void Lose()
    {
        itemNameText.text = "You Lose!";
        playing = false;
        EndGame(false);
        itemQueue.Clear();
        foreach (GameObject obj in instantiatedItems)
        {
            Destroy(obj);
        }
    }
}
