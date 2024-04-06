using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XavierTracker : MonoBehaviour
{
    [SerializeField] List<Transform> destinations = new List<Transform>();
    int destinationIndex;

    private void Update()
    {
        if(destinationIndex < destinations.Count)
        {
            transform.position = Vector3.Lerp(transform.position, destinations[0].position, 2 * Time.deltaTime);

            if(Vector3.Distance(transform.position, destinations[destinationIndex].position) < 0.01f)
            {
                destinationIndex++;
            }
        }
    }
}
