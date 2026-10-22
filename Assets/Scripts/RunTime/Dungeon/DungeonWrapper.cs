using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.ComponentModel;

public class DungeonWrapper : MonoBehaviour
{
    
    [Header("Algorithm")]
    [Tooltip("Whether to start generating connections on Start, or to wait for the Generate Connections button to be pressed. This is a global variable overriding the individual algorithms.")]
    public bool globalAutoGenerate = true;
    public ExecutionDelayType executionDelayType = ExecutionDelayType.ManualBetweenSteps;
    [Tooltip("The time delay between generating as part of the algorithm, in seconds. This is a global variable overriding the individual algorithms.")]
    [Range(0, 0.1f)][SerializeField] private float globalExecutionDelay = 0.01f;


    [NonSerialized] public List<RoomWrapper> rooms = new();
    [NonSerialized] public List<RoomWrapper> reducedRooms = new();
    [NonSerialized] public List<DoorWrapper> doors = new();
    [NonSerialized] public List<DoorWrapper> reducedDoors = new();
    [NonSerialized] public RoomWrapper playerSpawnRoom;
    [NonSerialized] public RoomWrapper bossSpawnRoom;
    [NonSerialized] public bool[,] tileMap;
    public DungeonStatus dungeonStatus = DungeonStatus.Empty;

    public event Action onRoomGenerator;
    public event Action onDoorGenerator;
    public event Action onRoomReducer;
    public event Action onCycleReducer;
    public event Action onReducedDrawer;
    public event Action onTileMapGenerator;
    public event Action onMarchingSquareSpawner;
    public event Action onFloodFillSpawner;
    public event Action onSpawnRoomSelector;
    public event Action onPlayerSpawner;
    public event Action onNavMeshGenerator;

    private ProceduralGenerator[] proceduralGenerators;

    private InputSystem_Actions inputActions;
    private InputAction IAContinue;
    private bool continueStep = false;

    // Apply global execution settings
    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        proceduralGenerators = GetComponents<ProceduralGenerator>();
        ApplyGlobalSettings();
        StartCoroutine(CheckGlobalVariablesChanged());
    }

    //Declare inputs
    private void OnEnable()
    {
        IAContinue = inputActions.Player.Jump;
        IAContinue.Enable();
    }

    private void OnDisable()
    {
        IAContinue.Disable();
    }

    private void Update()
    {
        if (IAContinue.WasPressedThisFrame() && !continueStep)
        {
            continueStep = true;
        }
    }
    private void ApplyGlobalSettings()
    {
        foreach(ProceduralGenerator generator in proceduralGenerators)
        {
            if (globalAutoGenerate)
            {
                generator.autoGenerate = true;
            }
            if (executionDelayType != ExecutionDelayType.None)
            {
                generator.executionDelay = globalExecutionDelay;
            }
            else
            {
                generator.executionDelay = 0;
            }
        }
    }

    private IEnumerator CheckGlobalVariablesChanged()
    {
        ExecutionDelayType currentType = executionDelayType;
        float currentDelay = globalExecutionDelay;
        while (true)
        {
            yield return new WaitUntil(() => currentType != executionDelayType || currentDelay != globalExecutionDelay);
            ApplyGlobalSettings();
        }
    }

    public IEnumerator ChangeDungeonStatus(DungeonStatus dungeonStatus)
    {
        this.dungeonStatus = dungeonStatus;
        if (executionDelayType == ExecutionDelayType.ManualBetweenSteps && dungeonStatus != DungeonStatus.DungeonGenerationComplete)
        {
            yield return new WaitUntil(() => continueStep);
            continueStep = false;
        }
        switch (dungeonStatus)
        {
            case DungeonStatus.RoomsCompleted:
                onDoorGenerator?.Invoke();
                break;
            case DungeonStatus.DoorsCompleted:
                onRoomReducer?.Invoke();
                break;
            case DungeonStatus.RoomsReduced:
                onCycleReducer?.Invoke();
                break;
            case DungeonStatus.CyclesReduced:
                onReducedDrawer?.Invoke();
                break;
            case DungeonStatus.RoomsDrawn:
                onTileMapGenerator?.Invoke();
                break;
            case DungeonStatus.TileMapGenerated:
                onMarchingSquareSpawner?.Invoke();
                break;
            case DungeonStatus.WallsSpawned:
                onFloodFillSpawner?.Invoke();
                break;
            case DungeonStatus.FloorSpawned:
                onNavMeshGenerator?.Invoke();
                break;
            case DungeonStatus.NavMeshGenerated:
                onSpawnRoomSelector?.Invoke();
                break;
            case DungeonStatus.SpawnRoomSelected:
                onPlayerSpawner?.Invoke();
                break;
            case DungeonStatus.PlayerSpawned:
                StartCoroutine(ChangeDungeonStatus(DungeonStatus.DungeonGenerationComplete));
                break;
            case DungeonStatus.DungeonGenerationComplete:
                Debug.Log("Dungeon generation complete");
                break;
        }
    }

    public enum DungeonStatus { Empty, RoomsCompleted, DoorsCompleted, RoomsReduced, CyclesReduced, RoomsDrawn, TileMapGenerated, WallsSpawned, FloorSpawned, NavMeshGenerated, SpawnRoomSelected, PlayerSpawned, DungeonGenerationComplete }
    public enum ExecutionDelayType { None, ManualBetweenSteps, Automatic }
}
