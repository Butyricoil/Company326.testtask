using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Cifkor.Karpusha
{
    public class WeatherView : View
    {
        [SerializeField] private Image _weatherImage;
        [SerializeField] private TextMeshProUGUI _weatherTextMeshPro;

        public void Init(Texture2D texture, string text)
        {
            _weatherImage.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            _weatherTextMeshPro.text = "Сегодня - " + text + "F";
        }

        public override void Hide()
        {
            _weatherImage.gameObject.SetActive(false);
            _weatherTextMeshPro.gameObject.SetActive(false);
            ChangeView.interactable = true;
        }

        public override void Show()
        {
            _weatherImage.gameObject.SetActive(true);
            _weatherTextMeshPro.gameObject.SetActive(true);
            ChangeView.interactable = false;
        }
    }
}
