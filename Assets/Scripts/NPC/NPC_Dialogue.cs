using System;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialogue : MonoBehaviour
{
    public float dialogueRange;
    public LayerMask playerLayer;
    public DialogueSettings dialogue;
    public bool playerHit;

    private List<string> sentences = new List<string>();
    private List<string> names = new List<string>();
    private List<Sprite> profiles = new List<Sprite>();
    private int state;
    private Collider[] hit;

    void Start()
    {
        GetNPCInfo();
    }

    void Update()
    {
        ShowDialogue();
    }

    void ShowDialogue()
    {
        hit = Physics.OverlapSphere(transform.position, dialogueRange, playerLayer);

        if (hit.Length > 0 && !playerHit)
        {
            playerHit = true;

            ButtonController.Instance.ActivateBtn("Speech", this);
        }
        else if (hit.Length == 0 && playerHit)
        {
            playerHit = false;

            ButtonController.Instance.DeactivateBtn();
        }
    }

    public void StartDialogue()
    {
        DialogueControl.instance.Speech(sentences.ToArray(), names.ToArray(), profiles.ToArray(), state, false);
    }

    void GetNPCInfo()
    {
        for (int i = 0; i < dialogue.dialogues.Count; i++)
        {
            sentences.Add(dialogue.dialogues[i].sentence.portuguese);
            names.Add(dialogue.dialogues[i].actorName);
            profiles.Add(dialogue.dialogues[i].profile);
            state = dialogue.state;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}
