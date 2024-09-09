using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField, Header("Variables")] private float jumpForce;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private string Dif;

    public void Start()
    {
        Debug.Log("this is the beggining");
    }

    public void OnMouseDown()
    {
        if (Dif=="Jump")
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            Debug.Log("salto");
        }
            
        
    }
}
