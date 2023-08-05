using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UniRx;
using System;

namespace QFrameworkStudy
{
    public class CounterAppQFModel
    {
        public ReactiveProperty<int> Count { get; private set; }
        public ReactiveProperty<string> SomeData = new ReactiveProperty<string>("");

        [Inject]
        public IStorageService StorageService { get; set; }

        public void Init()
        {
            Count = StorageService.CreateIntReactiveProperty("count", 0);
        }
    }

    public interface IStorageService
    {
        ReactiveProperty<int> CreateIntReactiveProperty(string key, int defaultValue = 0);
    }

    public class CounterAppStorageService : IStorageService
    {
        public ReactiveProperty<int> CreateIntReactiveProperty(string key, int defaultValue = 0)
        {
            var initValue = PlayerPrefs.GetInt(key, defaultValue);
            var property = new ReactiveProperty<int>(initValue);
            property.Subscribe(value =>
            {
                PlayerPrefs.SetInt(key, value);
            });
            return property;
        }
    }

    public interface ICounterApiService
    {
        void RequestSomeData(Action<string> onResponse);
    }

    public class CounterApiService:ICounterApiService
    {
        public void RequestSomeData(Action<string> onResponse)
        {
            Observable.Timer(TimeSpan.FromSeconds(4.0f)).Subscribe(_ =>
            {
                onResponse("数据请求成功");
            });
        }
    }

    public class CounterApp : MonoBehaviour, ISingleton
    {
        QFrameworkContainer mContainer;

        public static QFrameworkContainer Container
        {
            get
            {
                return mApp.mContainer;
            }
        }

        private static CounterApp mApp
        {
            get
            {
                return MonoSingletonProperty<CounterApp>.Instance;
            }
        }

        public void OnSingletonInit()
        {
            mContainer = new QFrameworkContainer();
            mContainer.Register<ICounterApiService, CounterApiService>();
            mContainer.Register<IStorageService, CounterAppStorageService>();
            var model = new CounterAppQFModel();
            mContainer.RegisterInstance(model, true);
            model.Init();
        }
    }
}