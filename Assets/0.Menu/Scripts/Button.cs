using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(Image))]
public abstract class Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image targetGraphic;

    public Action onClick;

    private Vector2 startScale;

    public float animationModificator = 1.2f;
    internal bool interactable;

    void OnEnable()
    {
        targetGraphic = GetComponent<Image>();
        startScale = transform.localScale;
    }

    public void AddListener(Action onClick)
    {
        this.onClick = onClick;
    }



    public void OnPointerDown(PointerEventData eventData)
    {

        transform.localScale *= animationModificator;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = startScale;
        onClick?.Invoke();
    }
}
