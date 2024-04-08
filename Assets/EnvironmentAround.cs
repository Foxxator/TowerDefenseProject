using UnityEngine;

public class EnvironmentAround : MonoBehaviour
{
    private float radius = 100.0f;
    private float newPosThreshold = 25.0f;
    private Vector3 refreshedPos;
    
    // Start is called before the first frame update
    void Start()
    {
        refreshedPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ((refreshedPos + transform.position).magnitude >= newPosThreshold)
        {
            
        }
    }
}
