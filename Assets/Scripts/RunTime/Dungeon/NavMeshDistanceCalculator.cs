using UnityEngine;
using UnityEngine.AI;

public class NavMeshDistanceCalculator
{
    public static bool GetPath(NavMeshPath path, Vector3 fromPos, Vector3 toPos, int passableMask = NavMesh.AllAreas)
    {
        path.ClearCorners();

        if (NavMesh.CalculatePath(fromPos, toPos, passableMask, path) == false)
        {
            return false;
        }
        return true;
    }

    public static float GetPathLength(NavMeshPath path)
    {
        float length = 0.0f;

        if ((path.status != NavMeshPathStatus.PathInvalid) && (path.corners.Length > 1))
        {
            for (int i = 1; i < path.corners.Length; i++)
            {
                length += Vector3.Distance(path.corners[i - 1], path.corners[i]);
            }
        }
        return length;
    }
}
