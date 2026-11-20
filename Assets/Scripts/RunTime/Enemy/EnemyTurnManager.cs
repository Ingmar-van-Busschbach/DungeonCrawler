using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyTurnManager : MonoBehaviour
{
    public event Action onEnemyTurnCompleted;
    public event Action onPlayerTurnCompleted;
    public List<EnemyGridMovement> enemies;

    public void OnEnemyTurnCompleted()
    {
        onEnemyTurnCompleted?.Invoke();
    }

    public void OnPlayerTurnCompleted()
    {
        bool enemyCanMove = false;
        foreach(EnemyGridMovement enemy in enemies)
        {
            if (enemy.Move())
            {
                enemyCanMove = true;
            }
        }
        if (!enemyCanMove)
        {
            OnEnemyTurnCompleted();
        }
        onPlayerTurnCompleted?.Invoke();
    }
}
