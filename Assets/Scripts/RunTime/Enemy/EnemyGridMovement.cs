using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent (typeof(EnemyRoomIdentifier))]
public class EnemyGridMovement : MonoBehaviour
{
    [HideInInspector] public PlayerSpawner playerSpawner;

    public UnityEvent onNearPlayer;

    [Range(2, 10)][SerializeField] private float minimumDistanceToMove = 2f;
    [Range(0, 1)][SerializeField] private float randomMoveChance = 0.5f;

    private PlayerGridMovement playerMovement;
    private PlayerCameraSwitcher playerCameraSwitcher;
    private EnemyRoomIdentifier enemyRoomIdentifier;
    private NavMeshAgent navMeshAgent;
    private System.Random random;

    private bool hasPath;
    private float pathEndTreshold = 0.1f;
    
    
    
    public void OnPlayerSpawned(PlayerGridMovement player)
    {
        playerMovement = player;
        playerCameraSwitcher = player.gameObject.GetComponent<PlayerCameraSwitcher>();
    }

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyRoomIdentifier = GetComponent<EnemyRoomIdentifier>();
        random = new System.Random(1);
    }

    private void OnDisable()
    {
        playerSpawner.onPlayerSpawned -= OnPlayerSpawned;
    }

    private void FixedUpdate()
    {
        hasPath |= navMeshAgent.hasPath;
        if (!hasPath)
        {
            return;
        }
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance + pathEndTreshold)
        {
            hasPath = false;
            SingletonEnemyTurnManager.Instance.enemyTurnManager.OnEnemyTurnCompleted();
        }
    }

    public bool Move()
    {
        if(playerCameraSwitcher == null)
        {
            return false;
        }
        if(playerCameraSwitcher.currentCameraIndex != enemyRoomIdentifier.roomIndex)
        {
            return false;
        }
        float randomFollowPlayer = (float)random.NextDouble();
        Vector3 direction = (playerMovement.gameObject.transform.position - transform.position);
        if (direction.magnitude < minimumDistanceToMove)
        {
            onNearPlayer?.Invoke();
            return false;
        }
        if (randomFollowPlayer < randomMoveChance)
        {
            if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.z))
            {
                if(direction.x > 0)
                {
                    direction = Vector3.right;
                }
                else
                {
                    direction = -Vector3.right;
                }
            }
            else
            {
                if (direction.z > 0)
                {
                    direction = Vector3.forward;
                }
                else
                {
                    direction = -Vector3.forward;
                }
            }
        }
        else
        {
            float randomDirection = (float)random.NextDouble();
            if (randomDirection <= 0.25f)
            {
                direction = Vector3.forward;
            }
            else if (randomDirection <= 0.50f)
            {
                direction = -Vector3.forward;
            }
            else if (randomDirection <= 0.75f)
            {
                direction = Vector3.right;
            }
            else
            {
                direction = -Vector3.right;
            }
        }
        direction += transform.position;
        direction = new Vector3(Mathf.Round(direction.x), Mathf.Round(direction.y), Mathf.Round(direction.z));
        navMeshAgent.SetDestination(direction);
        return true;
    }
}
