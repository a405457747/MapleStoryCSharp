using System;
using UnityEngine;

namespace CallPalCatGames.QFrameworkExtension
{
    public class Timer : MonoBehaviour
    {
        private float _cd;
        private bool _needWait;
        private float _timeCycleScale;
        private float _timerCd;
        private Action _timingAction;

        public void Constructor(float cd, Action timingAction, bool needWait)
        {
            _cd = cd;
            _timingAction = timingAction;
            _needWait = needWait;
            _timeCycleScale = 0f;
            if (_needWait)
                _timerCd = 0f;
            else
                _timerCd = _cd;
        }

        public void UpdateFunc()
        {
            if (_needWait)
            {
                _timerCd += Time.deltaTime;
                if (_timerCd > _cd)
                {
                    TimingActionExe();
                    _timerCd = 0f;
                }

                _timeCycleScale = 1 - (_cd - _timerCd) / _cd;
            }
            else
            {
                if (_timerCd == _cd) TimingActionExe();
                _timerCd -= Time.deltaTime;
                if (_timerCd < 0) _timerCd = _cd;
                _timeCycleScale = (_cd - _timerCd) / _cd;
            }
        }

        private void TimingActionExe()
        {
            _timingAction?.Invoke();
        }
    }
}