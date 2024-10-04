using UnityEngine;

namespace DialogueSystem
{
    public class DialogueTrigger : MonoBehaviour
    {
        // This is an example of how to hold a reference for a dialogue tree on a specific object in your scene.

        [Tooltip("Place the dialogue tree you wish to trigger upon interaction with this object here.")]
        public DialogueTree dialogueTree;

        // Call this method to start the above dialogue. This can be from a collider, raycast, unity event or however
        // you have your interaction mechanics setup.
        public void TriggerDialogue()
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogueTree);
        }
    }
}

