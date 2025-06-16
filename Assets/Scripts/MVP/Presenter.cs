using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Cifkor.Karpusha
{
    public class Presenter : MonoBehaviour
    {
        private WeatherView _weatherView;
        private DogBreedsView _breedsView;
        private View _currentView;

        private ModelWeather _modelWeather;
        private ModelBreeds _modelBreeds;

        private SignalBus _signalBus;


        [Inject]
        private void Construct(WeatherView weatherView, DogBreedsView breedsView, SignalBus signalBus)
        {
            _weatherView = weatherView;
            _breedsView = breedsView;           

            _signalBus = signalBus;
        }

        private void Start()
        {
            Init();
        }

        public void Init()
        {            
            _breedsView.Hide();

            _currentView = _weatherView;
            _currentView.Show();

            _signalBus.Subscribe<ChangeWindowSignal>(ChangePage);
            _signalBus.Subscribe<ShowDogDescription>(ShowBreedDescription);

            _modelWeather = gameObject.AddComponent<ModelWeather>();
            _modelBreeds = gameObject.AddComponent<ModelBreeds>();

            LoadWeatherData();
        }

        private void ChangePage(ChangeWindowSignal changeWindowSignal)
        {
            _currentView.Hide();
            _currentView = changeWindowSignal.View;
            _currentView.Show();

            if (changeWindowSignal.View == _breedsView)
            {
                LoadBreedsData();
            }
        }

        private void LoadWeatherData()
        {
            _modelWeather.FakeAnswert();
            StartCoroutine(ApplyNewWeaterData());
        }

        IEnumerator ApplyNewWeaterData()
        {
            while(_modelWeather.GetNewPeriod() == null)
            {
                yield return null;
            }
            _weatherView.Init(_modelWeather.GetNewPeriod().WeatherTexture, _modelWeather.GetNewPeriod().Temperature.ToString());
        }

        private void LoadBreedsData()
        {
            _modelBreeds.FakeBreedsLoad();
            StartCoroutine(ApplyNewBreedsData());
        }

        IEnumerator ApplyNewBreedsData()
        {
            while (_modelBreeds.GetBreeds() == null)
            {
                yield return null;
            }

            _breedsView.Init(_modelBreeds.GetBreeds());
        }

        private void ShowBreedDescription(ShowDogDescription showDogDescription)
        {
            _modelBreeds.StartLoadBreedDesc(showDogDescription.ID);
            StartCoroutine(ShowBreedsPopUpData(showDogDescription.ID));
        }

        IEnumerator ShowBreedsPopUpData(string id)
        {
            while (_modelBreeds.GetBreedDesc() == "")
            {
                yield return null;
            }

            _breedsView.ShowPopUp(_modelBreeds.GetBreeds()[id], _modelBreeds.GetBreedDesc());
        }
    }
}
