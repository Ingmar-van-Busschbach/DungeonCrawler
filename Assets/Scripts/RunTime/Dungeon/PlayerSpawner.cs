using System;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerSpawner : ProceduralGenerator
{
    [SerializeField] private PlayerMovement playerPrefab;
    [SerializeField] private GameObject playerCamera;
    private DungeonWrapper dungeonWrapper;
    private CameraSpawner cameraSpawner;
    public event Action<GameObject> onPlayerSpawned;

    private void Start()
    {
        dungeonWrapper = GetComponent<DungeonWrapper>();
        cameraSpawner = GetComponent<CameraSpawner>();
        dungeonWrapper.onPlayerSpawner += StartSpawning;
    }

    private void OnDisable()
    {
        dungeonWrapper.onPlayerSpawner -= StartSpawning;
    }
    public void StartSpawning()
    {
        Vector2 position2D = dungeonWrapper.reducedRooms[dungeonWrapper.playerSpawnRoomIndex].room.center;
        cameraSpawner.cameras[dungeonWrapper.playerSpawnRoomIndex].Priority = 10;
        Vector3 position3D = new Vector3(position2D.x, 0, position2D.y);
        playerCamera.transform.position = position3D;
        GameObject player = Instantiate(playerPrefab, position3D, Quaternion.identity).gameObject;
        onPlayerSpawned?.Invoke(player);
        StartCoroutine(dungeonWrapper.ChangeDungeonStatus(DungeonWrapper.DungeonStatus.PlayerSpawned));
    }
}
