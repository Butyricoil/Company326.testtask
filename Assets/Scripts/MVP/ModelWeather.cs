using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Zenject;

namespace Cifkor.Karpusha
{
    public class ModelWeather : Model
    {
        private string _realURL = "https://api.weather.gov/gridpoints/TOP/32,81/forecast";

        // Если не получилось десериализовать JSON, скачиваем картинку и подставляем заранее посмотренные данные
        private string _urlImage = "https://api.weather.gov/icons/land/night/few?size=medium";

        private Period _periods;
        private Coroutine _coroutine;
        private string _responseJSON;

        public void GetWeather(Action<string> response, Action callback)
        {
            //response(WebRequestService.RequestJSON(_realURL, null, response, callback));

            //string[] lines = request.downloadHandler.text.Split(new string[] { "periods" }, StringSplitOptions.None);
            //string line = lines.LastOrDefault().Substring(lines.LastOrDefault().IndexOf('['), lines.LastOrDefault().IndexOf(']') - lines.LastOrDefault().IndexOf('[') + 1);

            var jsonText = JObject.Parse(_responseJSON);
            var rrr = JsonConvert.DeserializeObject<Period>(_responseJSON);
            var data = JsonUtility.FromJson<Period>(_responseJSON);


            IList<JToken> results = jsonText["period"].Children().ToList();
        }


        /// Так как не получилось десеарилизовать строку JSON, чтобы завершить задачу, сделал фейковый ответ JsonConvert / JsonUtility

        private NewPeriod _newPeriod;
        private Texture2D _weatherTexture;
        public void FakeAnswert()
        {
            _newPeriod = null;
            StartCoroutine(LoadTextureFromServer("https://api.weather.gov/icons/land/night/few?size=medium"));
        }

        public NewPeriod GetNewPeriod()
        {
            return _newPeriod;
        }

        IEnumerator LoadTextureFromServer(string url)
        {
            var request = UnityWebRequestTexture.GetTexture(url);

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.ProtocolError && request.result != UnityWebRequest.Result.ConnectionError)
            {
                _weatherTexture = DownloadHandlerTexture.GetContent(request);
                _newPeriod = new NewPeriod(_weatherTexture, 60);
            }
            else
            {
                Debug.LogErrorFormat("error request [{0}, {1}]", url, request.error);
            }

            request.Dispose();
        }
    }
}
