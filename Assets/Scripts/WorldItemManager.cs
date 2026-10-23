using UnityEngine;

public class WorldItemManager : MonoBehaviour
{
    public void RestoreWorldItems(SaveData data)
    {
        WorldItem[] worldItems = FindObjectsByType<WorldItem>();

        if (data.remainWorldItemIDs == null)
        {
            Debug.LogWarning("No World Item data found.");
            return;
        }

        foreach (WorldItem worldItem in worldItems)
        {
            bool shouldExist = false;

            foreach (string id in data.remainWorldItemIDs)
            {
                if (worldItem.UniqueID == id)
                {
                    shouldExist = true;
                    break;
                }
            }

            if (!shouldExist) Destroy(worldItem.gameObject);
        }
    }

    public string[] GetRemainingWorldItemIds()
    {
        WorldItem[] worldItems = FindObjectsByType<WorldItem>();

        string[] ids = new string[worldItems.Length];

        for (int i = 0; i < worldItems.Length; i++)
        {
            ids[i] = worldItems[i].UniqueID;
        }

        return ids;
    }
}
