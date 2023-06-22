using Data.ScriptableObjects;
using UnityEngine;

namespace Systems.InputHandling
{
    public static class InputFunctions
    {
        public static void UpdatePlayerInputKeyboard(ref InputData inputData)
        {
            inputData.ForwardPressed = Input.GetKey(inputData.Bindings.ForwardKey);
            inputData.LeftPressed = Input.GetKey(inputData.Bindings.LeftKey);
            inputData.RightPressed = Input.GetKey(inputData.Bindings.RightKey);
            inputData.FirePressed = Input.GetKeyDown(inputData.Bindings.FireKey);
        }
    }
}