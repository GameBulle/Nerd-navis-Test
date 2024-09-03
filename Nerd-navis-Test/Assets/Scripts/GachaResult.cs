using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class GachaResult : MonoBehaviour, IPointerClickHandler  // ���콺 Ŭ�� �̺�Ʈ �ڵ鷯
{
    [SerializeField] Transform SlotTransform;   // Slot�� �θ� Transform

    Queue<GachaItemSlot> q_SlotQueue;   // ������ƮǮ ���Ͽ� ����� Queue(Pool)
    GachaItemSlot GachaSlotPrefab;
    Coroutine coroutine;    // �̱� ������ ���� �ڷ�ƾ
    WaitForSeconds ReturnInstance;  // GC �����ϱ� ���� ��� ������ ���

    public void Initialize()
    {
        GachaSlotPrefab = Resources.Load<GachaItemSlot>("Prefab/GachItemSlot");
        coroutine = null;
        q_SlotQueue = new();
    }

    public void GachaProduction(Dictionary<int, int> ItemsDic)
    {
        ReturnInstance = new WaitForSeconds(0.2f);  // ������ �ð� �ʱ�ȭ
        gameObject.SetActive(true);
        if (coroutine != null)  // �ڷ�ƾ �Լ��� ���� ������ Ȯ��
            coroutine = null;
        // ItemsDic ������������ ����
        coroutine = StartCoroutine(OnGachaProduce(ItemsDic.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, x => x.Value)));
    }

    IEnumerator OnGachaProduce(Dictionary<int, int> ItemsDic)
    {
        foreach(KeyValuePair<int,int> d in ItemsDic)
        {
            if (q_SlotQueue.Count == 0) // ���� ��� ������ ������ ���� ��
                MakeGachaItemSlot();

            GachaItemSlot Slot = q_SlotQueue.Dequeue();
            Slot.OnDestroy += ReturnBackPool;   // OnDestroy �̺�Ʈ�� ReturnBackPool �Լ� ���
            Slot.SetGachaItemSlot(d.Key, d.Value);
            Slot.transform.SetAsLastSibling();  // ���� Hierarchy �������� ������ ������ ����
            Slot.gameObject.SetActive(true);
            yield return ReturnInstance;
        }
    }

    // ���콺 Ŭ�� �� �����ϴ� �Լ�
    public void OnPointerClick(PointerEventData eventData)
    {
        // �ڷ�ƾ ������ �ð��� 0���� ����(�̱� ���� ����)
        ReturnInstance = new WaitForSeconds(0f);
    }

    // ������Ʈ(Slot)�� �ٽ� Queue(Pool)�� ���ư��� �ϴ� �Լ�
    void ReturnBackPool(GachaItemSlot Slot)
    {
        Slot.gameObject.SetActive(false);
        q_SlotQueue.Enqueue(Slot);
    }

    // ���� ��밡���� ������Ʈ(Slot)�� ���� ��, ���� �����ϴ� �Լ�
    void MakeGachaItemSlot()
    {
        GachaItemSlot Slot = Instantiate(GachaSlotPrefab, SlotTransform);
        q_SlotQueue.Enqueue(Slot);
    }
}
