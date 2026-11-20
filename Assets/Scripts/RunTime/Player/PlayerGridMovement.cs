
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerGridMovement : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private NavMeshAgent navMeshAgent;
    private bool hasPath;
    private float pathEndTreshold = 0.1f;
    private bool canMove = true;


    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SingletonEnemyTurnManager.Instance.enemyTurnManager.onEnemyTurnCompleted += OnEnemyMoveCompleted;
    }

    private void FixedUpdate()
    {
        if (!navMeshAgent.hasPath)
        {
            hasPath = false;
            return;
        }
        if (hasPath && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance + pathEndTreshold)
        {
            hasPath = false;
            SingletonEnemyTurnManager.Instance.enemyTurnManager.OnPlayerTurnCompleted();
        }
    }

    private void OnEnemyMoveCompleted()
    {
        canMove = true;
    }

    public void Move(InputAction.CallbackContext callbackContext)
    {
        if (!canMove)
        {
            return;
        }
        Vector2 input = callbackContext.ReadValue<Vector2>();
        if (input == null)
        {
            return;
        }
        if(input.magnitude == 0)
        {
            return;
        }
        Collider[] hitColliders = Physics.OverlapBox(transform.position + new Vector3(input.x, 1, input.y), new Vector3(0.8f, 0.8f, 0.8f), Quaternion.identity, layerMask);
        if(hitColliders.Length == 0)
        {
            Vector3 destination = transform.position + new Vector3(input.x, 0 , input.y);
            destination = new Vector3(Mathf.Round(destination.x), Mathf.Round(destination.y), Mathf.Round(destination.z));
            navMeshAgent.SetDestination(destination);
            hasPath = true;
            canMove = false;
        }
    }
}
