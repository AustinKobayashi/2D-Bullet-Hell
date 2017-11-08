using System.Collections.Generic;
using UnityEngine;

public class ItemRarityDatabase : MonoBehaviour {
    private List<int> common;
    private List<int> rare;
    private List<int> epic;
    private List<int> legendary;
    private List<int> mythic;
    
    public void updateDB(List<Item> db) {
        common = new List<int>();
        rare = new List<int>();
        epic = new List<int>();
        legendary = new List<int>();
        mythic = new List<int>();
        for (int i = 0; i < db.Count; i++) {
            switch (db[i].GetItemRarity()) {
                case ItemRarity.Common:
                    common.Add(i);
                    break;
                case ItemRarity.Rare:
                    rare.Add(i);
                    break;
                case ItemRarity.Epic:
                    epic.Add(i);
                    break;
                case ItemRarity.Legendary:
                    legendary.Add(i);
                    break;
                case ItemRarity.Mythic:
                    mythic.Add(i);
                    break;
            }
        }
    }

    public int getRandomOfRarity(ItemRarity r) {
        switch (r) {
            case ItemRarity.Common:
                if (common.Count == 0) return -1;
                return common[Random.Range(0, common.Count-1)];
            case ItemRarity.Rare:
                if (common.Count == 0) return -1;
                return rare[Random.Range(0, rare.Count-1)];
            case ItemRarity.Epic:
                if (common.Count == 0) return -1;
                return epic[Random.Range(0, epic.Count-1)];
            case ItemRarity.Legendary:
                if (common.Count == 0) return -1;
                return legendary[Random.Range(0, legendary.Count-1)];
            case ItemRarity.Mythic:
                if (common.Count == 0) return -1;
                return mythic[Random.Range(0, mythic.Count-1)];
        }
        return -1;
    }
}