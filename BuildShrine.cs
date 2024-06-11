using UnityEngine;

public class BuildShrine : MonoBehaviour
{
    public GameObject shrinePrefab;
    public float buildDistance = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Vector3 buildPosition = transform.position + transform.forward * buildDistance;
            Instantiate(shrinePrefab, buildPosition, Quaternion.identity);
        }
    }
}
