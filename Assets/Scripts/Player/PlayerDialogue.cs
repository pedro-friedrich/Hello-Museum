using UnityEngine;
using System.Collections.Generic;

public class PlayerDialogue : MonoBehaviour
{
    public List<DialogueSettings> allDialogues; // Lista com todos os diálogos possíveis

    private DialogueSettings currentDialogue;
    private List<string> sentences = new List<string>();
    private List<string> names = new List<string>();
    private List<Sprite> profiles = new List<Sprite>();
    private int state;

    void Start()
    {
        UpdateDialogue();
    }

    void Update()
    {
        // Atualiza o diálogo sempre que o gameState mudar
        if (currentDialogue == null || currentDialogue.state != GameManager.Instance.gameState)
        {
            UpdateDialogue();
        }
    }

    void UpdateDialogue()
    {
        currentDialogue = allDialogues.Find(d => d.state == GameManager.Instance.gameState);

        if (currentDialogue == null)
        {
            return;
        }

        sentences.Clear();
        names.Clear();
        profiles.Clear();

        for (int i = 0; i < currentDialogue.dialogues.Count; i++)
        {
            sentences.Add(currentDialogue.dialogues[i].sentence.portuguese);
            names.Add(currentDialogue.dialogues[i].actorName);
            profiles.Add(currentDialogue.dialogues[i].profile);
        }

        state = currentDialogue.state;
    }

    public void StartDialogue()
    {
        if (currentDialogue != null)
        {
            DialogueControl.instance.Speech(sentences.ToArray(), names.ToArray(), profiles.ToArray(), state, true);
        }
    }
}
