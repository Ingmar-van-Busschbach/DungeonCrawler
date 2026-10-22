using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraSpawner : ProceduralGenerator
{
    private DungeonWrapper dungeonWrapper;
    [SerializeField] private GameObject cinemachineCamera;
    [SerializeField] private Transform parent;
    public List<CinemachineCamera> cameras = new();

    private void Start()
    {
        dungeonWrapper = GetComponent<DungeonWrapper>();
        dungeonWrapper.onCameraSpawner += StartSpawning;
    }

    private void OnDisable()
    {
        dungeonWrapper.onCameraSpawner -= StartSpawning;
    }

    private void StartSpawning()
    {
        foreach (RoomWrapper roomWrapper in dungeonWrapper.reducedRooms)
        {
            Vector2 position = roomWrapper.room.center;
            GameObject camera = Instantiate(cinemachineCamera, new Vector3(position.x, 0, position.y), Quaternion.identity);
            camera.transform.parent = parent;
            CinemachineCamera cmCamera = camera.transform.GetChild(0).gameObject.GetComponent<CinemachineCamera>();
            cameras.Add(cmCamera);
        }
        StartCoroutine(dungeonWrapper.ChangeDungeonStatus(DungeonWrapper.DungeonStatus.CamerasSpawned));
    }
}
