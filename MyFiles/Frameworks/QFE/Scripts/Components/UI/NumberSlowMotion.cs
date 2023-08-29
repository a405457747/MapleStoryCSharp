using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CallPalCatGames.QFrameworkExtension
{
    public class NumberSlowMotion : MonoBehaviour
    {
        private int _attributeValue;
        private Sequence _scoreSequence;
        private Text _slowMotionText;
        [FormerlySerializedAs("SlowMotionTime")] public float slowMotionTime = 0.13f;

        public int AttributeValue
        {
            get => _attributeValue;
            set
            {
                if (_attributeValue != value)
                {
                    StartSlowMotion(_attributeValue, value);
                    _attributeValue = value;
                }
            }
        }

        private void Start()
        {
            _scoreSequence = DOTween.Sequence();
            _scoreSequence.SetAutoKill(false);
            _slowMotionText = GetComponent<Text>();
        }

        private void StartSlowMotion(int oldValue, int newValue)
        {
            _scoreSequence.Append(DOTween.To(delegate(float value) { _slowMotionText.text = $"{Mathf.Floor(value)}"; },
                oldValue, newValue, slowMotionTime));
        }
    }
}