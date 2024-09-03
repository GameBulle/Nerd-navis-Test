using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaItemSlot : ItemSlot, IObjectPooling<GachaItemSlot>
{
    public event Action<GachaItemSlot> OnDestroy;

    public void SetAngle(Vector2 Angle)
    {
        throw new NotImplementedException();
    }

    public override void SetGachaItemSlot(int ItemID, int ItemCount)
    {
        base.SetGachaItemSlot(ItemID, ItemCount);

        ItemCountText.text = ItemCount.ToString();
    }

    public void SetPosition(Vector2 Position)
    {
        throw new NotImplementedException();
    }

    private void OnDisable()
    {
        OnDestroy?.Invoke(this);
        OnDestroy = null;
    }
}
