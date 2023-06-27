using UnityEngine;

namespace Entities.General
{
    public class KeepInScreen : MonoBehaviour
    {
        [SerializeField]
        private float offset = 0.05f;

        private float _upperEdge;
        private float _rightenMostEdge;

        private bool _initialized;
        
        private void Start()
        {
            
        }

        private void InitEdges()
        {
            _upperEdge = Camera.current.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
            _rightenMostEdge = Camera.current.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        }

        private void Update()
        {
            if (Camera.current == null) return;
            
            if (!_initialized)
            {
                InitEdges();
                _initialized = true;
            }
            
            var newPos = transform.position;
            
            if (transform.position.x > _rightenMostEdge)
            {
                newPos.x = -_rightenMostEdge;
            }
            else if(transform.position.x < -_rightenMostEdge)
            {
                newPos.x = _rightenMostEdge;
            }

            if (transform.position.y > _upperEdge)
            {
                newPos.y = -_upperEdge;
            }
            else if (transform.position.y < -_upperEdge)
            {
                newPos.y = _upperEdge;
            }

            newPos.z = 0;

            transform.position = newPos;
        }
    }
}