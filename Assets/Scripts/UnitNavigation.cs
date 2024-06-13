using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitNavigation : MonoBehaviour
{
    private NavigationPoint[] points;
    void Start()
    {
        var tmpPoints = FindObjectsByType<NavigationPoint>(FindObjectsSortMode.None);
    }
    private void GenerateNavigation(NavigationPoint[] points)
    {
        float distance = Mathf.Infinity;
        NavigationPoint closestPoint = null;
        foreach (var point in points)
        {
            if(Vector3.Distance(transform.position, point.transform.position) < distance)
            {
                distance = Vector3.Distance(transform.position, point.transform.position);
                closestPoint = point;
            }
        }
    }
    
}
