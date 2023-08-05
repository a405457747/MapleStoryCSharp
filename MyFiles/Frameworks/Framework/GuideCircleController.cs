using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     圆形遮罩镂空引导
/// </summary>
[RequireComponent(typeof(GuideEventPenetrate))]
public class GuideCircleController : MonoBehaviour
{
    /// <summary>
    ///    镂空区域中心
    /// </summary>
    private Vector4 _center;

    /// <summary>
    ///     区域范围缓存
    /// </summary>
    private Vector3[] _corners;

    /// <summary>
    ///     要高亮显示的目标
    /// </summary>
    private RectTransform _target;

    /// <summary>
    ///     遮罩材质
    /// </summary>
    private Material _material;

    /// <summary>
    /// 渗透组件
    /// </summary>
    private GuideEventPenetrate _eventPenetrate;

    /// <summary>
    ///    动画收缩时间
    /// </summary>
    private float _shrinkTime;


    private Canvas _canvas;
    private RectTransform _canvasRectTransform;


    private static readonly int Slider = Shader.PropertyToID("_Slider");
    private static readonly int Center = Shader.PropertyToID("_Center");

    /// <summary>
    ///     当前高亮区域的半径
    /// </summary>
    private float _currentRadius;

    /// <summary>
    ///     镂空区域半径
    /// </summary>
    private float _radius;

    // 收缩速度
    private float _shrinkVelocity;

    private void Start()
    {
        _corners = new Vector3[4];
        _shrinkTime = 0.1f;
        _eventPenetrate = GetComponent<GuideEventPenetrate>();
        _material = GetComponent<Image>().material;
        _canvas = PanelManager.Instance.CanvasTrans.GetComponent<Canvas>();
        _canvasRectTransform = _canvas.transform as RectTransform;
    }

    private void Update()
    {
        //从当前半径到目标半径差值显示收缩动画
        var value = Mathf.SmoothDamp(_currentRadius, _radius, ref _shrinkVelocity, _shrinkTime);
        if (!Mathf.Approximately(value, _currentRadius))
        {
            _currentRadius = value;
            _material.SetFloat(Slider, _currentRadius);
        }
    }

    /// <summary>
    ///     世界坐标向画布坐标转换
    /// </summary>
    /// <param name="canvas">画布</param>
    /// <param name="world">世界坐标</param>
    /// <returns>返回画布上的二维坐标</returns>
    private Vector2 WorldToCanvasPos(Canvas canvas, Vector3 world)
    {
        return StructHelper.WorldToCanvasPos(canvas, world);
    }

    public void ChangeTarget(RectTransform target)
    {
        _target = target;
        _eventPenetrate.SetTargetImage(_target);
        //获取高亮区域的四个顶点的世界坐标
        _target.GetWorldCorners(_corners);
        //计算最终高亮显示区域的半径
        _radius = Vector2.Distance(WorldToCanvasPos(_canvas, _corners[0]), WorldToCanvasPos(_canvas, _corners[2])) / 2f;
        //计算高亮显示区域的圆心
        var x = _corners[0].x + (_corners[3].x - _corners[0].x) / 2f;
        var y = _corners[0].y + (_corners[1].y - _corners[0].y) / 2f;
        var centerWorld = new Vector3(x, y, 0);
        var center = WorldToCanvasPos(_canvas, centerWorld);
        //设置遮罩材料中的圆心变量
        var centerMat = new Vector4(center.x, center.y, 0, 0);
        _material.SetVector(Center, centerMat);
        //计算当前高亮显示区域的半径
        if (_canvasRectTransform != null)
        {
            //获取画布区域的四个顶点
            _canvasRectTransform.GetWorldCorners(_corners);
            //将画布顶点距离高亮区域中心最远的距离作为当前高亮区域半径的初始值
            foreach (var corner in _corners)
                _currentRadius = Mathf.Max(Vector3.Distance(WorldToCanvasPos(_canvas, corner), center), _currentRadius);
        }

        _material.SetFloat(Slider, _currentRadius);
    }
}