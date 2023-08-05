using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class NumberSlowMotion : MonoBehaviour
{
    public float slowMotionTime = 0.13f;

    private int _attributeValue;

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

    private Text _slowMotionText;

    private void Start()
    {
        _slowMotionText = GetComponent<Text>();
    }

    private void StartSlowMotion(int oldValue, int newValue)
    {
        DOTween.To(delegate (float value) { _slowMotionText.text = $"{Mathf.Floor(value)}"; },
            oldValue, newValue, slowMotionTime);
    }
}
