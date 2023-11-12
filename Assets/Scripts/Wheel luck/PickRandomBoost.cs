using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Item
{
    public string name;
    public int number;
    public float dropChance;
}

public class PickRandomBoost : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public void AddItem(string name, int number, float dropChance)
    {
        Item newItem = new Item();
        newItem.name = name;
        newItem.number = number;
        newItem.dropChance = dropChance;
        items.Add(newItem);
    }

    public int GetRandomItemNumber()
    {
        float totalChance = 0f;
        foreach (Item item in items)
        {
            totalChance += item.dropChance;
        }

        float randomNumber = Random.Range(0f, totalChance);

        foreach (Item item in items)
        {
            if (randomNumber <= item.dropChance)
            {
                return item.number;
            }
            randomNumber -= item.dropChance;
        }

        return 0;
    }
}

