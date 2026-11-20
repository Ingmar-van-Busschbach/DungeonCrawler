using Inventory;
using System;
using UnityEngine;

public class SingletonEnemyTurnManager : MonoBehaviour
{
    public EnemyTurnManager enemyTurnManager;

    public static SingletonEnemyTurnManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        if (enemyTurnManager == null)
        {
            if (!TryGetComponent(out enemyTurnManager))
            {
                enemyTurnManager = gameObject.AddComponent<EnemyTurnManager>();
            }
        }

    }
}
