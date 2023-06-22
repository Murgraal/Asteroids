using Systems.InputHandling;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Data/InputData")]
    public class InputData : ScriptableObject
    {
        public InputBindings Bindings;
    
        public bool ForwardPressed;
        public bool LeftPressed;
        public bool RightPressed;
        public bool FirePressed;
    }
}