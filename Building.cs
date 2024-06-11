using UnityEngine;

public class Building : MonoBehaviour
{
    public bool needsConstruction = true;
    public int maxRoles = 3;
    private int currentRoles = 0;

    public void Construct()
    {
        needsConstruction = false;
        Debug.Log("Building construction completed");
    }

    public bool HasFreeRoleSlot()
    {
        return currentRoles < maxRoles;
    }

    public void AssignRole()
    {
        if (currentRoles < maxRoles)
        {
            currentRoles++;
            Debug.Log("Role assigned to follower");
        }
    }
}
