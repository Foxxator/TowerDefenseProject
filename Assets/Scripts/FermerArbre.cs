using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FermerArbre : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (this.enabled == true)
            {
                Time.timeScale = 1;
                this.enabled = false;
            }
        }
    }
}
