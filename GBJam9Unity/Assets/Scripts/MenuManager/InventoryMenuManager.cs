using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuManager : PopulatingMenuManager
{
    [SerializeField]
    protected Image ItemImage;
    [SerializeField]
    protected Sprite BackupSprite;

    public override void PopulateMenu(List<EquiptmentSlot> inDetails)
    {
        base.PopulateMenu(inDetails);
        if (MenuItems.Count == 0)
        {
            DialogueBoxControl.Instance.PrintText("It's empty...");
        }
        else
        {
            UpdateInventoryVisuals();
        }
    }

    protected override void ChangeSelectedMenuItem(int change, bool force = false)
    {
        int start = CurrentIndex;
        base.ChangeSelectedMenuItem(change, force);
        if (CurrentIndex != start)
        {
            UpdateInventoryVisuals();
        }
    }

    protected virtual void UpdateInventoryVisuals()
    {
        ItemMenuItem imi = (ItemMenuItem)MenuItems[CurrentIndex];
        Sprite sprite = imi.Slot.Equiptment.ItemImage;
        if (sprite == null)
        {
            sprite = BackupSprite;
        }
        ItemImage.sprite = sprite;
        DialogueBoxControl.Instance.PrintText(imi.Slot.Equiptment.ToString(), timePerChar: 0);
    }

    public override void FeedbackResponse(eLittleFeedback feedback)
    {
        switch (feedback)
        {
            case eLittleFeedback.Wear:

                EquiptmentSlot slot = ((ItemMenuItem)MenuItems[CurrentIndex]).Slot;
                Debug.Log($"Need to wear {slot.Equiptment.Name}");
                eItemType type = slot.Equiptment.ItemType;
                ItemDetails returnedItem = PlayerData.GetChickenData(PlayerData.ActiveChicken).EquippedItems.EquipItem(slot.SubtractItem(), type);
                if(slot.Equiptment == null)
                {
                    MenuItemBase toDestroy = MenuItems[CurrentIndex];
                    RemoveMenuItem(toDestroy);
                    Destroy(toDestroy.gameObject);
                }
                if (returnedItem != null)
                {
                    ItemMenuItem match = null;
                    foreach(MenuItemBase mi in MenuItems)
                    {
                        ItemMenuItem cast = (ItemMenuItem)mi;
                        if(cast.Slot.Equiptment == returnedItem)
                        {
                            match = cast;
                            break;
                        }
                    }
                    if (match != null)
                    {
                        match.AddToCount();
                    }
                    else
                    {
                        ItemMenuItem imi = Instantiate(LootMenuItemPrefab, ItemHolder).GetComponent<ItemMenuItem>();
                        EquiptmentSlot newSlot = new EquiptmentSlot();
                        newSlot.AddItem(returnedItem);
                        imi.SetItem(newSlot);
                        imi.transform.localPosition += (Vector3.down * (InitialGap + EntryHeight * MenuItems.Count));
                        MenuItems.Add(imi);
                    }
                }

                break;
            case eLittleFeedback.Data:
                Debug.Log($"Need to give info on {((ItemMenuItem)MenuItems[CurrentIndex]).Slot.Equiptment.Name}");
                break;
        }
    }
}
