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
    /// <summary>
    /// Service for handling web requests with queue management and retry mechanism.
    /// Implements sequential request execution with priority support.
    /// </summary>
    public class WebRequestService
    {
        private List<UnityWebRequest> _currentRequests = new List<UnityWebRequest>();
        private TaskService _taskService = new TaskService();
        private const int MAX_RETRY_ATTEMPTS = 3;
        private const float RETRY_DELAY = 1f;

        /// <summary>
        /// Sends a JSON request to the specified URL with progress tracking and response handling.
        /// </summary>
        /// <param name="url">The URL to send the request to</param>
        /// <param name="progress">Callback for tracking download progress</param>
        /// <param name="response">Callback for handling the response</param>
        /// <param name="callback">Callback to be executed after request completion</param>
        /// <param name="isHighPriority">Whether this request should be prioritized in the queue</param>
        public void RequestJSON(string url, Action<float> progress, Action<string> response, Action callback, bool isHighPriority = false)
        {
            _taskService.AddTask(WebRequestData(url, progress, (unityWebRequest) =>
            {
                if (unityWebRequest.result != UnityWebRequest.Result.ProtocolError && unityWebRequest.result != UnityWebRequest.Result.ConnectionError)
                {
                    response(unityWebRequest.downloadHandler.text);
                }
                else
                {
                    Debug.LogWarning($"Error Data WebRequest: {unityWebRequest.error}");
                    response(null);
                }
            }), callback, isHighPriority);
        }

        /// <summary>
        /// Sends an image request to the specified URL with progress tracking and response handling.
        /// </summary>
        /// <param name="url">The URL to send the request to</param>
        /// <param name="progress">Callback for tracking download progress</param>
        /// <param name="response">Callback for handling the response</param>
        /// <param name="callback">Callback to be executed after request completion</param>
        /// <param name="isHighPriority">Whether this request should be prioritized in the queue</param>
        public void RequestImage(string url, Action<float> progress, Action<Texture2D> response, Action callback, bool isHighPriority = false)
        {
            _taskService.AddTask(WebRequestTexture(url, progress, (unityWebRequest) =>
            {
                if (unityWebRequest.result != UnityWebRequest.Result.ProtocolError && unityWebRequest.result != UnityWebRequest.Result.ConnectionError)
                {
                    response(DownloadHandlerTexture.GetContent(unityWebRequest));
                }
                else
                {
                    Debug.LogWarning($"Error Texture2D WebRequest: {unityWebRequest.error}");
                    response(null);
                }
            }), callback, isHighPriority);
        }

        /// <summary>
        /// Cancels all pending and current requests.
        /// </summary>
        public void CancelAllRequests()
        {
            _taskService.Clear();
            foreach (UnityWebRequest request in _currentRequests)
            {
                if (request != null)
                {
                    request.Abort();
                    request.Dispose();
                }
            }
            _currentRequests.Clear();
        }

        /// <summary>
        /// Cancels all requests of a specific type.
        /// </summary>
        /// <param name="type">The type of requests to cancel</param>
        public void CancelRequestsByType(RequestType type)
        {
            _taskService.CancelTasksByType(type);
            var requestsToCancel = _currentRequests.Where(r => r != null).ToList();
            foreach (var request in requestsToCancel)
            {
                request.Abort();
                request.Dispose();
                _currentRequests.Remove(request);
            }
        }

        private IEnumerator WebRequestData(string url, Action<float> progress, Action<UnityWebRequest> response)
        {
            int retryCount = 0;
            while (retryCount < MAX_RETRY_ATTEMPTS)
            {
                var request = UnityWebRequest.Get(url);
                yield return WebRequest(request, progress, response);
                
                if (request.result == UnityWebRequest.Result.Success)
                {
                    break;
                }
                
                retryCount++;
                if (retryCount < MAX_RETRY_ATTEMPTS)
                {
                    yield return new WaitForSeconds(RETRY_DELAY);
                }
            }
        }

        private IEnumerator WebRequestTexture(string url, Action<float> progress, Action<UnityWebRequest> response)
        {
            int retryCount = 0;
            while (retryCount < MAX_RETRY_ATTEMPTS)
            {
                var request = UnityWebRequestTexture.GetTexture(url);
                yield return WebRequest(request, progress, response);
                
                if (request.result == UnityWebRequest.Result.Success)
                {
                    break;
                }
                
                retryCount++;
                if (retryCount < MAX_RETRY_ATTEMPTS)
                {
                    yield return new WaitForSeconds(RETRY_DELAY);
                }
            }
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
            {
                yield return request.SendWebRequest();
            }

            response(request);

            if (_currentRequests.Contains(request))
            {
                _currentRequests.Remove(request);
            }

            request.Dispose();
        }
    }

    /// <summary>
    /// Represents the type of web request being made.
    /// </summary>
    public enum RequestType
    {
        /// <summary>
        /// Weather information request
        /// </summary>
        Weather,
        
        /// <summary>
        /// Dog breeds list request
        /// </summary>
        DogBreeds,
        
        /// <summary>
        /// Specific dog breed details request
        /// </summary>
        DogBreedDetails
    }
}
