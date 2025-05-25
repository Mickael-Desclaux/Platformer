using MGA.UniFlux;
using MUIStore.State;

namespace MUIStore
{
    public class UIStore : Store
    {
        public UIStore()
        {
            States.Add(new TestUIState());
        }
    }
}
