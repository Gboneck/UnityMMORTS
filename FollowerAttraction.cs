using UnityEngine;

public class FollowerAttraction : MonoBehaviour
{
    public GameObject followerPrefab;
    public int maxFollowers = 5;
    public float spawnInterval = 5f;
    private int currentFollowers = 0;

    void Start()
    {
        InvokeRepeating(nameof(SpawnFollower), spawnInterval, spawnInterval);
    }

    void SpawnFollower()
    {
        if (currentFollowers < maxFollowers)
        {
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
            Instantiate(followerPrefab, spawnPosition, Quaternion.identity);
            currentFollowers++;
        }
    }
}
