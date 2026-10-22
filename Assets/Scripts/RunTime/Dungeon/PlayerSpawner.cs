using System;
using UnityEngine;

public class PlayerSpawner : ProceduralGenerator
{
    [SerializeField] private PlayerMovement playerPrefab;
    private DungeonWrapper dungeonWrapper;
    public event Action<GameObject> onPlayerSpawned;

    private void Start()
    {
        dungeonWrapper = GetComponent<DungeonWrapper>();
    }
    public void StartSpawning()
    {
        Vector2 position = dungeonWrapper.reducedRooms[0].room.center;
        GameObject player = Instantiate(playerPrefab, new Vector3(position.x, 0, position.y), Quaternion.identity).gameObject;
        onPlayerSpawned?.Invoke(player);
        StartCoroutine(dungeonWrapper.ChangeDungeonStatus(DungeonWrapper.DungeonStatus.PlayerSpawned));
    }
}
