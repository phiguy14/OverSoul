using UnityEngine;

public class AnimationController
{
    public Animator playerAnimator;
    private EnemyController enemyController;
    private AnimatorLayers previousFrame;
    public AnimationController(MonoBehaviour manager)
    {
        playerAnimator = manager.gameObject.GetComponent<Animator>();
    }
    public AnimationController(MonoBehaviour manager, EnemyController enemyController)
    {
        this.enemyController = enemyController;
        playerAnimator = manager.gameObject.GetComponent<Animator>();
    }
    public void AnimationUpdate() { Animate(); }
    private void Animate()
    {
        SetDirection();
        if (enemyController.moving) { SetPlayerAnimation(AnimatorLayers.Run); }
        else { SetPlayerAnimation(AnimatorLayers.Stand); }
    }
    protected virtual void SetDirection()
    {
        playerAnimator.SetFloat("x", enemyController.IsFacingRight() ? 1 : -1);
    }
    protected virtual void SetPlayerAnimation(AnimatorLayers thisFrame)
    {
        playerAnimator.SetLayerWeight((int)previousFrame, 0);
        playerAnimator.SetLayerWeight((int)thisFrame, 1);
        previousFrame = thisFrame;
    }
}
