using MGA.InputMapper;
using MInputsStore;
using UnityEngine.InputSystem;

namespace MPlayer
{
    public class JumpInput : MonoInput
    {
        protected override void OnPerformed(InputAction.CallbackContext context)
        {
            SetJumpAction action = new SetJumpAction(10f);
            InputsStoreSingleton.Instance.Dispatch(action);
        }
    }
}
