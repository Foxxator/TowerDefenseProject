using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{ 
    public static HashSet<Enemy> allEnemies = new HashSet<Enemy>(); //JeudiEnemy
    private Stack<GameTiles> path = new Stack<GameTiles>(); 
    int hp = 5; 

    private void Awake()
    {
        allEnemies.Add(this);
    } 

     
    internal void SetPath(List<GameTiles> pathToGoal)
    {
        path.Clear(); 
        foreach(GameTiles tile in pathToGoal)
        {
            path.Push(tile);
        }
    }

    private void Update()
    {
        if(path.Count > 0)
        {
            Vector3 destPos = path.Peek().transform.position;
            transform.position = Vector3.MoveTowards(transform.position, destPos, 2 * Time.deltaTime);

            if (Vector3.Distance(transform.position, destPos) < 0.01f)
            {
                path.Pop();
            }
        }
        else
        {
            Die();
            PerdreVie();
        }
    } 

    private void PerdreVie()
    {
        GameManager.Instance.PerdreVie();
        Destroy(gameObject);
    }

    private void Die()
    {
        allEnemies.Remove(this);
        Destroy(gameObject); 
    }

    internal void Attack()
    {
        if(--hp <= 0)
        { 
            Die();
            GameManager.Instance.AddExperience(1);
        } 
    }
}
