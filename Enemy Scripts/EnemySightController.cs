using System;
using UnityEngine;

public class EnemySightController
{
    private Transform player;
    private MonoBehaviour manager;
    private EnemyController controller;
    private ContactFilter2D filter;
    public EnemySightController(MonoBehaviour manager, Transform player)
    {
        this.manager = manager;
        this.player = player;
        controller = manager.GetComponent<EnemyManager>().enemyController;
        filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Player", "Floor"));
    }

    public float LookUpdate(float LookNormalized)
    {
        var distance = Vector2.Distance(player.transform.position, manager.transform.position);
        if (IsPlayerInLineOfSight())
        {
            LookNormalized += 50 * .01f * (1 / distance);
            LookNormalized = Mathf.Clamp(LookNormalized, 0, 1);
        }
        else
        {
            LookNormalized -= .01f;
            LookNormalized = Mathf.Clamp(LookNormalized, 0, 1);
        }
        return LookNormalized;
    }

    private bool IsPlayerInLineOfSight()
    {
        RaycastHit2D[] hits = new RaycastHit2D[3];
        if (controller.IsFacingRight())
            filter.SetNormalAngle(170, 190);
        else { filter.SetNormalAngle(-10, 10); }
        Physics2D.Raycast(manager.transform.position,
                       player.position - manager.transform.position,
                   filter,
                        hits,
                       3f);
        foreach (RaycastHit2D hi in hits)
        {
            if (hi.collider != null)
            {
                if (hi.collider.gameObject.name == "Floor")
                    return false;
                if (hi.collider.gameObject.name == "Player")
                    return true;
            }
        }
        return false;
    }
}