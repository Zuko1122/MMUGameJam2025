using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaManager : MonoBehaviour
{
    //Assign your gacha item prefabs here into a list
    public List<GameObject> gachaPrefabs;

    //Spawns selected item at a respective position
    public Transform spawnPoint;

    private GameObject currentSpawn;

    public void PullOne()
    {
        // Remove previous pull if it exists
        if (currentSpawn != null)
        {
            Destroy(currentSpawn);
        }

        // Pick a random prefab
        int index = Random.Range(0, gachaPrefabs.Count);
        GameObject selectedPrefab = gachaPrefabs[index];

        // Instantiate it at the spawn point
        currentSpawn = Instantiate(selectedPrefab, spawnPoint.position, Quaternion.identity);
    }

    // Coroutine for delay between pulls
    private IEnumerator PullSequence(int count, float delay)
    {
        for (int i = 0; i < count; i++)
        {
            PullOne();
            yield return new WaitForSeconds(delay); // wait before next pull
        }
    }

    // Random Rolls of 3
    public void PullMultipleX3()
    {
        StartCoroutine(PullSequence(3, 1f));
    }
}
