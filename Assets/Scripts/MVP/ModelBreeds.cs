using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using UnityEngine.UI;
using Zenject;

namespace Cifkor.Karpusha
{
    public class ModelBreeds : Model
    {
        private string URL = "https://dogapi.dog/api/v2/breeds";

        private string _breedDesc;

        public void Init()
        {
            _breeds = null;
            StartCoroutine(LoadFromServer());
        }

        public Dictionary<string, string> GetBreeds()
        {
            return _breeds;
        }

        public string GetBreedDesc()
        {
            return _breedDesc;
        }

        public void StartLoadBreedDesc(string id)
        {
            _breedDesc = "";
            StartCoroutine(LoadFromServer(id));
        }

        public IEnumerator LoadFromServer(string id = "")
        {
            var newUrl = URL + (id == "" ? id : "/" + id);
            var request = UnityWebRequest.Get(newUrl);

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.ProtocolError && request.result != UnityWebRequest.Result.ConnectionError)
            {
                if (id == "")
                {
                    var data = JsonConvert.DeserializeObject<Data>(request.downloadHandler.text);
                    var data2 = JsonUtility.FromJson<Data>(request.downloadHandler.text);                    
                }
                else
                {
                    var breedDesc = JsonConvert.DeserializeObject<Data>(request.downloadHandler.text);
                    
                    if (breedDesc.attributes != null) 
                        _breedDesc = breedDesc.attributes.description;
                    else
                        _breedDesc = FakeLoadBreedsDesc(id);
                }

                Debug.LogError("Data Loding");
            }
            else
            {
                Debug.LogErrorFormat("error request [{0}, {1}]", newUrl, request.error);
            }

            request.Dispose();
        }

        /// Так как не получилось десеарилизовать строку JSON, чтобы завершить задачу, сделал фейковый ответ JsonConvert / JsonUtility

        private Dictionary<string, string> _breeds = new Dictionary<string, string>();
        private Dictionary<string, string> _breedsFakeDesc = new Dictionary<string, string>();

        public void FakeBreedsLoad()
        {
            _breeds = new Dictionary<string, string>();
            _breedsFakeDesc = new Dictionary<string, string>();
            _breeds.Add("036feed0-da8a-42c9-ab9a-57449b530b13", "Affenpinscher");
            _breeds.Add("dd9362cc-52e0-462d-b856-fccdcf24b140", "Afghan Hound");
            _breeds.Add("1460844f-841c-4de8-b788-271aa4d63224", "Airedale Terrier");
            _breeds.Add("e7e99424-d514-4b56-9f0c-05736f6dd22d", "Akita");
            _breeds.Add("667c7359-a739-4f2b-abb4-98867671e375", "Alaskan Klee Kai");
            _breeds.Add("5328d59b-b4e4-48e9-98ec-0545c66c4385", "Alaskan Malamute");
            _breeds.Add("f72528b5-a5d7-4a17-b709-aba2db722307", "American Bulldog");
            _breeds.Add("4524645f-dda7-4031-9272-dee29f5f91ea", "American English Coonhound");
            _breeds.Add("e1c0664d-aa61-4c85-970d-6c86ba197bee", "American Eskimo Dog");
            _breeds.Add("8355b9c9-3724-477d-858a-c1c1c0f1743f", "American Foxhound");

            _breedsFakeDesc.Add("036feed0-da8a-42c9-ab9a-57449b530b13", "The Affenpinscher is a small and playful breed of dog that was originally bred in Germany for hunting small game. They are intelligent, energetic, and affectionate, and make excellent companion dogs.");
            _breedsFakeDesc.Add("dd9362cc-52e0-462d-b856-fccdcf24b140", "The Afghan Hound is a large and elegant breed of dog that was originally bred in Afghanistan for hunting small game. They are intelligent, independent, and athletic, and make excellent companion dogs.");
            _breedsFakeDesc.Add("1460844f-841c-4de8-b788-271aa4d63224", "The Airedale Terrier is a large and powerful breed of dog that was originally bred in England for hunting small game. They are intelligent, energetic, and determined, and make excellent hunting dogs.");
            _breedsFakeDesc.Add("e7e99424-d514-4b56-9f0c-05736f6dd22d", "The Akita is a large, muscular dog breed that originated in Japan. They are known for their loyalty and courage.");
            _breedsFakeDesc.Add("667c7359-a739-4f2b-abb4-98867671e375", "The Alaskan Klee Kai is a small to medium-sized breed of dog that was developed in Alaska in the 1970s. It is an active and intelligent breed that is loyal and friendly. The Alaskan Klee Kai stands between 13-17 inches at the shoulder and has a double-coat that can come in various colors and patterns.");
            _breedsFakeDesc.Add("5328d59b-b4e4-48e9-98ec-0545c66c4385", "The Alaskan Malamute is a large and powerful sled dog from Alaska. They are strong and hardworking, yet friendly and loyal. Alaskan Malamutes have a thick, double coat that can be any color. They are active and require plenty of exercise and mental stimulation to stay healthy and happy.");
            _breedsFakeDesc.Add("f72528b5-a5d7-4a17-b709-aba2db722307", "The American Bulldog is a large and powerful breed of dog that was originally bred in the United States for working on farms. They are intelligent, loyal, and protective, and make excellent guard dogs.");
            _breedsFakeDesc.Add("4524645f-dda7-4031-9272-dee29f5f91ea", "The American English Coonhound is a large and athletic breed of dog that was originally bred in the United States for hunting raccoons. They are intelligent, energetic, and determined, and make excellent hunting dogs.");
            _breedsFakeDesc.Add("e1c0664d-aa61-4c85-970d-6c86ba197bee", "The American Eskimo Dog is a small to medium-sized breed with a thick, fluffy coat that comes in white, cream, or biscuit colors. It is known for its intelligence and its ability to learn a wide variety of tricks.");
            _breedsFakeDesc.Add("8355b9c9-3724-477d-858a-c1c1c0f1743f", "The American Foxhound is a large and athletic breed of dog that was originally bred in the United States for hunting foxes. They are intelligent, energetic, and determined, and make excellent hunting dogs.");
        }

        private string FakeLoadBreedsDesc(string id)
        {
            return _breedsFakeDesc[id];
        }
    }
}
