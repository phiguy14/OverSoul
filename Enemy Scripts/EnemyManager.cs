using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
public enum EnemyType
{
    Slime,
    Crow
}
public class EnemyManager : MonoBehaviour
{
    protected EnemyAIController aiController;
    public EnemyController enemyController;
    public GameObject pickupObject;
    protected AnimationController animator;
    public InventoryManager inventoryManager;
    public Dictionary<Stat, int> stats;
    private SpriteRenderer spriteRenderer;
    public Rigidbody2D rigidBody;
    public Vector2 movementInput;
    public void Start()
    {
        DamageHandler.SetEnemyDamage(this.gameObject.GetHashCode(), -1);
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        enemyController = new EnemyController(this);
        aiController = new EnemyAIController(this);
        animator = new AnimationController(this, enemyController);
        enemyController.SetAIController(aiController);
        inventoryManager = new InventoryManager(this);
        stats = new Dictionary<Stat, int>();
        stats[Stat.HP] = 2;
    }
    public void FixedUpdate()
    {
        aiController.AIUpdate();
        enemyController.MoveUpdate();
        animator.AnimationUpdate();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null
        && (other.gameObject.tag == "Sword" || other.gameObject.tag == "Arrow"))
        {
            if (other.gameObject.tag == "Arrow")
            {
                var arrowStat = new Dictionary<Stat, int>();
                arrowStat.Add(Stat.Arrow, 1);
                var arrow = new Item("arrow", "oh, you're back!", 5, arrowStat);
                inventoryManager.equipment.AddItem(arrow);

            }
            var isRight = (other.transform.position - gameObject.transform.position).x < 0 ? 1 : -1;
            var knockback = new Vector2(isRight * 200, 0);
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            rigidBody.AddForce(knockback);
            var damage = DamageHandler.GetPlayerDamage(other.gameObject.GetHashCode());
            if (damage >= 0)
            {
                stats[Stat.HP] -= damage;
                if (damageTimer != null)
                {
                    StopCoroutine(damageTimer);
                    damageTimer = null;
                }
                damageTimer = IndicateDamage(.5f);
                StartCoroutine(damageTimer);
            }
        }
    }
    public void OnDestroy()
    {
        Stats.playerExp += 5;
        inventoryManager.OnDestroy();
    }
    private IEnumerator damageTimer;
    private IEnumerator IndicateDamage(float damageCountdown)
    {
        spriteRenderer.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(damageCountdown);
        spriteRenderer.color = new Color(1, 1, 1, 1);
        if (stats[Stat.HP] <= 0)
            Destroy(this.gameObject);
    }
}

