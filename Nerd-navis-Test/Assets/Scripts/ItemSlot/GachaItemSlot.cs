using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaItemSlot : ItemSlot, IObjectPooling<GachaItemSlot>
{
    public event Action<GachaItemSlot> OnDestroy;   // 오브젝트 삭제 및 비활성화 됐을 때 실행하는 이벤트

    public override void SetGachaItemSlot(int ItemID, int ItemCount)
    {
        base.SetGachaItemSlot(ItemID, ItemCount);

        ItemCountText.text = ItemCount.ToString();
    }

    private void OnDisable()
    {
        OnDestroy?.Invoke(this);    // 등록된 함수 실행
        OnDestroy = null;   // 함수 중복 실행을 방지하기 위해 등록된 함수 다 제거
    }

    public void SetPosition(Vector2 Position) { }

    public void SetAngle(Vector2 Angle) { }
}
