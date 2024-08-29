using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using UnityEngine;

public class Speak : Interactable
{
    public override void Interact()
    {
        base.Interact();
        // Add logic to open the door
        DialogueTrigger dialogueTrigger = GetComponent<DialogueTrigger>();
        dialogueTrigger.TriggerDialogue();
        Debug.Log("The door is now open!");
    }
}
