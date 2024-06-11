using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BasicFollower : MonoBehaviour
{
    public float detectionRadius = 10f;  // Radius to detect tasks (resources/buildings)
    public float collectionRadius = 2f;  // Radius to collect resources
    public float collectionRate = 1f;    // Resources collected per second
    public float constructionRate = 1f;  // Rate of building construction
    private NavMeshAgent agent;
    private Transform resourceTarget;
    private Transform buildingTarget;

    public enum FollowerState { Idle, Collecting, Constructing, Specialized }
    public FollowerState state = FollowerState.Idle;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(TaskRoutine());
    }

    void Update()
    {
        if (state == FollowerState.Collecting && resourceTarget != null)
        {
            MoveToResource();
        }
        else if (state == FollowerState.Constructing && buildingTarget != null)
        {
            MoveToBuilding();
        }
    }

    IEnumerator TaskRoutine()
    {
        while (true)
        {
            if (state == FollowerState.Idle)
            {
                AssignTask();
            }
            yield return new WaitForSeconds(1f); // Check every second
        }
    }

    void AssignTask()
    {
        // Check for buildings that need roles
        Building[] buildings = FindObjectsOfType<Building>();
        foreach (var building in buildings)
        {
            if (!building.needsConstruction && building.HasFreeRoleSlot())
            {
                building.AssignRole();
                state = FollowerState.Specialized;
                Debug.Log("Follower assigned a specialized role.");
                return;
            }
        }

        // Check for resources
        ResourceNode[] resources = FindObjectsOfType<ResourceNode>();
        foreach (var resource in resources)
        {
            if (Vector3.Distance(transform.position, resource.transform.position) < detectionRadius)
            {
                resourceTarget = resource.transform;
                state = FollowerState.Collecting;
                Debug.Log("Resource target found: " + resourceTarget.position);
                return;
            }
        }

        // Check for buildings that need construction
        foreach (var building in buildings)
        {
            if (building.needsConstruction && Vector3.Distance(transform.position, building.transform.position) < detectionRadius)
            {
                buildingTarget = building.transform;
                state = FollowerState.Constructing;
                Debug.Log("Building target found: " + buildingTarget.position);
                return;
            }
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
                StartCoroutine(CompleteTask());
            }
        }
        else
        {
            Debug.LogWarning("NavMeshAgent is not on the NavMesh or is not active.");
        }
    }

    void MoveToBuilding()
    {
        if (agent != null && agent.isOnNavMesh)
        {
            agent.SetDestination(buildingTarget.position);

            if (Vector3.Distance(transform.position, buildingTarget.position) < collectionRadius)
            {
                // Construct the building
                ConstructBuilding(buildingTarget.GetComponent<Building>());
                buildingTarget = null;
                StartCoroutine(CompleteTask());
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

    void ConstructBuilding(Building building)
    {
        building.Construct();
        Debug.Log("Building constructed by follower");
    }

    IEnumerator CompleteTask()
    {
        state = FollowerState.Idle;
        yield return new WaitForSeconds(1f); // Cooldown after completing a task
        AssignTask(); // Immediately assign a new task
    }
}
