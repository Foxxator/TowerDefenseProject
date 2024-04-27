using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameTiles : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler 
{
    [SerializeField] SpriteRenderer hoverRenderer;
    [SerializeField] SpriteRenderer turretRenderer;
    [SerializeField] SpriteRenderer spawnRenderer;
    [SerializeField] SpriteRenderer wallRenderer; 

    private LineRenderer lineRenderer; //JeudiEnemy
    private SpriteRenderer spriteRenderer; 
    private Color originalColor; //Chemin le plus court 
    public GameManager GM { get; internal set; } //Chemin le plus court
    public int X { get; internal set; }//Chemin le plus court
    public int Y { get; internal set; }//Chemin le plus court
    public bool IsBlocked { get; internal set; } //Chemin le plus court

    bool canAttack;

    private void Awake()
    {
        lineRenderer =  GetComponent<LineRenderer>(); //JeudiEnemy
        lineRenderer.enabled = false; //JeudiEnemy
        lineRenderer.SetPosition(0, transform.position); //JeudiEnemy 
        spriteRenderer = GetComponent<SpriteRenderer>();
        turretRenderer.enabled = false; 
        originalColor = spriteRenderer.color; //Chemin le plus court
    } 

    void Update() //JeudiEnemy
    {
        if(turretRenderer.enabled && canAttack)
        { 
            Enemy target = null;
            foreach(var enemy in Enemy.allEnemies)
            {
                if(Vector3.Distance(transform.position, enemy.transform.position) < 2)
                {
                    target = enemy;
                    break;
                }
            }

            if(target != null)
            { 
                StartCoroutine(AttackCoroutine(target));
            } 
        }
    }
    
       IEnumerator AttackCoroutine(Enemy target) //Détruire l'objet de l'ennemi
      {
        target.GetComponent<Enemy>().Attack();
        canAttack = false;
        lineRenderer.SetPosition(1, target.transform.position);
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.2f);
        lineRenderer.enabled = false;
        yield return new WaitForSeconds(1.0f);
        canAttack = true;
    }  

    internal void TurnGrey()
    {
        spriteRenderer.color = Color.gray;
        originalColor = spriteRenderer.color;  
    }

    public void OnPointerEnter(PointerEventData eventData)
    { 
        hoverRenderer.enabled = true;
        //GM.TargetTile = this; //Level Design (pour les //)
    }

    public void OnPointerExit(PointerEventData eventData)
    { 
        hoverRenderer.enabled = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (wallRenderer.enabled)
        { 
            turretRenderer.enabled = true;
        }

        //if (numberOfTurretsPlaced <= 5)
        //{
        //    turretRenderer.enabled = true;
        //    caseHaveTurret++; 

        //   // numberOfTurretsPlaced++;
        //    IsBlocked = turretRenderer.enabled;
        //    Debug.Log(numberOfTurretsPlaced); 
        //}
        //if (caseHaveTurret == 1)
        //{
        //    numberOfTurretsPlaced++;
        //}
    } 

    internal void SetEnemySpawn()
    {
        spawnRenderer.enabled = true;
    } 
    internal void setPath(bool isPath) 
    {
        spriteRenderer.color = isPath ? Color.yellow : originalColor;
    }  
    internal void SetWall()
    {
        wallRenderer.enabled = true;
        IsBlocked = true;
    }
}
