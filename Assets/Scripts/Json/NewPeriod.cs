using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cifkor.Karpusha
{
    public class NewPeriod
    {
        public Texture2D WeatherTexture;
        public int Temperature;

        public NewPeriod(Texture2D weatherTexture, int temp)
        {
            WeatherTexture = weatherTexture;
            Temperature = temp;
        }
    }
}
