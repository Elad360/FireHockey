using UnityEngine;

namespace Assets.Scripts
{
    public class OnDragPlayer : MonoBehaviour
    {
        public float SpeedMultiplier = 10f;
        public float MaxDistance = 300f;
        public GameObject ArrowObject;

        private bool _isDragging;
        private Vector3 _objectScreenPoint;
        private Vector3 _shootDirection;
        private float _force;

        protected void Awake()
        {
        }
    
        protected void Start()
        {
        
        }

        protected void Update()
        {
            if (_isDragging)
            {
                OnDrag();
            }
        }

        public void OnMouseDown()
        {
            _objectScreenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            _isDragging = true;
        }

        private void OnDrag()
        {
            var heading = _objectScreenPoint - Input.mousePosition;
            var distance = heading.magnitude;
            if (distance > 20f)
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
            ArrowObject.transform.localScale = new Vector2(1 + distance * 0.02f, 1);
            _force = distance * SpeedMultiplier;
            _shootDirection = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
        }

        public void OnMouseUp()
        {
            _isDragging = false;
            ArrowObject.SetActive(false);
            gameObject.GetComponent<Rigidbody2D>().AddForce(_shootDirection * _force, ForceMode2D.Force);
        }

    }
}