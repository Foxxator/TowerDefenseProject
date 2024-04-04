using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum NodeState
{
    Obtained,
    Accessible,
    Unaccessible
}

public class XavierNodeButton : MonoBehaviour
{
    [SerializeField] XavierNodeButton parentNode;
    [SerializeField] SpriteRenderer spriteRenderer;
    LineRenderer lineRenderer;
    NodeState currentState = NodeState.Unaccessible;

    List<XavierNodeButton> children = new List<XavierNodeButton>();

    private void Awake()
    {
        if(parentNode != null)
        {
            parentNode.children.Add(this);
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, parentNode.transform.position);
        }
        else
        {
            SetState(NodeState.Accessible);
        }
    }

    private void Start()
    {
        if(parentNode == null)
        {
            SetState(NodeState.Accessible);
        }
    }

    private void SetState(NodeState nodeState)
    {
        currentState = nodeState;

        switch (currentState)
        {
            case NodeState.Obtained:
                spriteRenderer.color = Color.green;
                foreach(var child in children)
                {
                    child.SetState(NodeState.Accessible);
                }
                break;
            case NodeState.Accessible:
                spriteRenderer.color = Color.yellow;
                foreach(var child in children)
                {
                    child.SetState(NodeState.Unaccessible);
                }
                break;
            case NodeState.Unaccessible:
                spriteRenderer.color = Color.red;
                foreach(var child in children)
                {
                    child.SetState(NodeState.Unaccessible);
                }
                break;
        }
    }

    private void OnMouseDown()
    {
        if(currentState == NodeState.Accessible)
        {
            SetState(NodeState.Obtained);
        }
    }
}
