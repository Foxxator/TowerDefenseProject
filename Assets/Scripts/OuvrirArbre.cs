using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuvrirArbre : MonoBehaviour
{
    public GameObject arbredecomp;
    void Start()
    {
        arbredecomp.SetActive(false);
    }

    public void AciveCanva()
    {
        bool arbreActive = arbredecomp.activeSelf;
        arbredecomp.SetActive(!arbreActive);
        Time.timeScale = arbreActive ? 1 : 0;
    }
}
