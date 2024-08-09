using UnityEngine;

public class EnemyController
{
    EnemyAIController aIController;
    public bool moving = false;
    EnemyManager enemy;

    protected float moveForcePerFixedUpdate;
    protected bool isRight;
    public float verticalMovementMultiplier { get; set; }
    public float controllerNoMovementThreshold { get; set; }
    public EnemyController(EnemyManager enemy)
    {
        this.enemy = enemy;
        verticalMovementMultiplier = 0.75f;
        moveForcePerFixedUpdate = 3.5f;
        controllerNoMovementThreshold = 0.1f;
    }
    public void UpdateIsPlayerFacingRight()
    {

        var movementInput = aIController.GetInput();
        if (!isRight && movementInput.x > controllerNoMovementThreshold) { isRight = true; }
        else if (isRight && movementInput.x < (-1 * controllerNoMovementThreshold)) { isRight = false; }
    }
    public void MainControllerUpdate() { MoveUpdate(); }
    public void MoveUpdate()
    {
        enemy.movementInput = aIController.GetInput();
        UpdateIsPlayerFacingRight();
        var movement = new Vector2(enemy.movementInput.x, enemy.movementInput.y * verticalMovementMultiplier);
        moving = movement.x == 0 ? false : true;
        if (moving)
            enemy.rigidBody.AddForce(2 * movement);
    }
    public void SetAIController(EnemyAIController aIController)
    {
        this.aIController = aIController;

    }

    public virtual bool IsMoving() { return moving; }
    public virtual bool IsFacingRight() { return isRight; }

}


