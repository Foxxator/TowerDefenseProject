using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static HashSet<Enemy> allEnemies = new HashSet<Enemy>(); //JeudiEnemy

    private Stack<GameTiles> path = new Stack<GameTiles>();

    private void Awake() //JeudiEnemy
    {
        allEnemies.Add(this);
    }

    private void Update()
    {
        transform.position += Vector3.right * Time.deltaTime;

        if(path.Count > 0)
        {

        }
        else
        {
            allEnemies.Remove(this);
            Destroy(gameObject);
        }
    }
}
