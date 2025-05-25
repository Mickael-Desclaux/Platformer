using MGA.UniJect;
using MUIStore;

public class DIInstaller : MonoInstaller
{
    public override void Install()
    {
        Container.Bind<UIStore>().AsSingleton().NonLazy();
    }
}
