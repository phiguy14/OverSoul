using UnityEngine;
using System.Collections;

public enum EnemyAttackDirections {
    LeftRight,
    UpDown,
}

public abstract class EnemyAttack
{
    public Vector2 attackVector;
    public float attackDuration;
    public float attackCooldown;
    protected bool isAttacking = false;
    protected bool isReady = true;
    public EnemyAttackDirections[] attackDirections;
    public abstract IEnumerator Attack();
    public virtual bool IsAttacking() {return isAttacking;}
    public virtual bool IsReady() {return isReady;}
}