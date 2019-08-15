using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();

        StartCoroutine(FollowPath(path));
        
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
       foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            print(waypoint.name);
            yield return new WaitForSeconds(1f);
        }
    }

}
