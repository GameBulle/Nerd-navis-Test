using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaItemSlot : ItemSlot, IObjectPooling<GachaItemSlot>
{
    public event Action<GachaItemSlot> OnDestroy;   // ������Ʈ ���� �� ��Ȱ��ȭ ���� �� �����ϴ� �̺�Ʈ

    public override void SetGachaItemSlot(int ItemID, int ItemCount)
    {
        base.SetGachaItemSlot(ItemID, ItemCount);

        ItemCountText.text = ItemCount.ToString();
    }

    private void OnDisable()
    {
        OnDestroy?.Invoke(this);    // ��ϵ� �Լ� ����
        OnDestroy = null;   // �Լ� �ߺ� ������ �����ϱ� ���� ��ϵ� �Լ� �� ����
    }

    public void SetPosition(Vector2 Position) { }

    public void SetAngle(Vector2 Angle) { }
}
