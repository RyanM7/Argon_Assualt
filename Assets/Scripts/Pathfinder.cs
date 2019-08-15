using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, endWaypoint;
    
    Queue<Waypoint> queue = new Queue<Waypoint>();

    bool isRunning = true;

    Waypoint searchCentre;

    public List<Waypoint> path = new List<Waypoint>(); // make private

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

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
        CreatePath();
        return path;
    }

    private void CreatePath()
    {
        Debug.Log("I work!");
        path.Add(endWaypoint);

        Waypoint previous = endWaypoint.exploredFrom;
        Debug.Log("the waypoints here" + endWaypoint);

        while (previous != startWaypoint)
        {          
            path.Add(previous);
            Debug.Log("ive got previous" + previous );
            previous = previous.exploredFrom;
        }

        path.Add(startWaypoint);
        path.Reverse();
    }

    private void BreadthFirstSearch()
    {
        print("Breadth first search!");
        queue.Enqueue(startWaypoint);

        while (queue.Count > 0 && isRunning)
        {
            print("BFS While loop");
            var searchCentre = queue.Dequeue();
            HaltIfSearchingIfEndFound();
            ExploreNeighbours();
            searchCentre.isExplored = true;
        }       
    }

    private void HaltIfSearchingIfEndFound()
    {
        print("Halt if searching end found!");
        if (searchCentre == endWaypoint)
        {
            print("end searching");
            isRunning = false;
        }
    }

    private void ExploreNeighbours()
    {
        print("Exploring neighbours!");
        if (!isRunning) { return; }
        foreach (Vector2Int direction in directions)
        {
            print("For each loop  explore neighbours");
            Vector2Int neighbourCoordinates = startWaypoint.GetGridPos() + direction;

            if (grid.ContainsKey(neighbourCoordinates))
            { print("am i being ran?"); QueueNewNeighbours(neighbourCoordinates); }
 
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        print("in Queue new neighbours");
        if (neighbour.isExplored)
            if (neighbour.isExplored || queue.Contains(neighbour))
            {
                print("done nothing here ");
            }
            else
            {
                print("im in the neighbour else loop!");
                queue.Enqueue(neighbour);
                print("Queueing " + neighbour);
                neighbour.exploredFrom = searchCentre;
            }
    }

    private void ColorStartAndEnd()
    {
        print("Coloring start and end");
        startWaypoint.SetTopcolor(Color.green);
        endWaypoint.SetTopcolor(Color.red);
    }

    private void Loadblocks()
    {
        print("Loading blocks!");
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            print("For each loading block");
            var gridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Skipping overlapping block " + waypoint);
            }
            else
            {
                print("adding grid position via waypoint");
                grid.Add(gridPos, waypoint);              
            }
        }
        print("loaded " + grid.Count + " blocks");
    }

}
