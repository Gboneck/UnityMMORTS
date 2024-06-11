using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public GameObject resourcePrefab;
    public int numberOfResources = 10;
    public Vector3 spawnArea = new Vector3(50, 0, 50);

    void Start()
    {
        for (int i = 0; i < numberOfResources; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                0,
                Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
            );
            Instantiate(resourcePrefab, randomPosition, Quaternion.identity);
        }
    }
}
