using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public int resourceAmount = 100;

    public void Collect()
    {
        resourceAmount -= 10; // Decrease by a fixed amount
        if (resourceAmount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
