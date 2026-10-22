using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public event Action<float> onCompletionChanged;

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
        int count = (int)DungeonStatus.DungeonGenerationComplete;
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
                onCompletionChanged?.Invoke((float)dungeonStatus / count);
                break;
            case DungeonStatus.DoorsCompleted:
                onCompletionChanged?.Invoke((float)dungeonStatus / count);
                onRoomReducer?.Invoke();
                break;
            case DungeonStatus.RoomsReduced:
                onCompletionChanged?.Invoke((float)dungeonStatus / count);
                onCycleReducer?.Invoke();
                break;
            case DungeonStatus.CyclesReduced:
                onCompletionChanged?.Invoke((float)dungeonStatus / count);
                onReducedDrawer?.Invoke();
                break;
            case DungeonStatus.RoomsDrawn:
                onCompletionChanged?.Invoke((float)dungeonStatus / count);
                onTileMapGenerator?.Invoke();
                break;
            case DungeonStatus.TileMapGenerated:
                onCompletionChanged?.Invoke((float)dungeonStatus / count);
                onMarchingSquareSpawner?.Invoke();
                break;
            case DungeonStatus.WallsSpawned:
                onCompletionChanged?.Invoke((float)dungeonStatus / count);
                onFloodFillSpawner?.Invoke();
                break;
            case DungeonStatus.FloorSpawned:
                onCompletionChanged?.Invoke((float)dungeonStatus / count);
                onNavMeshGenerator?.Invoke();
                break;
            case DungeonStatus.NavMeshGenerated:
                onCompletionChanged?.Invoke((float)dungeonStatus / count);
                onSpawnRoomSelector?.Invoke();
                break;
            case DungeonStatus.SpawnRoomSelected:
                onCompletionChanged?.Invoke((float)dungeonStatus / count);
                onPlayerSpawner?.Invoke();
                break;
            case DungeonStatus.PlayerSpawned:
                onCompletionChanged?.Invoke((float)dungeonStatus / count);
                StartCoroutine(ChangeDungeonStatus(DungeonStatus.DungeonGenerationComplete));
                break;
            case DungeonStatus.DungeonGenerationComplete:
                onCompletionChanged?.Invoke((float)dungeonStatus / count);                   
                break;
        }
    }

    public enum DungeonStatus { Empty = 0, RoomsCompleted, DoorsCompleted, RoomsReduced, CyclesReduced, RoomsDrawn, TileMapGenerated, WallsSpawned, FloorSpawned, NavMeshGenerated, SpawnRoomSelected, PlayerSpawned, DungeonGenerationComplete}
    public enum ExecutionDelayType { None, ManualBetweenSteps, Automatic }
}
