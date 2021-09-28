using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenuMI : MenuItemBase
{
    public override bool PerformAction()
    {
        PauseMenuControl.Instance.ClosePauseMenu();
        return true;
    }
}
