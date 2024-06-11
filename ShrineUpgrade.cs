using UnityEngine;

public class ShrineUpgrade : MonoBehaviour
{
    public int upgradeCost = 100;
    public int currentLevel = 1;
    public int maxLevel = 3;

    private int resourcesCollected = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Follower"))
        {
            ResourceCollector collector = other.GetComponent<ResourceCollector>();
            resourcesCollected += collector.CollectResources();
            CheckForUpgrade();
        }
    }

    void CheckForUpgrade()
    {
        if (resourcesCollected >= upgradeCost && currentLevel < maxLevel)
        {
            UpgradeShrine();
        }
    }

    void UpgradeShrine()
    {
        currentLevel++;
        resourcesCollected -= upgradeCost;
        upgradeCost *= 2; // Increase the cost for the next level
        // Add visual or functional changes for the upgraded shrine here
        Debug.Log("Shrine upgraded to level " + currentLevel);
    }
}
