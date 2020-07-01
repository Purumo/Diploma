using UnityEngine;
using UnityEngine.EventSystems;
namespace GameScene.BulletsModule.PointerEnterExitAction
{
    [System.Serializable]
    public class PointerEvent :         UnityEngine.Events.UnityEvent<PointerEventData> { };
    public class PointerEnterExitHandler : MonoBehaviour, 
        IPointerEnterHandler, IPointerExitHandler
    {
        public PointerEvent onPointerEnterEvent;
        public PointerEvent onPointerExitEvent;

        public void OnPointerEnter(PointerEventData data)
        {
            if (onPointerEnterEvent != null)
                onPointerEnterEvent.Invoke(data);
        }
        public void OnPointerExit(PointerEventData data)
        {
            if (onPointerExitEvent != null)
                onPointerExitEvent.Invoke(data);
        }
    }
}