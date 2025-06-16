using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Cifkor.Karpusha
{
    public class DogBreedsView : View
    {
        [SerializeField] private Transform _dogBreedListRoot;
        [SerializeField] private DogBreed _dogBreedPrefab;
        [SerializeField] private DogBreedsPopUp _dogBreedsPopUp;

        private List<DogBreed> _breedsListButton = new List<DogBreed>();

        public void Init(Dictionary<string, string> keyValuePairs)
        {
            _dogBreedsPopUp.gameObject.SetActive(false);

            int count = 0;
            foreach (KeyValuePair<string, string> keyVP in keyValuePairs)
            {
                var breed = Instantiate(_dogBreedPrefab, _dogBreedListRoot);
                breed.Construct(SignalBus);
                breed.Init((++count).ToString(), keyVP.Value, keyVP.Key);
                _breedsListButton.Add(breed);
            }
        }

        public override void Hide()
        {
            foreach (DogBreed dogBreed in _breedsListButton)
                dogBreed.gameObject.SetActive(false);

            _dogBreedsPopUp.gameObject.SetActive(false);
            ChangeView.interactable = true;
        }

        public override void Show()
        {
            ChangeView.interactable = false;
        }

        public void ShowPopUp(string title, string desc)
        {
            _dogBreedsPopUp.gameObject.SetActive(true);
            var button = _dogBreedsPopUp.Init(title, desc);
            button.onClick.AddListener(() => { _dogBreedsPopUp.gameObject.SetActive(false); Show(); });            
        }
    }
}
