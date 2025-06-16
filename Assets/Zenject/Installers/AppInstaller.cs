using UnityEngine;
using Zenject;

namespace Cifkor.Karpusha
{
    public class AppInstaller : MonoInstaller
    {
        [SerializeField] private Presenter _presenter;
        [SerializeField] private WeatherView _weatherView;
        [SerializeField] private DogBreedsView _breedsView;
        private WebRequestService _webRequestService;

        public override void InstallBindings()
        {
            BindWebRequestService();
            BindView();
        }

        private void BindWebRequestService()
        {
            _webRequestService = new WebRequestService();

            Container
                .Bind<WebRequestService>()
                .FromInstance(_webRequestService)
                .AsSingle()
                .NonLazy();
        }

        private void BindView()
        {
            WeatherView weatherView = Container
                .InstantiatePrefabForComponent<WeatherView>(_weatherView);
            DogBreedsView dogBreedView = Container
                .InstantiatePrefabForComponent<DogBreedsView>(_breedsView);

            Container
                .Bind<WeatherView>()
                .FromInstance(weatherView)
                .AsSingle();
            
            Container
                .Bind<DogBreedsView>()
                .FromInstance(dogBreedView)
                .AsSingle();
        }
    }
}