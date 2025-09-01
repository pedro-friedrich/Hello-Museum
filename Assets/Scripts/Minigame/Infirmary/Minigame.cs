using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class InfirmaryMinigame : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject endgamePanel;
    public TextMeshProUGUI endgameText;
    public Text missionText;
    public Transform livesObj;
    public int lives;
    public List<RectTransform> slots;
    public List<GameObject> bruisePrefabs;
    [HideInInspector] public int score = 0;

    private List<GameObject> lifes = new List<GameObject>();
    private bool playing;

    public bool Playing
    {
        get => playing;
        set => playing = value;
    }

    void Start()
    {
        foreach (Transform life in livesObj)
        {
            lifes.Add(life.gameObject);
        }
    }

    private void Update()
    {
        if (score >= 4 && playing)
        {
            Win();
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Game");
    }

    public void StartGame()
    {
        lives = 3;
        score = 0;

        GameObject[] bruises = GameObject.FindGameObjectsWithTag("Bruise");
        foreach (GameObject bruise in bruises)
        {
            Destroy(bruise);
        }

        foreach (Transform life in livesObj)
        {
            life.gameObject.SetActive(true);
            lifes.Add(life.gameObject);
        }

        playing = true;
        menuPanel.SetActive(false);
        endgamePanel.SetActive(false);

        List<RectTransform> availableSlots = new List<RectTransform>(slots);
        List<GameObject> availableBruises = new List<GameObject>(bruisePrefabs);

        for (int i = 0; i < bruisePrefabs.Count; i++)
        {
            int slotIndex = Random.Range(0, availableSlots.Count);
            RectTransform chosenSlot = availableSlots[slotIndex];
            availableSlots.RemoveAt(slotIndex);

            int bruiseIndex = Random.Range(0, availableBruises.Count);
            GameObject bruisePrefab = availableBruises[bruiseIndex];
            availableBruises.RemoveAt(bruiseIndex);

            GameObject newBruise = Instantiate(bruisePrefab, chosenSlot.position, Quaternion.identity);
            newBruise.transform.SetParent(chosenSlot);
        }
    }

    public void Hit()
    {
        lives--;
        lifes[lifes.Count - 1].SetActive(false);
        lifes.RemoveAt(lifes.Count - 1);
        if (lives <= 0)
        {
            Lose();
        }
    }

    public void EndGame(bool victory)
    {
        endgamePanel.SetActive(true);

        if (victory)
        {
            endgameText.text = "Vitória!";
        }
        else
        {
            endgameText.text = "Derrota";
        }
    }

    void Win()
    {
        playing = false;
        EndGame(true);

        if (GameManager.Instance.gameState == 18)
        {
            GameManager.Instance.gameState++;
        }
    }

    void Lose()
    {
        playing = false;
        EndGame(false);
    }
}