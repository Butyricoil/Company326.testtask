using UnityEngine;
using Zenject;

namespace Cifkor.Karpusha
{
    public class SignalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container
                .DeclareSignal<ChangeWindowSignal>()
                .OptionalSubscriber();

            Container
                .DeclareSignal<ShowDogDescription>()
                .OptionalSubscriber();
        }
    }
}