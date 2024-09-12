using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowInfo : MonoBehaviour
{
    [SerializeField] public TMP_Text ItemName, ItemDescription;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            Debug.Log("TriggerCon Item");
            ItemScript activeItem = other.gameObject.GetComponent<ItemScript>();
           ItemName.text = activeItem.nombre;
            ItemDescription.text = activeItem.descripción;

        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            Debug.Log("TriggerCon Item");
            ItemScript activeItem = other.gameObject.GetComponent<ItemScript>();
            ItemName.text = "";
            ItemDescription.text = "";

        }

    }
}
