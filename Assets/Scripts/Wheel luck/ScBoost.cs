using System.Collections;
using System.Collections.Generic;
using UnityEngine;





[CreateAssetMenu(fileName = "ItemData", menuName = "Item Data", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;
    public int dropChance;
}

public class RandomItemPicker : MonoBehaviour
{
    public ItemData[] items;

    private void OnEnable()
    {
        PickRandomItem();
    }

    public void PickRandomItem()
    {
        int totalDropChance = 0;

        // ��������� ����� ���� ��������� ���� ���������
        foreach (ItemData item in items)
            totalDropChance += item.dropChance;

        int randomValue = Random.Range(0, totalDropChance);

        // ��������� �� ����� ��������� ������ ��������� ����� � �������� �������
        for (int i = 0; i < items.Length; i++)
        {
            if (randomValue < items[i].dropChance)
            {
                Debug.Log("Picked item: " + items[i].itemName);
                break;
            }
            else
            {
                randomValue -= items[i].dropChance;
            }
        }
    }
}