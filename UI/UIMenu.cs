using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;
using UnityEngine.UIElements;
using Player;
public static class CursorData
{
    public static Item selectedItem;
    public static Item selectedEquip;
    public static int hoveredEquipIndex;
    public static int selectedEquipIndex;
    public static int hoveredItemIndex;
    public static int selectedItemIndex;
    public static int hoveredAbilityIndex;
}
public class UIMenu
{
    VisualElement root;
    VisualElement cursor;
    Label arrow;
    ProgressBar healthBar;
    List<VisualElement> equipment;
    List<Button> inventory;
    List<Label> inventoryText;
    List<VisualElement> ability;
    List<VisualElement> boost;
    PlayerManager manager;
    PlayerInventory<Item> inventoryItems;
    PlayerInventory<Item> equipItems;
    PlayerInventory<Ability> abilityItems;
    PlayerInventory<Ability> boostItems;
    SpriteAtlas sprites;
    public UIMenu(UIDocument uiDocument, PlayerManager manager)
    {
        this.manager = manager;
        this.root = uiDocument.rootVisualElement;

        arrow = root.Q<Label>("ArrowCount");
        cursor = root.Q<VisualElement>("cursor");
        healthBar = root.Q<ProgressBar>("HealthBar");
        equipment = root.Query<VisualElement>("equip").ToList();
        inventory = root.Query<Button>("inventory").ToList();
        inventoryText = root.Query<Label>("inv1").ToList();
        ability = root.Query<VisualElement>("ability").ToList();
        boost = root.Query<VisualElement>("boost").ToList();

        arrow.text = Stats.playerStats[Stat.Arrow].ToString();
        healthBar.highValue = Stats.maxStats[Stat.HP];
        healthBar.lowValue = 0;

        inventoryItems = PersistenceManager.SetupItemInventory("playerInventory", 10);
        abilityItems = PersistenceManager.SetupAbilityInventory("playerAbilities", 10);
        boostItems = PersistenceManager.SetupAbilityInventory("playerBuffs", 6);
        equipItems = PersistenceManager.SetupItemInventory("playerEquipment", 4);

        Stats.playerStats.CollectionChanged += changeStatDisplay;
        Stats.onLVChange += LevelUp;

        Addressables.LoadAssetAsync<SpriteAtlas>("items").Completed += LoadedAtlas;
        foreach (Button v in equipment)
        {
            v.RegisterCallback((KeyDownEvent evnt) => EquipmentClicked(evnt));
            v.RegisterCallback<FocusEvent>((evnt) => EquipmentSelected(evnt, v));
        }
        foreach (Button v in inventory)
        {
            v.RegisterCallback((KeyDownEvent evnt) => ItemClicked(evnt));
            v.RegisterCallback<FocusEvent>((evnt) => ItemSelected(evnt, v));
        }
        foreach (Button v in ability)
        {
            v.RegisterCallback((KeyDownEvent evnt) => AbilityClicked(evnt));
            v.RegisterCallback<FocusEvent>((evnt) => AbilitySelected(evnt, v));
        }
        foreach (Button v in boost) { v.RegisterCallback<FocusEvent>((evnt) => BoostSelected(evnt, v)); }

        inventory[0].Focus();
    }
    public void Unregister()
    {
        Stats.playerStats.CollectionChanged -= changeStatDisplay;
        Stats.onLVChange -= LevelUp;
        inventoryItems.CollectionChanged -= UpdateCollectionDisplay;
        equipItems.CollectionChanged -= UpdateCollectionDisplay;
        foreach (Button v in equipment)
        {
            v.UnregisterCallback((KeyDownEvent evnt) => EquipmentClicked(evnt));
            v.UnregisterCallback<FocusEvent>((evnt) => EquipmentSelected(evnt, v));
        }
        foreach (Button v in inventory)
        {
            v.UnregisterCallback((KeyDownEvent evnt) => ItemClicked(evnt));
            v.UnregisterCallback<FocusEvent>((evnt) => ItemSelected(evnt, v));
        }
        foreach (Button v in ability)
        {
            v.UnregisterCallback((KeyDownEvent evnt) => AbilityClicked(evnt));
            v.UnregisterCallback<FocusEvent>((evnt) => AbilitySelected(evnt, v));
        }
        foreach (Button v in boost) { v.UnregisterCallback<FocusEvent>((evnt) => BoostSelected(evnt, v)); }
    }
    public void EquipmentClicked(KeyDownEvent downEvent)
    {
        if (downEvent.keyCode == KeyCode.Return)
        {
            if (CursorData.selectedItem == null)
            {
                if (!equipItems[CursorData.hoveredEquipIndex].GetName().Equals(""))
                {
                    CursorData.selectedEquipIndex = CursorData.hoveredEquipIndex;
                    CursorData.selectedEquip = equipItems[CursorData.hoveredEquipIndex];
                    cursor.style.backgroundImage = new StyleBackground(sprites.GetSprite(CursorData.selectedEquip.GetName()));
                }
            }
            else
            {
                if (((int)CursorData.selectedItem.itemClass) < 4
                    && equipItems[(int)CursorData.selectedItem.itemClass].GetName().Equals(""))
                {
                    inventoryItems.Equip(equipItems, CursorData.selectedItemIndex);
                    UpdateCollectionDisplay(this, new EventArgs());
                    ResetTransaction();
                }
            }
        }
    }
    public void EquipmentSelected(FocusEvent e, Button b)
    {
        CursorData.hoveredEquipIndex = equipment.IndexOf(b);
        cursor.style.left = b.worldBound.position.x;
        cursor.style.top = b.worldBound.position.y;
    }
    public void ItemClicked(KeyDownEvent downEvent)
    {
        if (downEvent.keyCode == KeyCode.Return)
        {
            if (CursorData.selectedEquip != null)
            {
                equipItems.Unequip(inventoryItems, (int)CursorData.selectedEquip.itemClass);
                ResetTransaction();
            }
            else if (CursorData.selectedItem == null)
            {
                if (!inventoryItems[CursorData.hoveredItemIndex].GetName().Equals(""))
                {
                    CursorData.selectedItemIndex = CursorData.hoveredItemIndex;
                    CursorData.selectedItem = inventoryItems[CursorData.hoveredItemIndex];
                    cursor.style.left = inventory[CursorData.selectedItemIndex].worldBound.position.x;
                    cursor.style.top = inventory[CursorData.selectedItemIndex].worldBound.position.y;
                    cursor.style.backgroundImage = new StyleBackground(sprites.GetSprite(CursorData.selectedItem.GetName()));
                }
            }
            else
            {
                inventoryItems.SwitchSlot(CursorData.hoveredItemIndex, CursorData.selectedItemIndex);
                ResetTransaction();
            }
        }
        if (downEvent.keyCode == KeyCode.Tab)
        {
            Debug.Log("Drop item " + inventoryItems[CursorData.hoveredItemIndex].GetName());
            Drop(inventoryItems[CursorData.hoveredItemIndex]);
            ResetTransaction();
        }
    }
    public void ResetTransaction()
    {
        CursorData.selectedItem = null;
        CursorData.selectedEquip = null;
        cursor.style.backgroundImage = null;
    }
    public void AbilityClicked(KeyDownEvent downEvent)
    {
        if (downEvent.keyCode == KeyCode.Return)
        {
            var ability = abilityItems[CursorData.hoveredAbilityIndex];
            if (!ability.GetName().Equals(""))
            {
                ability.ChangeEquipped();
                Stats.RefreshPlayerStats();
            }
        }
    }
    public void ItemSelected(FocusEvent e, Button b)
    {
        CursorData.hoveredItemIndex = inventory.IndexOf(b);
        cursor.style.left = b.worldBound.position.x;
        cursor.style.top = b.worldBound.position.y;
    }
    public void AbilitySelected(FocusEvent e, Button b) { CursorData.hoveredAbilityIndex = ability.IndexOf(b); }
    public void BoostSelected(FocusEvent e, Button b) { }
    public void LevelUp() { healthBar.highValue = Stats.maxStats[Stat.HP]; }
    public void changeStatDisplay(object sender, EventArgs e)
    {
        var statEventArgs = (StatEventArgs)e;
        switch (statEventArgs.stat)
        {
            case Stat.Arrow:
                arrow.text = Stats.playerStats[Stat.Arrow].ToString();
                break;
            case Stat.HP:
                healthBar.value = Stats.playerStats[Stat.HP];
                break;
            default:
                break;
        }
    }
    public void Drop(Item item)
    {
        if (inventoryItems.Contains(item))
        {
            if (inventoryItems[inventoryItems.IndexOf(item)].itemCount > 1)
                inventoryItems[inventoryItems.IndexOf(item)].itemCount -= 1;
            else
            {
                var i = inventoryItems.RemoveItem(item);
            }
            GameObject pickupObject = manager.gameObject.GetComponent<PlayerManager>().PickupObject;
            //TODO set on player position and make pickup manual    
            var pickup = EnemyManager.Instantiate(pickupObject, new Vector2(manager.transform.position.x - 1, manager.transform.position.y), Quaternion.identity);
            pickup.GetComponent<ItemPickupWrapper>().item = item;
            pickup.GetComponent<SpriteRenderer>().sprite = sprites.GetSprite(item.GetName());
            //change for different drops
            pickup.tag = "Arrow";
            UpdateCollectionDisplay(this, new EventArgs());
        }
    }
    public void UpdateCollectionDisplay(object sender, EventArgs e)
    {
        for (int i = 0; i < inventoryItems.GetCapacity(); i++)
        {
            if (!inventoryItems[i].GetName().Equals(""))
                inventory[i].style.backgroundImage =
                    new StyleBackground(GetInventorySprite(inventoryItems, i));
            else { inventory[i].style.backgroundImage = null; }
            if (inventoryItems[i].itemCount > 0)
                inventoryText[i].text = inventoryItems[i].itemCount.ToString();
            else { inventoryText[i].text = ""; }
        }
        for (int i = 0; i < equipItems.GetCapacity(); i++)
        {
            if (!equipItems[i].GetName().Equals(""))
            {
                equipment[i].style.backgroundImage =
                    new StyleBackground(GetInventorySprite(equipItems, i));
            }
            else { equipment[i].style.backgroundImage = null; }
        }
    }
    public void LoadedAtlas(AsyncOperationHandle<SpriteAtlas> handle)
    {
        try { sprites = handle.Result; }
        catch { }
        inventoryItems.CollectionChanged += UpdateCollectionDisplay;
        equipItems.CollectionChanged += UpdateCollectionDisplay;
        UpdateCollectionDisplay(this, new EventArgs());
    }
    public Sprite GetInventorySprite(PlayerInventory<Item> inventory, int i)
    {
        return sprites.GetSprite(name: inventory[i].GetName());
    }
}