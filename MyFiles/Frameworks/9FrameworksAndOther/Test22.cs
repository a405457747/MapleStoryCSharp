using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using System;
using System.Linq;
using UniRx;
using UnityEngine.UI;

namespace QFrameworkStudy
{
    public class Test22 : MonoBehaviour
    {
        public Button _add;
        public Button _rud;
        public Text HpText;
        [Inject] private EnemyMode _enemyMode;

        void Start()
        {
            //QC.Instance.QFrameworkContainer.Inject(this);
            _rud.OnClickAsObservable()
                .Subscribe(_ => { _enemyMode.CurrentHp.Value -= 33; });
            _add.OnClickAsObservable()
                .Subscribe(_ => { _enemyMode.CurrentHp.Value += 33; });

            _enemyMode.CurrentHp.SubscribeToText(HpText);

            _enemyMode.IsDead.Where(item => item).Select(item => !item).SubscribeToInteractable(_rud);
        }
    }

    public class EnemyMode
    {
        public ReactiveProperty<long> CurrentHp;
        public IReadOnlyReactiveProperty<bool> IsDead;

        public EnemyMode(long hp)
        {
            CurrentHp = new ReactiveProperty<long>(hp);
            IsDead = CurrentHp.Select(item => item <= 0).ToReactiveProperty();
        }
    }
}