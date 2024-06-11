using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ResourceCollector : MonoBehaviour
{
    public float detectionRadius = 10f;  // Radius to detect resources
    public float collectionRadius = 2f; // Radius to collect resources
    public float collectionRate = 1f;   // Resources collected per second
    private NavMeshAgent agent;
    private Transform resourceTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(FindResource());
    }

    void Update()
    {
        if (resourceTarget != null)
        {
            MoveToResource();
        }
    }

    IEnumerator FindResource()
    {
        Debug.Log("Finding resource...");
        while (true)
        {
            ResourceNode[] resources = FindObjectsOfType<ResourceNode>();
            foreach (var resource in resources)
            {
                if (Vector3.Distance(transform.position, resource.transform.position) < detectionRadius)
                {
                    resourceTarget = resource.transform;
                    Debug.Log("Resource target found: " + resourceTarget.position);
                    yield break;
                }
            }
            yield return new WaitForSeconds(1f); // Check every second
        }
    }

    void MoveToResource()
    {
        if (agent != null && agent.isOnNavMesh)
        {
            agent.SetDestination(resourceTarget.position);

            if (Vector3.Distance(transform.position, resourceTarget.position) < collectionRadius)
            {
                // Collect the resource
                CollectResource(resourceTarget.GetComponent<ResourceNode>());
                resourceTarget = null;
                StartCoroutine(FindResource());
            }
        }
        else
        {
            Debug.LogWarning("NavMeshAgent is not on the NavMesh or is not active.");
        }
    }

    void CollectResource(ResourceNode resource)
    {
        resource.Collect();
        Debug.Log("Resource collected by follower");
    }

    // Method to return collected resources
    public int CollectResources()
    {
        int collected = 10; // Example amount
        // Add logic to transfer collected resources
        return collected;
    }
}
