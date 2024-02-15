using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public List<GameObject> gemPrefabs; // List of prefabs to spawn
    public int numberOfGems = 5; // Number of gems to spawn
    public Vector3 spawnAreaCenter;
    public Vector3 spawnAreaSize;

    void Start()
    {
        SpawnGems();
    }

    void SpawnGems()
    {
        List<GameObject> availableGems = new List<GameObject>(gemPrefabs); // Create a copy of the list of prefabs

        for (int i = 0; i < numberOfGems; i++)
        {
            if (availableGems.Count == 0)
            {
                Debug.LogWarning("Not enough unique gems to spawn.");
                break; 
            }

            // Randomly select a gem prefab from the available list
            int randomIndex = Random.Range(0, availableGems.Count);
            GameObject selectedPrefab = availableGems[randomIndex];

            // Random position within the spawn area
            Vector3 spawnPos = new Vector3(
                Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2),
                spawnAreaCenter.y,
                Random.Range(spawnAreaCenter.z - spawnAreaSize.z / 2, spawnAreaCenter.z + spawnAreaSize.z / 2)
            );

            // Instantiate gem at the random position
            GameObject gemInstance = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);

            // Remove the selected prefab from the available list to ensure uniqueness
            availableGems.RemoveAt(randomIndex);
        }
    }
}
