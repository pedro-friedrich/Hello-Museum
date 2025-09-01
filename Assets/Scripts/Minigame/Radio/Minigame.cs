using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class RadioMinigame : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject endgamePanel;
    public TextMeshProUGUI endgameText;
    public Transform livesObj;
    public int lives;
    public GameObject wordPrefab;
    public List<RectTransform> slots;
    public List<string> sentences;
    public List<string> translatedSentences;
    public List<string> randomWords;
    [HideInInspector] public int score = 0;

    private List<GameObject> lifes = new List<GameObject>();
    private List<string> usedWords = new List<string>();
    private List<RectTransform> usedSlots = new List<RectTransform>();
    private bool playing;
    private int sentenceIndex;

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
        score = 0;

        foreach (Transform life in livesObj)
        {
            life.gameObject.SetActive(true);
            lifes.Add(life.gameObject);
        }
        playing = true;
        menuPanel.SetActive(false);
        endgamePanel.SetActive(false);

        LoadWords();
    }

    public void LoadWords()
    {
        usedWords.Clear();
        usedSlots.Clear();

        foreach (RectTransform slot in slots)
        {
            if (slot.transform.childCount > 0)
            {
                Destroy(slot.transform.GetChild(0).gameObject);
            }
        }

        GameObject answerSlot = GameObject.Find("Answer Slot");
        GameObject questionSlot = GameObject.Find("Question Slot");
        answerSlot.GetComponent<TextMeshProUGUI>().text = "";

        sentenceIndex = Random.Range(0, sentences.Count);
        questionSlot.GetComponent<TextMeshProUGUI>().text = "";
        questionSlot.GetComponent<TextMeshProUGUI>().text = sentences[sentenceIndex];

        string[] words = translatedSentences[sentenceIndex].Split(' ');

        List<RectTransform> avaliableSlots = new List<RectTransform>(slots);

        foreach (string word in words)
        {
            int slotIndex = Random.Range(0, avaliableSlots.Count);

            GameObject currentWord = Instantiate(wordPrefab, avaliableSlots[slotIndex].position, Quaternion.identity, avaliableSlots[slotIndex]);
            currentWord.GetComponent<SelectableWords>().word = word;
            currentWord.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = word;

            usedWords.Add(word);
            usedSlots.Add(avaliableSlots[slotIndex]);

            avaliableSlots.RemoveAt(slotIndex);
        }

        List<string> avaliablewords = new List<string>(randomWords);
        foreach (RectTransform slot in avaliableSlots)
        {
            int wordIndex = Random.Range(0, avaliablewords.Count);

            GameObject currentWord = Instantiate(wordPrefab, slot.position, Quaternion.identity, slot);
            currentWord.GetComponent<SelectableWords>().word = avaliablewords[wordIndex];
            currentWord.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = avaliablewords[wordIndex];
            
            usedWords.Add(avaliablewords[wordIndex]);
            usedSlots.Add(slot);

            avaliablewords.RemoveAt(wordIndex);
        }
    }

    public void Check()
    {
        GameObject answerSlot = GameObject.Find("Answer Slot");
        string answerSentence = answerSlot.GetComponent<TextMeshProUGUI>().text;

        if (answerSentence.Length > 0 && answerSentence.Substring(0, answerSentence.Length - 1) == translatedSentences[sentenceIndex])
        {
            score++;

            if (score < 4)
            {
                LoadWords();
            }
            else
            {
                Win();
            }
        }
        else
        {
            Reorganize();
            lives--;
            lifes[lifes.Count - 1].SetActive(false);
            lifes.RemoveAt(lifes.Count - 1);
            if (lives <= 0)
            {
                Lose();
            }
        }
    }

    public void Reorganize()
    {
        GameObject answerSlot = GameObject.Find("Answer Slot");
        answerSlot.GetComponent<TextMeshProUGUI>().text = "";

        for (int i = 0; i < usedSlots.Count; i++)
        {
            if (usedSlots[i].transform.childCount > 0)
            {
                Transform child = usedSlots[i].transform.GetChild(0);
                Destroy(child.gameObject);
            }
            GameObject currentWord = Instantiate(wordPrefab, usedSlots[i].position, Quaternion.identity, usedSlots[i]);
            currentWord.GetComponent<SelectableWords>().word = usedWords[i];
            currentWord.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = usedWords[i];
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

        if (GameManager.Instance.gameState == 12)
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
