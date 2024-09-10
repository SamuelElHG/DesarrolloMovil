using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filters : MonoBehaviour
{
    [SerializeField] private ItemScript[] inventoryItems;
    //[SerializeField] private string tagSearch;
    public void filterItems(string tagSearch)
    {

        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i].ItemTag == tagSearch)
            {
                inventoryItems[i].gameObject.SetActive(true);
            }
            else
            {
                inventoryItems[i].gameObject.SetActive(false);

            }

        }
    }

    public void ResetFilters()
    {

        for (int i = 0; i < inventoryItems.Length; i++)
        {

            inventoryItems[i].gameObject.SetActive(true);
        }
    }
}
