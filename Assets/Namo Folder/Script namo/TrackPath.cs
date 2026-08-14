using UnityEngine;
using System.Collections.Generic;

public class TrackPath : MonoBehaviour
{
    [Header("จุด Waypoints เรียงตามลำดับราง")]
    public List<Transform> waypoints = new List<Transform>();

    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Count < 2) return;

        Gizmos.color = Color.cyan;
        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            if (waypoints[i] != null && waypoints[i + 1] != null)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                Gizmos.DrawWireSphere(waypoints[i].position, 0.2f);
            }
        }
        if (waypoints[waypoints.Count - 1] != null)
        {
            Gizmos.DrawWireSphere(waypoints[waypoints.Count - 1].position, 0.2f);
        }
    }
}