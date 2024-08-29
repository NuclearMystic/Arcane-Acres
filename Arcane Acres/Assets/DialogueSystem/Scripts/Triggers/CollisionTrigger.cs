using UnityEngine;

namespace DialogueSystem
{
    public class CollisionTrigger : MonoBehaviour
    {
        // This is an example of how to use colliders to trigger dialogue

        private void OnTriggerEnter(Collider other)
        {
            // You can nest this inside a if statement that checks the tag or layer of the object collided with
            // (e.g. "npc", "event", "player", etc...)
            GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }
}

