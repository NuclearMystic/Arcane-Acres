using UnityEngine;

public class Door : Interactable
{
    public override void Interact()
    {
        base.Interact();
        // Add logic to open the door
        Debug.Log("The door is now open!");
    }
}
