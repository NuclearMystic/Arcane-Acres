using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvest : Interactable
{
    private Crop crop;  // Reference to the crop being interacted with
    public GameObject cropItemPrefab;  // Prefab of the crop item that will spawn when harvested
    public int numberOfItemsToSpawn = 3;  // Number of crop items to spawn

    public float popUpForce = 3f;  // Force to pop up the items
    public float magnetDelay = 1f;  // Delay before the magnet effect starts
    public float magnetSpeed = 5f;  // Speed at which items move toward the player

    public Transform itemSpawnPoint;

    void Start()
    {
        crop = GetComponent<Crop>();
    }

    // This method is called when the player interacts with the crop
    public override void Interact()
    {
        if (crop.IsHarvestable())
        {
            HarvestCrop();
        }
        else
        {
            // For testing, if the crop is not harvestable, advance it one stage
            Debug.Log("Crop is not harvestable yet. Advancing to the next stage for testing.");
            crop.ForceAdvanceStage();
        }
    }

    // Logic for harvesting the crop
    private void HarvestCrop()
    {
        Debug.Log("Harvesting the crop!");
        StartCoroutine(HarvestAction());
        crop.UpdateGrowth();
    }

    private IEnumerator HarvestAction()
    {
        FindObjectOfType<PlayerInteractions>().HarvestCrop(crop);
        yield return new WaitForSeconds(1.5f);
        List<GameObject> spawnedItems = new List<GameObject>();

        // Spawn crop items and apply pop-up force
        for (int i = 0; i < numberOfItemsToSpawn; i++)
        {
            Vector3 spawnPosition = itemSpawnPoint.position + new Vector3(Random.Range(-0.5f, 0.5f), 0.5f, Random.Range(-0.5f, 0.5f));
            GameObject cropItem = Instantiate(cropItemPrefab, spawnPosition, Quaternion.identity);
            Rigidbody rb = cropItem.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Apply an upward force to pop the item up
                Vector3 randomDirection = new Vector3(Random.Range(-0.5f, 0.5f), 1f, Random.Range(-0.5f, 0.5f));
                rb.AddForce(randomDirection * popUpForce, ForceMode.Impulse);
            }

            spawnedItems.Add(cropItem);
        }

        // Wait for the magnet delay
        yield return new WaitForSeconds(magnetDelay);

        // Start moving the items toward the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            foreach (GameObject item in spawnedItems)
            {
                StartCoroutine(MoveItemToPlayer(item, player.transform));
            }
        }
    }

    // Coroutine to move an item toward the player over time (magnet effect)
    private IEnumerator MoveItemToPlayer(GameObject item, Transform playerTransform)
    {
        float duration = 2f;  // How long the item will take to move toward the player
        float elapsedTime = 0f;

        while (elapsedTime < duration && item != null)
        {
            // Smoothly move the item toward the player's position
            item.transform.position = Vector3.Lerp(item.transform.position, playerTransform.position, (elapsedTime / duration) * magnetSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
