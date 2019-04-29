﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonModule : Module, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] float incomePerClick = 1;
    [SerializeField] SpriteRenderer up = null;
    [SerializeField] SpriteRenderer down = null;

    int amountOfSimultaneousPresses = 0;
    bool wasRecentlyCancelled = false;
    public bool IsDown { get => amountOfSimultaneousPresses > 0; }
    public bool IsUp { get => !IsDown; }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        CancelAllPresses();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Press();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Release();
    }

    public void Press()
    {
        amountOfSimultaneousPresses++;
        wasRecentlyCancelled = false;
        RefreshVisual();
    }

    void RefreshVisual()
    {
        down.enabled = IsDown;
        up.enabled = IsUp;
    }

    void CancelAllPresses()
    {
        amountOfSimultaneousPresses = 0;
        wasRecentlyCancelled = true;
        RefreshVisual();
    }

    public void Release()
    {
        if (wasRecentlyCancelled)
        {
            amountOfSimultaneousPresses = 0;
            wasRecentlyCancelled = false;
            RefreshVisual();
            return;
        }

        amountOfSimultaneousPresses--;
        if (IsUp && IsPowered)
        {
            GenerateIncome(incomePerClick);
        }
        RefreshVisual();
    }

    void OnDrawGizmos()
    {
        if (IsUp)
        {
            Gizmos.color = Color.blue;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawSphere(transform.position + new Vector3(0.5f, 0.5f), 0.25f);
    }
}
