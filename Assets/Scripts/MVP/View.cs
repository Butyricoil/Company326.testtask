using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Cifkor.Karpusha
{
    public abstract class View : MonoBehaviour
    {
        [SerializeField] protected Button ChangeView;
      
        protected SignalBus SignalBus;

        [Inject]
        protected void Construct(SignalBus signalBus)
        {
            SignalBus = signalBus;
        }
        private void Start()
        {
            ChangeView.onClick.AddListener(() => ChangeViewClick());
        }

        public void ChangeViewClick()
        {
            Show();
            SignalBus.Fire(new ChangeWindowSignal(this));
        }

        public abstract void Show();

        public abstract void Hide();
    }
}
