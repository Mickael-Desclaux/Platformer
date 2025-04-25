using MGA.InputMapper;
using MInputsStore;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MPlayer
{
    public class MoveInput : MonoInput
    {
        protected override void OnPerformed(InputAction.CallbackContext context)
        {
            SetMoveAction action = new SetMoveAction(context.ReadValue<Vector2>());
            InputsStoreSingleton.Instance.Dispatch(action);
        }

        protected override void OnCanceled(InputAction.CallbackContext context)
        {
            SetMoveAction action = new SetMoveAction(Vector2.zero);
            InputsStoreSingleton.Instance.Dispatch(action);
        }
    }
}
