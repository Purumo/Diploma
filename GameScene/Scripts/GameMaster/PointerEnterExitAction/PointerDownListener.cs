//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.Events;

//namespace GameScene.GameMaster.PointerDownAction
//{
//    public class PointerDownListener : MonoBehaviour
//    {
//        public PointerDownHandler pointerDownHandler;
//        public UnityAction<PointerEventData> pointerDownFunctionCall;

//        private void Start()
//        {
//            pointerDownHandler.onPointerDownEvent.AddListener(OnPointerDown);
//        }

//        public void OnPointerDown(PointerEventData data)
//        {
//            pointerDownFunctionCall(data);
//        }
//    }
//}