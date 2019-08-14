using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent( typeof (Waypoint))]
public class CubeEditor : MonoBehaviour
{
    Waypoint waypoint;

    void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        int gridsize = waypoint.GetGridSize();
       
        transform.position = new Vector3(waypoint.GetGridPos().x * gridsize ,0, waypoint.GetGridPos().y * gridsize);
    }

    private void UpdateLabel()
    {
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        string labelText = waypoint.GetGridPos().x  + "," + waypoint.GetGridPos().y ;
        textMesh.text = labelText;
        gameObject.name = labelText;
    }
}
