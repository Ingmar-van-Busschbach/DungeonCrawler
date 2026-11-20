using UnityEngine;
using UnityEngine.AI;

public class PlayerCameraSwitcher : MonoBehaviour
{
    private CameraSpawner cameraSpawner;
    private DungeonWrapper dungeonWrapper;
    public int currentCameraIndex = -1;
    void Start()
    {
        GameObject generator = GameObject.FindGameObjectWithTag("Generator");
        cameraSpawner = generator.GetComponent<CameraSpawner>();
        dungeonWrapper = generator.GetComponent<DungeonWrapper>();
    }

    private void Update()
    {
        int index = -1;
        for(int i = 0; i < dungeonWrapper.reducedRooms.Count; i++)
        {
            Bounds bounds = new Bounds(new Vector3(dungeonWrapper.reducedRooms[i].room.center.x,0, dungeonWrapper.reducedRooms[i].room.center.y), new Vector3(dungeonWrapper.reducedRooms[i].room.width, 10, dungeonWrapper.reducedRooms[i].room.height));
            if (bounds.Contains(transform.position))
            {
                index = i;
                break;
            }
        }
        if (index >= 0 && index != currentCameraIndex)
        {
            if(currentCameraIndex >= 0)
            {
                cameraSpawner.cameras[currentCameraIndex].Priority = 0;
            }
            currentCameraIndex = index;
            cameraSpawner.cameras[currentCameraIndex].Priority = 1;
        }
    }
}
