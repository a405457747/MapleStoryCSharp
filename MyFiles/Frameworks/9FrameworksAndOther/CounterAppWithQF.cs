using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UniRx;
using System;
using UnityEngine.UI;

namespace QFrameworkStudy
{
    public class CounterAppWithQF : ViewController
    {
        public Text Number;
        public Text ResultText;
        public Button BtnAdd;
        public Button BtnSub;
        public Button BtnRequest;

        [Inject]
        public CounterAppQFModel Model { get; set; }
        [Inject]
        public ICounterApiService ApiService { get; set; }

        private void Start()
        {
            CounterApp.Container.Inject(this);

            Model.Count.Select(count => count.ToString())
                .SubscribeToText(Number);

            Model.SomeData.SubscribeToText(ResultText).AddTo(this);

            BtnAdd.OnClickAsObservable().Subscribe(_ =>
            {
                Model.Count.Value++;
            });

            BtnSub.OnClickAsObservable().Subscribe(_ =>
            {
                Model.Count.Value--;
            });

            BtnRequest.OnClickAsObservable().Subscribe(_ =>
            {
                ApiService.RequestSomeData(someData =>
                {
                    Model.SomeData.Value = someData;
                });
            });
        }
    }
}