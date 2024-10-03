using UnityEngine;

namespace DialogueSystem
{
    public class PlayerRaycastTrigger : MonoBehaviour
    {
        // This is an example for how you can use a raycast to trigger dialogues or alter conditions
        private DialogueUI dialogueUI;

        private void Start()
        {
            dialogueUI = FindObjectOfType<DialogueUI>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && !dialogueUI.IsDialogueActive())
            {
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 3))
                {
                    if (hit.collider.GetComponent<DialogueTrigger>())
                    {
                        hit.collider.GetComponent<DialogueTrigger>().TriggerDialogue();
                    }
                    if (hit.collider.GetComponent<ConditionTrigger>())
                    {
                        hit.collider.GetComponent<ConditionTrigger>().ModifyCondition();
                    }
                }
            }
        }
    }
}


