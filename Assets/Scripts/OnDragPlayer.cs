using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts
{
    public class OnDragPlayer : MonoBehaviour
    {
        public float SpeedMultiplier = 10f;
        public float MinDistance = 10f;
        public float MaxDistance = 100f;
        public bool IsInteractive;
        public GameObject ArrowObject;

        private bool _isDragging;
        private Vector2 _objectScreenPoint;
        private Vector2 _shootDirection;
        private float _force;

        protected void Update()
        {
            if (_isDragging)
            {
                OnDrag();
            }
        }

        public void OnMouseDown()
        {
            if (!IsInteractive) return;
            
            _objectScreenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            _isDragging = true;
        }

        private void OnDrag()
        {
            var heading = _objectScreenPoint - (Vector2)Input.mousePosition;
            var distance = heading.magnitude;
            if (distance > MinDistance)
            {
                if (!ArrowObject.activeSelf)
                {
                    ArrowObject.SetActive(true);
                }
            }
            float angle = Mathf.Atan2(heading.y, heading.x) * Mathf.Rad2Deg;
            ArrowObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            if (distance > MaxDistance)
            {
                distance = MaxDistance;
            }
            ArrowObject.transform.localScale = new Vector2(1 + distance * 0.01f, 1);
            _force = distance * SpeedMultiplier;
            _shootDirection = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
        }

        public void OnMouseUp()
        {
            if (!IsInteractive) return;
            
            _isDragging = false;
            ArrowObject.SetActive(false);
            gameObject.GetComponent<Rigidbody2D>().AddForce(_shootDirection * _force, ForceMode2D.Force);

            GameManager.Instance.SwitchTurn();
        }

    }
}