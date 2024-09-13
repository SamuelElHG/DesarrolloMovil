using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ItemScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] public string ItemTag, nombre, descripción;


    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Transform startTransform;
    private Transform endTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f; // Hacer el objeto semitransparente
        //canvasGroup.blocksRaycasts = false; // Permitir que los raycasts pasen a través del objeto
        startTransform = gameObject.transform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas != null)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f; // Restaurar la opacidad
        //canvasGroup.blocksRaycasts = true; // Volver a bloquear raycasts
        transform.position = endTransform.position;
    }

    /*   private void OnCollisionEnter2D(Collision2D collision)
       {
           Debug.Log("Colisión detectada con: " + collision.gameObject.name);
           endTransform = collision.gameObject.transform;
       }*/
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger detectado con: " + other.gameObject.name);
        //other.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        if (other.CompareTag("EquipmentSlot") || other.CompareTag("SlotZone"))
        {
            endTransform = other.transform;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        other.gameObject.tag = "SlotZone";
    }

    private void OnTriggerStay2D(Collider2D collision) //cuando salga puede tener el tag normal
    {
        collision.gameObject.tag = "SlotZoneOccupied";

    }
}
