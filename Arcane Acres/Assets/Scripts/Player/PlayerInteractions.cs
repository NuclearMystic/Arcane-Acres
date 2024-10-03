using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public float interactionRange = 2f;
    public LayerMask interactableLayerMask;
    public Transform lookAtTarget;

    private Animator playerAnimator;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))  // Press E to interact
        {
            RaycastHit hit;
            if (Physics.Raycast(lookAtTarget.position, transform.forward, out hit, interactionRange, interactableLayerMask))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }

    public void WaterCrops(Crop crop, int waterAmount)
    {
        crop.WaterCrop(waterAmount);
    }

    public void HarvestCrop(Crop crop)
    {
        playerAnimator.SetTrigger("Harvest");
    }
}
