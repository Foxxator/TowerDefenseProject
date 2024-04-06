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
    private SpriteRenderer spriteRenderer;

    private Color originalColor; //Chemin le plus court

    public GameManager GM { get; internal set; } //Chemin le plus court
    public int X { get; internal set; }//Chemin le plus court
    public int Y { get; internal set; }//Chemin le plus court
    public bool IsBlocked { get; internal set; } //Chemin le plus court

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        turretRenderer.enabled = false;
        originalColor = spriteRenderer.color; //Chemin le plus court
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
