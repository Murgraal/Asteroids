using System;
using Data.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Entities.General
{
    public class KeepInScreen : MonoBehaviour
    {
        [SerializeField]
        private float offset = 0.05f;

        [Inject] GameDataContainer _dataContainer;
        private PlayField _playField => _dataContainer.Data.PlayFieldBoundaries;
        
        private void Update()
        {
            var newPos = transform.position;
            
            if (transform.position.x > _playField.RightenMostEdge)
            {
                newPos.x = -_playField.RightenMostEdge;
            }
            else if(transform.position.x < -_playField.RightenMostEdge)
            {
                newPos.x = _playField.RightenMostEdge;
            }

            if (transform.position.y > _playField.UpperEdge)
            {
                newPos.y = -_playField.UpperEdge;
            }
            else if (transform.position.y < -_playField.UpperEdge)
            {
                newPos.y = _playField.UpperEdge;
            }

            newPos.z = 0;

            transform.position = newPos;
        }
    }

    [Serializable]
    public struct PlayField
    {
        public PlayField(float upperEdge, float rightenMostEdge)
        {
            UpperEdge = upperEdge;
            RightenMostEdge = rightenMostEdge;
        }
        
        public float UpperEdge;
        public float RightenMostEdge;
    }
}