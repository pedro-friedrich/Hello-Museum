using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [System.Serializable]
    public enum idiom
    {
        pt
    }

    public idiom language;

    [Header("Components")]
    public GameObject dialogueObj;
    public Image profileSprite;
    public Image Waypoint;
    public TextMeshProUGUI speechText;
    public TextMeshProUGUI actorNametext;
    public Button skipButton;
    public Transform buttonsParent;
    public GameObject guideGhost;

    [Header("Settings")]
    public int animSpeed;

    private bool isShowing;
    private bool isPlayerDialogue;
    private int index;
    private int state;
    private string[] sentences;
    private string[] names;
    private Sprite[] profiles;
    private Player player;
    private List<GameObject> previouslyActiveButtons = new List<GameObject>();

    public static DialogueControl instance;

    public bool IsShowing { get => isShowing; set => isShowing = value; }

    void Start()
    {
        player = FindFirstObjectByType<Player>();
    }

    // Awake is called before all Start() no the scene hierarchy
    private void Awake()
    {
        instance = this;
    }

    IEnumerator TypeSentence()
    {
        Vector3 startPos = speechText.rectTransform.position - new Vector3(0, animSpeed, 0);
        Vector3 endPos = speechText.rectTransform.position;
        speechText.rectTransform.position = startPos;

        speechText.text = sentences[index];

        float elapsed = 0f;
        float duration = 0.3f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            speechText.rectTransform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        speechText.rectTransform.position = endPos;
        skipButton.gameObject.SetActive(true);
    }


    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            speechText.text = "";
            actorNametext.text = null;
            profileSprite.sprite = null;
            skipButton.gameObject.SetActive(false);
            StartCoroutine(TypeSentence());
            actorNametext.text = names[index];
            profileSprite.sprite = profiles[index];
        }
        else
        {
            speechText.text = "";
            actorNametext.text = null;
            profileSprite.sprite = null;
            index = 0;
            dialogueObj.SetActive(false);
            sentences = null;
            IsShowing = false;
            player.IsTalking = false;
            player.joystick.OnPointerUp(null);
            if (guideGhost.activeSelf)
            {
                guideGhost.SetActive(false);
            }
            if (isPlayerDialogue)
            {
                GameManager.Instance.SetState(state + 1);
            }
            foreach (GameObject button in previouslyActiveButtons)
            {
                button.SetActive(true);
            }
            previouslyActiveButtons.Clear();
            Waypoint.gameObject.SetActive(true);
        }
    }

    public void Speech(string[] txt, string[] name, Sprite[] img, int nextState, bool playerDialogue)
    {
        if (!IsShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            names = name;
            profiles = img;
            state = nextState;
            skipButton.gameObject.SetActive(false);
            previouslyActiveButtons.Clear();
            foreach (Transform child in buttonsParent)
            {
                if (child.gameObject.activeSelf)
                {
                    previouslyActiveButtons.Add(child.gameObject);
                    child.gameObject.SetActive(false);
                }
            }
            Waypoint.gameObject.SetActive(false);

            StartCoroutine(TypeSentence());
            actorNametext.text = names[index];
            profileSprite.sprite = profiles[index];
            IsShowing = true;
            player.IsTalking = true;
            isPlayerDialogue = playerDialogue;
        }
    }
}
