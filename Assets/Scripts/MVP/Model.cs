using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Cifkor.Karpusha
{
    public class Model : MonoBehaviour
    {
        protected WebRequestService WebRequestService;

        [Inject]
        public void Construct(WebRequestService webRequestService)
        {
            WebRequestService = webRequestService;
        }
    }
}
