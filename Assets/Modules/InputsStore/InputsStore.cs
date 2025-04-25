using MGA.UniFlux;
using UnityEngine;

namespace MInputsStore
{
    public class InputsStore : Store
    {
        public InputsStore()
        {
            States.Add(new MoveState(Vector2.zero));
        }
    }
}
