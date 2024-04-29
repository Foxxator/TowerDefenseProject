using UnityEngine;

public class EnvironmentAround : MonoBehaviour
{
    private float radius = 100.0f;
    private float newPosThreshold = 25.0f;
    private Vector3 refreshedPos;
     
    void Start()
    {
        refreshedPos = transform.position;
    }
     
    void Update()
    {
        if ((refreshedPos + transform.position).magnitude >= newPosThreshold)
        {
            
        }
    }
}
