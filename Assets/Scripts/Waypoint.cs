using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    // public ok here as is data class
    public bool isExplored = false;
    public Waypoint exploredFrom;

    Vector2Int gridPos;
    const int gridSize = 10;

    public int GetGridSize()
    {
        Debug.Log("Get Grid Size");
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
        Mathf.RoundToInt(transform.position.x / 10f),
        Mathf.RoundToInt(transform.position.z / 10f)
        );
    }

    public void SetTopcolor(Color color)
    {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
