using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ADgameTiles : MonoBehaviour
{
    [SerializeField] SpriteRenderer hoverRenderer;
    [SerializeField] SpriteRenderer turretRenderer;
    [SerializeField] SpriteRenderer spawnRenderer;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        turretRenderer.enabled = false;
    }

    internal void TurnGrey()
    {
        spriteRenderer.color = Color.gray;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverRenderer.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverRenderer.enabled = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        turretRenderer.enabled = !turretRenderer.enabled;
    }

    internal void SetEnemySpawn()
    {
        spawnRenderer.enabled = true;
    }
}
