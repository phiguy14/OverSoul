using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;

public class InventoryManager
{
    public Inventory<Item> inventory, equipment;
    public Inventory<Ability> abilities, buffs;
    protected MonoBehaviour manager;
    private SpriteRenderer renderer;
    private SpriteAtlas atlas;
    public InventoryManager(MonoBehaviour manager)
    {
        this.manager = manager;
        SetInventories();
    }
    protected void SetInventories()
    {
        equipment = new Inventory<Item>(4);
        buffs = new Inventory<Ability>(4);
        //RandomizeSetup();
        InitiateInventoryStats();
    }
    public void OnDestroy() { DropLoot(); }
    protected void InitiateInventoryStats()
    {
        Dictionary<Stat, int>[] stats = new Dictionary<Stat, int>[] { Stats.SetStats(equipment), Stats.SetStats(buffs) };
        Stats.RefreshMobStats(stats);
    }
    private void RandomizeSetup() { }
    public GameObject DropLoot()
    {

        foreach (Item item in equipment)
        {
            if (Random.Range(0, 10) >= 2)
            {
                var i = equipment.RemoveItem(item);
                if (i.Item2 >= 0)
                {
                    Debug.Log(i.Item2);
                    GameObject pickupObject = manager.gameObject.GetComponent<EnemyManager>().pickupObject;

                    var pickup = EnemyManager.Instantiate(pickupObject, manager.transform.position, Quaternion.identity);
                    pickup.GetComponent<ItemPickupWrapper>().item = i.Item1;
                    renderer = pickup.GetComponent<SpriteRenderer>();
                    Addressables.LoadAssetAsync<SpriteAtlas>("items").Completed += AssignSprite;
                    //change for different drops
                    pickup.tag = "Arrow";
                    return pickupObject;
                }
            }
        }
        return null;
    }

    public void AssignSprite(AsyncOperationHandle<SpriteAtlas> handle)
    {
        try { atlas = handle.Result; }
        catch { }
        //change to handle other sprites TODO
        if (renderer)
            renderer.sprite = atlas.GetSprite("arrow");
    }
}