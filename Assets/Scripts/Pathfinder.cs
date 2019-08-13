using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint;
    [SerializeField] Waypoint endWaypoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;

    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.left,
        Vector2Int.down
    };

    // Start is called before the first frame update
    void Start()
    {
        Loadblocks();
        ColorStartAndEnd();
        Pathfind();
        
    }

    private void Pathfind()
    {
        queue.Enqueue(startWaypoint);

        while (queue.Count > 0 && isRunning)
        {
            var search = queue.Dequeue();
            HaltIfSearchingIfEndFound(search);
            ExploreNeighbours(search);
            search.isExplored = true;
        }
       
    }

    private void HaltIfSearchingIfEndFound(Waypoint search)
    {
        if (search == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbours(Waypoint from)
    {
        if (!isRunning) { return; }
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = startWaypoint.GetGridPos() + direction;
            try
            {
                QueueNewNeighbours(neighbourCoordinates);
            }
            catch { }
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        if (neighbour.isExplored)
        {

        }
        else
        {
            grid[neighbourCoordinates].SetTopcolor(Color.blue);
            queue.Enqueue(neighbour);
        }
    }

    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopcolor(Color.green);
        endWaypoint.SetTopcolor(Color.blue);
    }

    private void Loadblocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Skipping overlapping block " + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
                
            }
        }
        print("loaded " + grid.Count + " blocks");
    }

}
