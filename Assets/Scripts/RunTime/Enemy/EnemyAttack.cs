using Damage;
using UnityEngine;

[RequireComponent(typeof(EnemyGridMovement))]
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private DamageStruct weaponSettings;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float attackRange = 1.5f;

    private EnemyGridMovement enemyGridMovement;
    private void OnEnable()
    {
        enemyGridMovement = GetComponent<EnemyGridMovement>();
        enemyGridMovement.onNearPlayer.AddListener(Attack);
    }

    private void OnDisable()
    {
        enemyGridMovement.onNearPlayer.RemoveListener(Attack);
    }

    private void Attack()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, new Vector3(attackRange, attackRange, attackRange), Quaternion.identity, layerMask);
        if(hitColliders.Length > 0)
        {
            foreach(Collider collider in hitColliders)
            {
                if (collider.TryGetComponent(out HealthContainer healthContainer))
                {
                    if (collider.TryGetComponent(out StatusEffectContainer statusEffectContainer))
                    {
                        HitApplicationStrategy.ApplyHit(weaponSettings, healthContainer, statusEffectContainer);
                    }
                }
            }
            
        }
    }
}
