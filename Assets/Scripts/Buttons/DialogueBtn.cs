using UnityEngine;

public class DialogueButton : MonoBehaviour, IButtonParameter
{
    private NPC_Dialogue targetNPC;

    public void ReceiveParameter(object parameter)
    {
        targetNPC = parameter as NPC_Dialogue;

        if (targetNPC == null)
        {
            Debug.LogWarning("Invalid NPC passed to DialogueButton.");
        }
    }

    public void Click()
    {
        if (targetNPC != null)
        {
            targetNPC.StartDialogue();
        }
        else
        {
            Debug.LogWarning("No NPC assigned to DialogueButton.");
        }
    }
}
