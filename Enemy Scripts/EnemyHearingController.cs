using UnityEngine;
using Player;

public class EnemyHearingController
{
    private Transform player;
    private MonoBehaviour manager;
    private PlayerMainController controller;
    private ContactFilter2D filter;
    private RaycastHit2D[] hits;
    public EnemyHearingController(MonoBehaviour manager, Transform player)
    {
        filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Player"));
        this.manager = manager;
        this.player = player;
        controller = player.GetComponent<PlayerManager>().MainController;
    }

    public float ListenUpdate(float listenNormalized)
    {
        hits = new RaycastHit2D[1];
        var distance = Vector2.Distance(manager.transform.position, player.transform.position);
        var playerMoving = controller.IsMoving;
        Physics2D.Raycast(manager.transform.position,
                       player.position - manager.transform.position,
                   filter,
                         hits,
                        4f);
        if (hits[0]
        && hits[0].collider.gameObject.name == "Player"
        && playerMoving)
        {
            listenNormalized += 50 * (1 / distance);
            listenNormalized = Mathf.Clamp(listenNormalized, 0, 1);
        }
        else
        {
            listenNormalized -= .001f;
            listenNormalized = Mathf.Clamp(listenNormalized, 0, 1);
        }
        return listenNormalized;
    }
}