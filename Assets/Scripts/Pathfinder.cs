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
    Waypoint searchCentre;

    public List<Waypoint> path = new List<Waypoint>(); // make private

    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.left,
        Vector2Int.down
    };

    public List<Waypoint> GetPath()
    {
        Loadblocks();
        ColorStartAndEnd();
        BreadthFirstSearch();
        createPath();
        return path;
    }

    private void createPath()
    {
        path.Add(endWaypoint);

        Waypoint previous = endWaypoint.exploredFrom;
        while (previous != startWaypoint)
        {
            path.Add(previous);
            previous = previous.exploredFrom;
        }

        path.Add(startWaypoint);
        path.Reverse();

    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);

        while (queue.Count > 0 && isRunning)
        {
            var searchCentre = queue.Dequeue();
            HaltIfSearchingIfEndFound(searchCentre);
            ExploreNeighbours();
            searchCentre.isExplored = true;
        }
       
    }

    private void HaltIfSearchingIfEndFound(Waypoint searchCentre)
    {
        if (searchCentre == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbours()
    {
        if (!isRunning) { return; }
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = searchCentre.GetGridPos() + direction;
            if (grid.ContainsKey(neighbourCoordinates)) { QueueNewNeighbours(neighbourCoordinates); }
 
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        if (neighbour.isExplored || queue.Contains(neighbour))
        {

        }
        else
        {
            
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCentre;
        }
    }

    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopcolor(Color.green);
        endWaypoint.SetTopcolor(Color.red);
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
