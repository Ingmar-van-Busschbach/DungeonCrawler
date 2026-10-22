using UnityEngine;
using UnityEngine.AI;

public class SpawnRoomSelector : ProceduralGenerator
{   
    private DungeonWrapper dungeonWrapper;

    private void OnEnable()
    {
        dungeonWrapper = GetComponent<DungeonWrapper>();

        dungeonWrapper.onSpawnRoomSelector += SelectRooms;
    }

    private void OnDisable()
    {
        dungeonWrapper.onSpawnRoomSelector -= SelectRooms;
    }

    private void SelectRooms()
    {
        float length = 0;
        dungeonWrapper.playerSpawnRoom = dungeonWrapper.reducedRooms[0];
        dungeonWrapper.bossSpawnRoom = dungeonWrapper.reducedRooms[1];
        for (int i = 0; i < dungeonWrapper.reducedRooms.Count; i++)
        {
            for (int j = i + 1; j < dungeonWrapper.reducedRooms.Count; j++)
            {
                Vector3 roomAPosition = new Vector3(dungeonWrapper.reducedRooms[i].room.center.x, 0, dungeonWrapper.reducedRooms[i].room.center.y);
                Vector3 roomBPosition = new Vector3(dungeonWrapper.reducedRooms[j].room.center.x, 0, dungeonWrapper.reducedRooms[j].room.center.y);
                NavMeshPath path = new();
                if (NavMeshDistanceCalculator.GetPath(path, roomAPosition, roomBPosition))
                {
                    float distance = NavMeshDistanceCalculator.GetPathLength(path);
                    if (distance > length)
                    {
                        Debug.Log("New spawn rooms found: Distance of " + distance);
                        length = distance;
                        dungeonWrapper.playerSpawnRoom = dungeonWrapper.reducedRooms[i];
                        dungeonWrapper.bossSpawnRoom = dungeonWrapper.reducedRooms[j];
                    }
                }
                else
                {
                    Debug.Log("Failed to get path!");
                }
            }
        }
        StartCoroutine(dungeonWrapper.ChangeDungeonStatus(DungeonWrapper.DungeonStatus.SpawnRoomSelected));
    }
}
