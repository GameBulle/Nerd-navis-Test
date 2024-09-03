using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class GachaResult : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Transform SlotTransform;

    Queue<GachaItemSlot> q_SlotQueue;
    GachaItemSlot GachaSlotPrefab;
    Coroutine coroutine;
    WaitForSeconds ReturnInstance;

    public void Initialize()
    {
        GachaSlotPrefab = Resources.Load<GachaItemSlot>("Prefab/GachItemSlot");
        coroutine = null;
        q_SlotQueue = new();
    }

    public void GachaProduction(Dictionary<int, int> ItemsDic)
    {
        ReturnInstance = new WaitForSeconds(0.2f);
        gameObject.SetActive(true);
        if (coroutine != null)
            coroutine = null;
        coroutine = StartCoroutine(OnGachaProduce(ItemsDic.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, x => x.Value)));
        //coroutine = StartCoroutine(OnGachaProduce(ItemsDic));
    }

    IEnumerator OnGachaProduce(Dictionary<int, int> ItemsDic)
    {
        foreach(KeyValuePair<int,int> d in ItemsDic)
        {
            if (q_SlotQueue.Count == 0)
                MakeGachaItemSlot();

            GachaItemSlot Slot = q_SlotQueue.Dequeue();
            Slot.OnDestroy += ReturnBackPool;
            Slot.SetGachaItemSlot(d.Key, d.Value);
            //Slot.transform.SetAsFirstSibling();
            Slot.transform.SetAsLastSibling();
            Slot.gameObject.SetActive(true);
            yield return ReturnInstance;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ReturnInstance = new WaitForSeconds(0f);
    }

    void ReturnBackPool(GachaItemSlot Slot)
    {
        Slot.gameObject.SetActive(false);
        q_SlotQueue.Enqueue(Slot);
    }

    void MakeGachaItemSlot()
    {
        GachaItemSlot Slot = Instantiate(GachaSlotPrefab, SlotTransform);
        q_SlotQueue.Enqueue(Slot);
    }
}
