using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class CubeEditor : MonoBehaviour
{
    [SerializeField] [Range(1f, 20f)] float gridSize = 10f;

    TextMesh textMesh;

    private void Start()
    {
        
    }

    void Update()
    {
        Vector3 snapPos;
   
        snapPos.x = Mathf.RoundToInt(transform.position.x / 10f) * 10f;
        snapPos.z = Mathf.RoundToInt(transform.position.z / 10f) * 10f;

        transform.position = new Vector3(snapPos.x, 0, snapPos.z);

        textMesh = GetComponentInChildren<TextMesh>();
        textMesh.text = snapPos.x / gridSize + "," + snapPos.z / gridSize;
    }
}
