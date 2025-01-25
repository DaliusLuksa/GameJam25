using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class Helpers
{
    public static bool ValidateBubble(Item bubble, ManagerSO managerSO)
    {
        if (bubble == null)
        {
            return false;
        }
        if (bubble.CurrentItemType == ItemType.Complex)
        {
            return bubble.CurrentItemUpgradeLevel < managerSO.MAX_UPGRADE_LEVEL;
        }
        return managerSO.BubbleTypes.Contains(bubble.CurrentItemType);
    }
}

