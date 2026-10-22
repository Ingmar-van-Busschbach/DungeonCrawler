using UnityEngine;
using UnityEngine.AI;

public class PlayerCameraSwitcher : MonoBehaviour
{
    private CameraSpawner cameraSpawner;
    private DungeonWrapper dungeonWrapper;
    int currentCameraIndex = -1;
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
            currentCameraIndex = index;
            for (int i = 0; i < cameraSpawner.cameras.Count; i++)
            {
                if(i == index)
                {
                    cameraSpawner.cameras[i].Priority = 1;
                }
                else
                {
                    cameraSpawner.cameras[i].Priority = 0;
                }
            }
        }
    }
}
