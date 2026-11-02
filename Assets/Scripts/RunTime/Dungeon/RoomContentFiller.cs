using System.Collections.Generic;
using UnityEngine;

public class RoomContentFiller : ProceduralGenerator
{
    private DungeonWrapper dungeonWrapper;

    [Range(0, 100)][SerializeField] private int seed = 90;
    [Range(0, 100)][SerializeField] private int lootChancePercent = 25;
    [Range(0, 10)][SerializeField] private int roomFillAmount;
    [Range(0, 10)][SerializeField] private float radius = 4;


    [SerializeField] private GameObject lootPrefab;
    [SerializeField] private GameObject enemyPrefab;

    private System.Random numberGenerator;

    private void OnEnable()
    {
        dungeonWrapper = GetComponent<DungeonWrapper>();

        dungeonWrapper.onSpawnRoomSelector += FillRoomsWithContent;
    }

    private void OnDisable()
    {
        dungeonWrapper.onSpawnRoomSelector -= FillRoomsWithContent;
    }

    private void FillRoomsWithContent()
    {
        numberGenerator = new System.Random(seed);
        for (int i = 0; i < dungeonWrapper.reducedRooms.Count; i++)
        {
            if (i == dungeonWrapper.playerSpawnRoomIndex)
            {
                continue;
            }
            if (i == dungeonWrapper.bossSpawnRoomIndex)
            {
                continue;
            }
            List<Vector2> points = PoissonDiscSampling.GeneratePoints(radius, dungeonWrapper.reducedRooms[i].room.size - new Vector2(2 * radius, 2 * radius), seed, 20);
            int randomFillAmount = numberGenerator.Next(0, roomFillAmount);
            Vector2 position = dungeonWrapper.reducedRooms[i].room.position;
            Vector3 roomLocation = new Vector3(position.x + radius, 0, position.y + radius);
            for (int j = 0; j < Mathf.Min(randomFillAmount, points.Count); j++)
            {
                int randomLootChance = numberGenerator.Next(0, 100);
                if (randomLootChance <= lootChancePercent)
                {
                    Instantiate(lootPrefab, new Vector3(points[j].x, 0, points[j].y) + roomLocation, Quaternion.identity);
                }
                else
                {
                    Instantiate(enemyPrefab, new Vector3(points[j].x, 0, points[j].y) + roomLocation, Quaternion.identity);
                }
            }
        }
        StartCoroutine(dungeonWrapper.ChangeDungeonStatus(DungeonWrapper.DungeonStatus.RoomsFilledWithContent));
    }
}
