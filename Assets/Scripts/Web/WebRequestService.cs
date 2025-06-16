using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Cifkor.Karpusha
{
    public class WebRequestService
    {
        private List<UnityWebRequest> _currentRequests = new List<UnityWebRequest>();

        private TaskService _taskService = new TaskService();

        public void RequestJSON(string url, Action<float> progress, Action<string> response, Action callback)
        {
            _taskService.AddTask(WebRequestData(url, progress, (unityWebRequest) =>
            {
                if (unityWebRequest.result != UnityWebRequest.Result.ProtocolError && unityWebRequest.result != UnityWebRequest.Result.ConnectionError)
                {
                    response(unityWebRequest.downloadHandler.text);
                }
                else
                {
                    Debug.LogWarning("Error Data WebRequest: " + unityWebRequest.error);
                    response(null);
                }
            }), callback);
        }

        public void RequestImage(string url, Action<float> progress, Action<Texture2D> response, Action callback)
        {
            _taskService.AddTask(WebRequestTexture(url, progress, (unityWebRequest) =>
            {
                if (unityWebRequest.result != UnityWebRequest.Result.ProtocolError && unityWebRequest.result != UnityWebRequest.Result.ConnectionError)
                {
                    response(DownloadHandlerTexture.GetContent(unityWebRequest));
                }
                else
                {
                    Debug.LogWarning("Error Texture2D WebRequest: " + unityWebRequest.error);
                    response(null);
                }
            }), callback);
        }

        public void Clear()
        {
            _taskService.Clear();

            foreach (UnityWebRequest request in _currentRequests)
            {
                request.Abort();
                request.Dispose();
            }

            _currentRequests.Clear();
        }

        private IEnumerator WebRequestData(string url, Action<float> progress, Action<UnityWebRequest> response)
        {
            var request = UnityWebRequest.Get(url);
            return WebRequest(request, progress, response);
        }

        private IEnumerator WebRequestTexture(string url, Action<float> progress, Action<UnityWebRequest> response)
        {
            var request = UnityWebRequestTexture.GetTexture(url);
            return WebRequest(request, progress, response);
        }

        private IEnumerator WebRequest(UnityWebRequest request, Action<float> progress, Action<UnityWebRequest> response)
        {
            while(!Caching.ready)
            {
                yield return null;
            }

            if (progress != null)
            {
                request.SendWebRequest();
                _currentRequests.Add(request);

                while (!request.isDone)
                {
                    progress(request.downloadProgress);

                    yield return null;
                }

                progress(1.0f);
            }
            else
                yield return request.SendWebRequest();

            response(request);

            if (_currentRequests.Contains(request))
                _currentRequests.Remove(request);

            request.Dispose();
        }
    }
}
