using UnityEngine;
using System.Collections;

public class CrowPeckAttack : EnemyAttack
{
    public float force;
    private Rigidbody2D rigidbody;
    // private new Transform transform;
    private Vector2 direction;
    public CrowPeckAttack(Rigidbody2D rigidbody){
        this.rigidbody = rigidbody;
    }
    

    public override IEnumerator Attack() {
        isAttacking = true;
        isReady = false;
        
         
        rigidbody.AddForce(force * direction); // * transform.localPosition); // TODO verify localPosition will get left/right
        yield return new WaitForSeconds(attackDuration);
         
        isAttacking = false;

        yield return new WaitForSeconds(attackCooldown);
        isReady = true;
         
    }

    public IEnumerator Attack(Vector2 direction) {
        this.direction = direction;
        return Attack();
    }
}