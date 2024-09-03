using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class GachaResult : MonoBehaviour, IPointerClickHandler  // 마우스 클릭 이벤트 핸들러
{
    [SerializeField] Transform SlotTransform;   // Slot의 부모 Transform

    Queue<GachaItemSlot> q_SlotQueue;   // 오브젝트풀 패턴에 사용할 Queue(Pool)
    GachaItemSlot GachaSlotPrefab;
    Coroutine coroutine;    // 뽑기 연출을 위한 코루틴
    WaitForSeconds ReturnInstance;  // GC 방지하기 위해 멤버 변수로 사용

    public void Initialize()
    {
        GachaSlotPrefab = Resources.Load<GachaItemSlot>("Prefab/GachItemSlot");
        coroutine = null;
        q_SlotQueue = new();
    }

    public void GachaProduction(Dictionary<int, int> ItemsDic)
    {
        ReturnInstance = new WaitForSeconds(0.2f);  // 딜레이 시간 초기화
        gameObject.SetActive(true);
        if (coroutine != null)  // 코루틴 함수가 실행 중인지 확인
            coroutine = null;
        // ItemsDic 내림차순으로 정렬
        coroutine = StartCoroutine(OnGachaProduce(ItemsDic.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, x => x.Value)));
    }

    IEnumerator OnGachaProduce(Dictionary<int, int> ItemsDic)
    {
        foreach(KeyValuePair<int,int> d in ItemsDic)
        {
            if (q_SlotQueue.Count == 0) // 현재 사용 가능한 슬롯이 없을 때
                MakeGachaItemSlot();

            GachaItemSlot Slot = q_SlotQueue.Dequeue();
            Slot.OnDestroy += ReturnBackPool;   // OnDestroy 이벤트에 ReturnBackPool 함수 등록
            Slot.SetGachaItemSlot(d.Key, d.Value);
            Slot.transform.SetAsLastSibling();  // 현재 Hierarchy 순서에서 마지막 순서로 변경
            Slot.gameObject.SetActive(true);
            yield return ReturnInstance;
        }
    }

    // 마우스 클릭 시 실행하는 함수
    public void OnPointerClick(PointerEventData eventData)
    {
        // 코루틴 딜레이 시간을 0으로 변경(뽑기 연출 생략)
        ReturnInstance = new WaitForSeconds(0f);
    }

    // 오브젝트(Slot)가 다시 Queue(Pool)로 돌아가게 하는 함수
    void ReturnBackPool(GachaItemSlot Slot)
    {
        Slot.gameObject.SetActive(false);
        q_SlotQueue.Enqueue(Slot);
    }

    // 현재 사용가능한 오브젝트(Slot)이 없을 때, 새로 생성하는 함수
    void MakeGachaItemSlot()
    {
        GachaItemSlot Slot = Instantiate(GachaSlotPrefab, SlotTransform);
        q_SlotQueue.Enqueue(Slot);
    }
}
