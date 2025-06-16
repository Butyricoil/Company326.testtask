using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

namespace Cifkor.Karpusha
{
    public class DogBreed : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _buttonName;
        private string _breedID;

        SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Init(string count, string breedName, string breedID)
        {
            _breedID = breedID;
            _buttonName.text = count + " - " + breedName;

            _button.onClick.AddListener(() => _signalBus.Fire(new ShowDogDescription(_breedID)));
        }
    }
}
