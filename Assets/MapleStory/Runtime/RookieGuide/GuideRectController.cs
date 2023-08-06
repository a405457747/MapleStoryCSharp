using UnityEngine;
using UnityEngine.UI;

namespace MapleStory
{
    /// <summary>
    ///     矩形引导组件
    /// </summary>
    [RequireComponent(typeof(GuideEventPenetrate))]
    public class GuideRectController : MonoBehaviour
    {
        private RectTransform _canvasRectTransform;

        /// <summary>
        ///     镂空区域中心
        /// </summary>
        private Vector4 _center;

        /// <summary>
        ///     区域范围缓存
        /// </summary>
        private Vector3[] _corners;

        /// <summary>
        ///     当前的偏移值X
        /// </summary>
        private float _currentOffsetX;

        /// <summary>
        ///     当前的偏移值Y
        /// </summary>
        private float _currentOffsetY;

        /// <summary>
        ///     渗透组件
        /// </summary>
        private GuideEventPenetrate _eventPenetrate;

        /// <summary>
        ///     遮罩材质
        /// </summary>
        private Material _material;

        /// <summary>
        ///     动画收缩时间
        /// </summary>
        private float _shrinkTime;


        private float _shrinkVelocityX;
        private float _shrinkVelocityY;

        /// <summary>
        ///     高亮显示的目标
        /// </summary>
        private RectTransform _target;

        /// <summary>
        ///     最终的偏移值X
        /// </summary>
        private float _targetOffsetX;

        /// <summary>
        ///     最终的偏移值Y
        /// </summary>
        private float _targetOffsetY;


        private Canvas canvas;

        private void Start()
        {
            _corners = new Vector3[4];
            _shrinkTime = 0.1f;
            _eventPenetrate = GetComponent<GuideEventPenetrate>();
            _material = GetComponent<Image>().material;
            canvas = _eventPenetrate.GetCanvas();
            _canvasRectTransform = canvas.transform as RectTransform;
        }

        private void FixedUpdate()
        {
            //从当前偏移值到目标偏移值差值显示收缩动画
            var valueX = Mathf.SmoothDamp(_currentOffsetX, _targetOffsetX, ref _shrinkVelocityX, _shrinkTime);
            var valueY = Mathf.SmoothDamp(_currentOffsetY, _targetOffsetY, ref _shrinkVelocityY, _shrinkTime);
            if (!Mathf.Approximately(valueX, _currentOffsetX))
            {
                _currentOffsetX = valueX;
                _material.SetFloat("_SliderX", _currentOffsetX);
            }

            if (!Mathf.Approximately(valueY, _currentOffsetY))
            {
                _currentOffsetY = valueY;
                _material.SetFloat("_SliderY", _currentOffsetY);
            }
        }

        /// <summary>
        ///     世界坐标到画布坐标的转换
        /// </summary>
        /// <param name="canvas">画布</param>
        /// <param name="world">世界坐标</param>
        /// <returns>转换后在画布的坐标</returns>
        private Vector2 WorldToCanvasPos(Canvas canvas, Vector3 world)
        {
            return GuideEventPenetrate.WorldToCanvasPos(canvas, world);
        }

        public void ChangeTarget(RectTransform target)
        {
            _target = target;
            _eventPenetrate.SetTargetImage(_target);
            //获取高亮区域四个顶点的世界坐标
            _target.GetWorldCorners(_corners);
            //计算高亮显示区域咋画布中的范围
            _targetOffsetX =
                Vector2.Distance(WorldToCanvasPos(canvas, _corners[0]), WorldToCanvasPos(canvas, _corners[3])) / 2f;
            _targetOffsetY =
                Vector2.Distance(WorldToCanvasPos(canvas, _corners[0]), WorldToCanvasPos(canvas, _corners[1])) / 2f;
            //计算高亮显示区域的中心
            var x = _corners[0].x + (_corners[3].x - _corners[0].x) / 2f;
            var y = _corners[0].y + (_corners[1].y - _corners[0].y) / 2f;
            var centerWorld = new Vector3(x, y, 0);
            var center = WorldToCanvasPos(canvas, centerWorld);
            //设置遮罩材料中中心变量
            var centerMat = new Vector4(center.x, center.y, 0, 0);
            _material = GetComponent<Image>().material;
            _material.SetVector("_Center", centerMat);
            //计算当前偏移的初始值
            var canvasRectTransform = canvas.transform as RectTransform;
            if (canvasRectTransform != null)
            {
                //获取画布区域的四个顶点
                canvasRectTransform.GetWorldCorners(_corners);
                //求偏移初始值
                for (var i = 0; i < _corners.Length; i++)
                    if (i % 2 == 0)
                        _currentOffsetX = Mathf.Max(Vector3.Distance(WorldToCanvasPos(canvas, _corners[i]), center),
                            _currentOffsetX);
                    else
                        _currentOffsetY = Mathf.Max(Vector3.Distance(WorldToCanvasPos(canvas, _corners[i]), center),
                            _currentOffsetY);
            }

            //设置遮罩材质中当前偏移的变量
            _material.SetFloat("_SliderX", _currentOffsetX);
            _material.SetFloat("_SliderY", _currentOffsetY);
        }
    }
}