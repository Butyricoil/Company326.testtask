using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Cifkor.Karpusha
{
    public class DogBreedsPopUp : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Button _buttonOk;

        public Button Init(string title, string desc)
        {
            _title.text = title;
            _description.text = desc;
            return _buttonOk;
        }
    }
}
