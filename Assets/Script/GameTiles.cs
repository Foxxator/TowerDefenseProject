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
    private LineRenderer lineRenderer; //JeudiEnemy
    private SpriteRenderer spriteRenderer; 
    private Color originalColor; //Chemin le plus court 
    public GameManager GM { get; internal set; } //Chemin le plus court
    public int X { get; internal set; }//Chemin le plus court
    public int Y { get; internal set; }//Chemin le plus court
    public bool IsBlocked { get; internal set; } //Chemin le plus court

    private void Awake()
    {
        lineRenderer =  GetComponent<LineRenderer>(); //JeudiEnemy
        lineRenderer.enabled = false; //JeudiEnemy
        lineRenderer.SetPosition(0, transform.position); //JeudiEnemy 
        spriteRenderer = GetComponent<SpriteRenderer>();
        turretRenderer.enabled = false;
       // hoverRenderer.enabled = false;
        originalColor = spriteRenderer.color; //Chemin le plus court
    }

    void Update() //JeudiEnemy
    {
        if(turretRenderer.enabled)
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
                lineRenderer.SetPosition(1, target.transform.position);
                lineRenderer.enabled = true;
            }
            else
            {
                lineRenderer.enabled=false;
            }
        }
    }

    internal void TurnGrey()
    {
        spriteRenderer.color = Color.gray;
        originalColor = spriteRenderer.color; //Chemin le plus court
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(gameObject.name);
        hoverRenderer.enabled = true;
        GM.TargetTile = this; //Chemin le plus court
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log(gameObject.name);
        hoverRenderer.enabled = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        turretRenderer.enabled = !turretRenderer.enabled;
        IsBlocked = turretRenderer.enabled; //Chemin le plus court
    }

    internal void SetEnemySpawn()
    {
        spawnRenderer.enabled = true;
    }

    internal void setPath(bool isPath)//Chemin le plus court
    {
        spriteRenderer.color = isPath ? Color.yellow : originalColor;
    }
}
