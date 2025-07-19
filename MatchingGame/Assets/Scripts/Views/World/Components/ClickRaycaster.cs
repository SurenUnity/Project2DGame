using UnityEngine;

namespace Views.World.Components
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class ClickRaycaster : MonoBehaviour
    {
        private void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                    return;

                TryRaycast(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
#elif UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;

            TryRaycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));
        }
#endif
        }

        private void TryRaycast(Vector2 screenPosition)
        {
            RaycastHit2D hit = Physics2D.Raycast(screenPosition, Vector2.zero);

            if (hit.collider != null)
            {
                var clickable = hit.collider.GetComponent<IClickable>();
                clickable?.Click();
            }
        }
    }
}