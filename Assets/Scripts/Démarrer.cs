using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Démarrer : MonoBehaviour
{
    [SerializeField] public int sceneindex = 0;

    public void NextScene()
    {
        SceneManager.LoadScene(sceneindex);
    }
}
