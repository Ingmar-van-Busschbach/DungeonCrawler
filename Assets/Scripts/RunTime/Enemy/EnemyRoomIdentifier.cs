using System;
using UnityEngine;

public class EnemyRoomIdentifier : MonoBehaviour
{
    private DungeonWrapper dungeonWrapper;
    public int roomIndex = -1;
    void Start()
    {
        GameObject generator = GameObject.FindGameObjectWithTag("Generator");
        dungeonWrapper = generator.GetComponent<DungeonWrapper>();
        for (int i = 0; i < dungeonWrapper.reducedRooms.Count; i++)
        {
            Bounds bounds = new Bounds(new Vector3(dungeonWrapper.reducedRooms[i].room.center.x, 0, dungeonWrapper.reducedRooms[i].room.center.y), new Vector3(dungeonWrapper.reducedRooms[i].room.width, 10, dungeonWrapper.reducedRooms[i].room.height));
            if (bounds.Contains(transform.position))
            {
                roomIndex = i;
                break;
            }
        }
    }
}
