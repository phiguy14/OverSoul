using UnityEngine;

public class CrowManager : EnemyManager {
    private CrowDiveAttack crowDiveAttack;
    private CrowPeckAttack crowPeckAttack;
    
    public new void Start(){
        base.Start();
        var rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        crowDiveAttack = new CrowDiveAttack(rigidBody);
        crowPeckAttack = new CrowPeckAttack(rigidBody);
        
    }

}