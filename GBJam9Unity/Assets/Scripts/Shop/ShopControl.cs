using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopControl : MonoBehaviour
{
    [SerializeField]
    ShopItem SpecialItem;
    [SerializeField]
    ShopItem DefenseItem;
    [SerializeField]
    ShopItem AttackItem;

    [SerializeField]
    ItemPool SpecialItemPool;
    [SerializeField]
    ItemPool DefenseItemPool;
    [SerializeField]
    ItemPool AttackItemPool;

    [SerializeField]
    string ShopText;


    private void Start()
    {
        PopulateShop();
        DialogueBoxControl.Instance.PrintText(ShopText, closeAfterText: false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpecialItem.Selected();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DefenseItem.Selected();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            AttackItem.Selected();
        }
    }

    private void PopulateShop()
    {
        int random = Random.Range(0, SpecialItemPool.Items.Count);

        SpecialItem.SetUp(SpecialItemPool.Items[random]);

        random = Random.Range(0, DefenseItemPool.Items.Count);

        DefenseItem.SetUp(DefenseItemPool.Items[random]);

        random = Random.Range(0, AttackItemPool.Items.Count);

        AttackItem.SetUp(AttackItemPool.Items[random]);

    }


#if DEVELOPMENT_BUILD || UNITY_EDITOR
    private void OnEnable()
    {
        DebugConsole.ConsoleEvent += OnDebugConsole;
    }

    private void OnDisable()
    {
        DebugConsole.ConsoleEvent -= OnDebugConsole;
    }

    private void OnDebugConsole(string message)
    {
        var parts = message.Split(' ');

        
        if (parts[0].ToLower().Equals("shop"))
        {
            if (parts[1].ToLower() == "reset")
            {
                PopulateShop();
            }
        }
    }
#endif
}
