using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RawImage joystickBackground;
    public RawImage joystickStick;
    public Vector2 inputVector;

    private void Start()
    {
        joystickStick.rectTransform.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = RectTransformUtility.WorldToScreenPoint(Camera.main, joystickBackground.transform.position);
        Vector2 radius = joystickBackground.rectTransform.sizeDelta / 2;

        inputVector = (eventData.position - position) / radius.magnitude;
        inputVector = Vector2.ClampMagnitude(inputVector, 1);

        joystickStick.rectTransform.anchoredPosition = inputVector * radius;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystickStick.rectTransform.anchoredPosition = Vector2.zero;
    }
}