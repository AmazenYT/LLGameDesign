using UnityEngine;
using UnityEngine.EventSystems;
namespace GoSystem
{
    [GoSystem.GBehaviourAttribute("Touch Space",false)]
    public class GoTouchAxis : GoSystemsBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [HideInInspector]
        public Vector3 newCameraAxis;
        private Vector2 TouchCoord;
        private bool isTouching = false;
        private Vector2 lastTouchPosition;

        void Update()
        {
            if (isTouching)
            {
                Vector2 currentTouchPosition = Input.mousePosition;

                // حساب الفرق (delta) بين لمستين
                Vector2 delta = currentTouchPosition - lastTouchPosition;

                // حفظ موضع اللمسة الحالي لفريم التحديث القادم
                lastTouchPosition = currentTouchPosition;

                // تحويل إلى نسب الشاشة (اختياري إذا كانت الحساسية عالية)
                newCameraAxis = new Vector3(delta.x / Screen.width, delta.y / Screen.height, 0);
            }
            else
            {
                // تصفير الحركة عند عدم اللمس
                newCameraAxis = Vector3.zero;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isTouching = true;
            lastTouchPosition = Input.mousePosition;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isTouching = false;
            lastTouchPosition = TouchCoord;
        }
    }
}
