using UnityEngine;

public interface ICustomMessageTarget{
    
}

public class API : MonoBehaviour, ICustomMessageTarget
{
    public Transform Capsule;

    public GameObject cube1;
    public Transform CylinderTarget;
    public Transform Gou;
    private Rigidbody rb;
    public Transform T;

    //Mathf类
    //Vector2
    //Vector3;
    //Quaternion类
    //Rigidbody
    //Physiics;
    //Event,EventSystem,;
    //常用On
    //点乘和叉乘，
    //相机过度，2D朝向，3D朝向
    //Matrix4x4
    //Transform
    //RectTransform;
    //移动旋转（moveposition）
    //Animator，Animation
    //2D的效应器，3D关节，约束

    public void Message1()
    {
        print("m1");
    }

    public void Message2()
    {
        print("m2");
    }

    private void j()
    {
    }

    private void Start()
    {
        #region Mathf(空闲的4个函数暂时没搞懂）

        //print(Mathf.Deg2Rad);
        //print(Mathf.Rad2Deg);
        //print(2 * Mathf.PI / 360);//角速度是矢量右手法则单位弧度/秒，线速度v=w*r;
        //print(Mathf.Sin(90 * Mathf.Deg2Rad));//输入参数是弧度制的
        //print(Mathf.Asin(1) * Mathf.Rad2Deg);//根据值求角度
        //print(Mathf.Atan2(1f, 1f) * Mathf.Rad2Deg);//其中 x 是临边边长，而 y 是对边边长。和Mathf.Atan一样只是参数不一样罢了
        //var b = Mathf.ClosestPowerOfTwo(9);
        //print(Mathf.DeltaAngle(360,45));//计算以度为单位的两个给定角度之间的最短差。意义最短差
        //print(Mathf.Exp(1));
        //print(Mathf.GammaToLinearSpace(2f));//将给定值从gamma（sRGB）转换为线性色彩空间。
        //print(Mathf.IsPowerOfTwo(9));//返回是不是二次冥
        //print(Mathf.Log(8,2));//默认是e所以要指明第一个真数。
        //print(Mathf.PingPong(Time.time, 1));对第一变化值pingpong操作0-1 1-0这样的
        //print(Mathf.Repeat(Time.time, 1));//和取模运算一样
        //Mathf.Lerp();
        //cube1.transform.position = new Vector3(Mathf.Lerp(0, 10, Time.fixedTime), 0, 0);//实锤了这样做是匀速运动
        //cube1.transform.position = new Vector3(Mathf.Lerp(cube1.transform.position.x, 10, Time.fixedTime), 0, 0);//插值（相差再乘以最后的比例）搞懂了虽然不知道是什么曲线，但是后面慢得比较明显
        //Mathf.LerpAngle();与Lerp相同，但要确保值在360度范围内正确插值。
        //float angle = Mathf.LerpAngle(0, 180, Time.time);//400会变成40,360不会动 0和360分不清的关系
        //cube1.transform.eulerAngles = new Vector3(0, angle, 0);//为y轴正就是顺时针旋转
        //Mathf.MoveTowardsAngle();与MoveTowards相同，但是要确保值在360度范围内正确插值//变量current和target假定为度。出于优化原因，maxDelta不支持的负值，并可能导致振荡。要推current离目标角度，请在该角度增加180度。
        //Mathf.MoveTowards();这与Mathf.Lerp本质上相同，但是该函数将确保速度不会超过maxDelta。负值maxDelta会将值推离target。就是速度固定了
        //float x = Mathf.MoveTowards(0, 10000, Time.time);
        //print(x +":"+Time.time);
        //cube1.transform.position = new Vector3(x, 0, 0);
        //print(Mathf.NextPowerOfTwo(7));//返回等于或大于参数的二的下一个幂。
        //print(Mathf.PerlinNoise(1, 1));//生成2D Perlin噪声。
        //Mathf.SmoothStep();
        //f1 = Mathf.SmoothStep(0, 10, Time.time);
        //f2 = f1 - f2;
        //cube1.transform.position = new Vector3(f2, 0, 0);//s曲线越来越快,还有f2=f1-f2,就是一个先快后满的拱形。

        //Mathf.InverseLerp();
        //Mathf.LerpUnclamped();
        //Mathf.SmoothDamp();
        //Mathf.SmoothDampAngle();

        #endregion

        #region Vector2

        //print(Vector2.one);
        //print(Vector2.negativeInfinity);
        //Vector2 temp = new Vector2(2, 2);
        //print(temp.magnitude);//向量的长度,归一化就是让向量长度为1而已
        //print(temp.normalized);
        //print(temp.sqrMagnitude);//这个没意思 x平方加y平方
        //temp.Set(1, 3);
        //print(temp.x);
        //temp[0] = 8;
        //print(Vector2.Equals(new Vector2(1, 1), new Vector2(1, 1)));
        //print(temp.ToString());
        //print(Vector2.Angle( new Vector2(1, 1), new Vector2(0, 1)));//结果不能超过180；
        //print(Vector2.ClampMagnitude(new Vector2(50, 50), 2));
        //print(Vector2.Max(new Vector2(1, 3), new Vector2(2, 2)));//这个方法优点意思分别取各种最大分量
        //print(Vector2.Perpendicular(new Vector2(-1,1)));//返回它的垂直向量 相当于顺时针移到结果位置
        //向量加法和减法意义，谁减指向谁
        //print(Vector2.Reflect(new Vector2(-1, 1) * -1, new Vector2(0, 1) * -1));
        //print(Vector2.Dot(new Vector2(2, 1), new Vector2(2, 0)));
        //var a = new Vector2(2, 1).magnitude;
        //var b = new Vector2(2, 0).magnitude;
        //var f = b / a;
        //print(new Vector2(2, 1).magnitude * new Vector2(2, 0).magnitude * f);
        //Vector2.Dot
        //print(Vector2.Scale(new Vector2(1, 2), new Vector2(2, 3)));
        //print(Vector2.SignedAngle(Vector2.up, Vector2.right));//不能超过180而是a到b的顺时针方向
        //Vector2.SmoothDamp()

        #endregion

        #region Vector3(MoveTowards很神奇啊,ProjectOnPlane、搞不懂）

        //print(Vector3.ClampMagnitude(new Vector3(100, 100,100), 1f));
        //print(new Vector3(1, 1, 1).magnitude);
        //print(Mathf.Sqrt(3));
        //Vector3.Cross();
        //cube1.transform.position = Vector3.MoveTowards(Vector3.zero, new Vector3(30, 0, 0), Time.time);//先快后慢呢Time.time和Time.deltaTime都能用
        //zero与非zero可以和Time.time结合，而Tiime.delta至于非zero结合，而最后一个值是限制速度而已。
        //print(cube1.transform.position.x);
        //Vector3 one = new Vector3(3, 3, 0);
        //Vector3 two = new Vector3(0, 5, 0);
        //Vector3.OrthoNormalize(ref one, ref two);//都会使量归一化并时第二个向量改变用来正交。
        //print(one.magnitude);
        //print(two.magnitude);
        //print(one);
        //print(two);
        //print(Vector3.Project(new Vector3(2,2,0),Vector3.up));//参数二onNormal ： 坐标轴（X-Vector3.right，Y-Vector3.up，Z-Vector3.forward）。
        //Vector3.Reflect();
        //Vector3.Scale();
        //Vector3.Slerp();        //cube1.transform.position = Vector3.Slerp(Vector3.zero, Vector3.one, Time.time);

        //Vector3.ProjectOnPlane();
        //Vector3.RotateTowards();//该向量将旋转在弧线上，而不是线性插值。这个函数基本上和Vector3.Slerp相同，而是该函数将确保角速度和变换幅度不会超过maxRadiansDelta和maxMagnitudeDelta。maxRadiansDelta和maxMagnitudeDelta的负值从目标推开该向量
        //Vector3.SmoothDamp();//第一个参数只能是自己，第三个参数开始都是0，
        //Vector3 speed = Vector3.one * 9.01f;
        //private void FixedUpdate()//fixedUpdate会统一Tiem.deltaTime
        //{
        //    cube1.transform.position = Vector3.SmoothDamp(cube1.transform.position, Vector3.one * 10, ref speed, Time.deltaTime);//只能向我这样搞
        //}

        #endregion

        #region Quaternion(SetLookRotation,LookRotation, Quaternion.SlerpUnclamped不懂)

        //Quaternion q = Quaternion.identity;
        //q.eulerAngles = new Vector3(20, 30, 40);
        //cube1.transform.rotation = q;
        //q.SetFromToRotation();//此方法用于创建一个从fromDirection到toDirection的rotation
        //Quaternion.Normalize;//大小为1的四元数

        //Quaternion.identity.SetLookRotation()//下面4个注释
        //            （1）transform.forward方向与v1方向相同；

        //（2）transform.right垂直于由Vector3.zero、v1和v2三点构成的平面；

        //（3）v2的作用除了与Vector3.zero和v1构成平面来决定transform.right的方向外，还用来决定transform.up的朝向，因为当transform.forward和transform.right方向确定后，transform.up方向剩下两种可能，到底选用哪一种便由v2来影响，transform.up方向的选取方式总会使得transform.up的方向和v2的方向的夹角小于或等于90度。当然，一般情况下v2.normalized和transform.up是不相同的。

        //（4）当v1为Vector3.zero时方法失效，即不要在使用此方法时把v1设置成Vector3.zero。

        //提示：不可以直接使用transform.rotation.SetLookRotation(v1, v2)的方式来使用SetLookRotation方法，否则会不起作用。应该使用上述代码所示的方式，首先实例化一个Quaternion，然后对其使用SetLookRotation，最后将其赋给transform.rotation。
        //float angle = 0.0f;
        //Vector3 axis = Vector3.zero;
        //cube1.transform.rotation.ToAngleAxis(out angle, out axis);//旋转角度转换成轴表示
        //print(angle + ":" + axis.ToString());

        //print(Quaternion.Angle(cube1.transform.rotation, Quaternion.identity));//返回两个旋转之间的角度。

        //cube1.transform.rotation = Quaternion.AngleAxis(30, Vector3.up);//将转换旋转设置为绕y轴旋转30度
        //var a = Quaternion.Euler(Vector3.one);

        //cube1.transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.forward);//创建从旋转fromDirection到的旋转toDirection。

        //Quaternion q = Quaternion.Inverse(Quaternion.identity);//逆

        //Quaternion.Lerp();
        // Quaternion.LerpUnclamped();
        // Quaternion.LookRotation(Vector3.one, Vector3.one);
        //cube1.transform.rotation = Quaternion.RotateTowards(cube1.transform.rotation, Quaternion.Euler(new Vector3(90, 90, 90)), Time.time);
        //Quaternion.Slerp();
        //Quaternion.SlerpUnclamped();

        //cube1.transform.rotation *= Quaternion.identity;//

        #endregion

        #region Rigidbody（有些不懂）

        rb = cube1.GetComponent<Rigidbody>();
        //rb.angularDrag = 0.3f;//角速度的阻力
        //rb.centerOfMass = Vector3.one;//设置刚体重心
        //rb.detectCollisions = true;//忽略碰撞很好啊
        ////rb.freezeRotation();
        //rb.inertiaTensor = Vector3.one;//设置惯性的
        ////rb.maxDepenetrationVelocity;//未知
        //print(rb.position);//
        //print(rb.transform.position);
        //rb.rotation//这个方法新鲜
        //rb.sleepThreshold;//低于阈值进入睡眠
        //rb.solverIterations;//SolverIterations确定了刚体关节和碰撞接触的解析精度。
        //rb.solverIterations。
        //rb.worldCenterOfMass;//世界空间中刚体的质心

        //rb.AddExplosionForce();//添加爆炸力
        //rb.AddForceAtPosition(Vector3.one, transform.position);
        //rb.AddRelativeForce();//相对于它的局部坐标？
        //       rb.AddForce(transform.forward * 20f);
        //      rb.AddRelativeTorque(Vector3.up*20);
        //rb.ClosestPointOnBounds//最接近所附加对撞机边界框的点用于爆炸的受伤力案例
        // rb.GetPointVelocity();//刚体在世界坐标空间中worldPoint点的速度。搞不懂啊
        //rb.GetRelativePointVelocity();//搞不懂
        //rb.IsSleeping();
        //rb.MovePosition();
        //rb.MoveRotation();
        //rb.MovePosition(cube1.transform.position + Vector3.left * Time.deltaTime);
        //Quaternion del = Quaternion.Euler(new Vector3(0, 1, 0));
        //rb.MoveRotation(rb.rotation * del);
        //rb.ResetCenterOfMass();
        //rb.ResetInertiaTensor();
        //rb.SetDensity();//设置密度

        //rb.Sleep();//强制刚体睡眠一帧
        //rb.SweepTest(I);从刚体扫描有点意思啊
        //rb.SweepTestAll();
        //rb.WakeUp();//强制刚体唤醒

        #endregion

        #region Physice（有些不懂）

        //var g = Physics.AllLayers;//选择所有层
        //Physics.autoSimulation = true;//设置是否应自动模拟物理。
        //Physics.autoSyncTransforms;//每当“ 变换”组件发生更改时，是否自动将变换与物理系统同步。
        //Physics.bounceThreshold;//两个相对速度低于此速度的碰撞对象将不会反弹（默认值为2）。必须是积极的。
        //Physics.defaultContactOffset;//新创建的对撞机的默认接触偏移。//距离小于其contactOffset值之和的碰撞器将生成接触。触点偏移量必须为正。接触偏移使碰撞检测系统可以预测性地强制执行接触约束，即使物体稍微分开也是如此。
        //Physics.defaultPhysicsScene();//
        //Physics.DefaultRaycastLayers();//图层蒙版常数。
        //Physics.defaultSolverIterations//defaultSolverIterations确定解析刚体关节和碰撞接触的精度。（默认为6）。必须是积极的。
        //Physics.defaultSolverIterations;//defaultSolverVelocityIterations影响刚性刚体关节和碰撞接触的解析精度。（默认为1）。必须是积极的。
        //Physics.gravity = new Vector3(0, 0, 0);//修改重力。有趣的方法啊
        //Physics.IgnoreRaycastLayer();
        //Physics.interCollisionDistance();//设置布料相互碰撞的最小间隔距离。
        //Physics.interCollisionStiffness();//设置布料之间的碰撞刚度
        //Physics.queriesHitBackfaces();//物理查询是否应触及背面三角形。
        //Physics.queriesHitTriggers=true;//默认是否触发触发器。raycasts, spherecasts, overlap tests, 
        //Physics.reuseCollisionCallbacks();//确定垃圾收集器是否应仅对所有冲突回调重用Collision类型的单个实例。
        //Physics.sleepThreshold();//这个接口就很简单啊

        //Physics.BoxCast()//投射个盒子
        //Physics.BoxCastNonAlloc()//可能也是投射盒子

        //Physics.CheckBox()//检查是否重叠呢

        //Physics.ClosestPoint()//返回给定对撞机上最接近指定位置的点。

        //Physics.ComputePenetration();//这个方法很骚啊，计算将给定对撞机以指定姿势分开所需的最小平移。
        //Physics.GetIgnoreLayerCollision(1,1);//返回有没有忽略
        //Physics.IgnoreCollision();//这个不会持久的
        //Physics.IgnoreLayerCollision();//这个持久的应该是。
        //Physics.Linecast()//线性投射吧

        //Physics.OverlapSphere();//重叠的关系
        //Physics.OverlapSphereNonAlloc();//重叠的关系

        //Physics.Raycast();
        //Physics.Linecast();//这些差不多的，返回和参数不一样
        //Physics.RaycastAll();
        //Physics.RaycastNonAlloc()//类似于Physics.RaycastAll，但不会产生垃圾。
        //Physics.RebuildBroadphaseRegions//重建广相利益地区并设定世界边界。仅在使用Multi - box Pruning Broadphase时有效。

        //Physics.Simulate;//不知道有啥用
        //Physics.SphereCast();一个串的糖葫芦非常棒的方法啊
        //Physics.SyncTransforms; //当Transform组件发生更改时，可能需要根据Transform的更改重新放置，旋转或缩放该Transform上的任何刚体或碰撞器或其子级。使用此功能可以将这些更改手动刷新到物理引擎。

        #endregion

        #region event,eventsytstem(Input、physicsRaycaster、 独立输入模块(设置最大打字数目只知道一个)、PointerInputModule*、ExecuteEvents、BaseInputMode、PointerEventData、EventTrigger、EventSystem.current(EventSystem.SetSelectedGameObject)没看并且是重点）

        //private void OnGUI()
        //{
        //    Event e = Event.current;
        //    if (e.alt)
        //    {
        //        //e.button;//按下了哪个按钮
        //        //e.capsLock;//是否锁了
        //        //e.character;//键入的字符
        //        //e.clickCount;//多少次鼠标连续点击
        //        //e.command是否按住windows
        //        //e.commandName();//“复制”，“剪切”，“粘贴”，“删除”有关系
        //        //e.delta;//与上一个事件相比，鼠标相对位移
        //        //e.displayIndex;事件所属的显示索引
        //        //e.functionKey;//是功能键吗
        //        ///e.isKey;//是键盘事件
        //        ///e.isMouse;//是鼠标事件
        //        //e.keyCode;//这将返回与物理键盘按键匹配的KeyCode值
        //        //EventModifiers.Alt;按住了那些修饰键
        //        //e.mousePosition();//鼠标位置
        //        //e.numeric;//是数字按键
        //        //e.type;//事件的类型
        //        //e.GetTypeForControl(); 获取给定控件ID的过滤事件类型。
        //        //e.Use;//表示其事件已经用过
        //        //Event.GetEventCount(); 返回事件队列中存储的当前事件数。
        //        //Event.KeyboardEvent;//创建一个键盘事件。
        //        //Event.PopEvent;//从事件系统中获取下一个排队的Event;
        //    }

        //    //AbstractEventData a; a.Reset();//可以用于通过事件系统发送简单事件的类。
        //    //AxisEventData //与轴事件（控制器 / 键盘）关联的事件数据。

        //    //BaseEventData;// 包含新EventSystem中所有事件类型通用的基本事件数据的类。
        //    //基本输入这么多事件啊
        //    //Input.compositionCursorPos;
        //    //Input.compositionString;
        //    //Input.imeCompositionMode;
        //    //Input.mousePresent;
        //    //Input.mouseScrollDelta;
        //    //Input.touchCount;
        //    //Input.touchSupported;
        //    //Input.GetTouch;
        //    //Input.GetAxisRaw();

        //    //this.runInEditMode;//允许MonoBehaviour的特定实例在编辑模式下运行（仅在编辑器中可用）。
        //    //this.useGUILayout;//禁用此选项可以跳过GUI布局阶段。
        //    //this.hideFlags//该对象是否应该隐藏，随场景保存或可由用户修改？

        //    //this.CancelInvoke();
        //    //BaseInputModule sl;
        //    //EventSystem.current;
        //    //EventSystem.current.currentInputModule;
        //    //EventSystem.current.firstSelectedGameObject;
        //    //EventSystem.current.isFocused;
        //    //EventSystem.current.pixelDragThreshold;//用于拖动像素的软区域？、
        //    //EventSystem.current.sendNavigationEvents;//EventSystem是否应允许导航事件（移动/提交/取消）。
        //    //EventSystem.current.IsPointerOverGameObject;//具有给定ID的指针是否位于EventSystem对象上？
        //    //EventSystem.current.RaycastAll;//使用所有已配置的BaseRaycaster将其光线投射到场景中。
        //    //EventSystem.current.SetSelectedGameObject();//EventSystem.current.currentSelectedGameObject代表点击到的游戏对象?
        //    //EventSystem.current.UpdateModules;//重新计算BaseInputModules的内部列表。

        //    //ExecuteEvents.CanHandleEvent;//给定的GameObject能否处理T类型的IEventSystemHandler。
        //    //ExecuteEvents.Execute(); 在GameObject上执行类型T的事件：IEventSystemHandler。
        //    //ExecuteEvents.ExecuteHierarchy(); 递归调用Execute<T> 的层次结构，直到有一个可以处理该事件的GameObject。
        //    //ExecuteEvents.GetEventHandler(); 从根开始遍历对象层次结构，并返回实现<T> 类型事件处理程序的GameObject。
        //    //ExecuteEvents.ValidateEventData(); 尝试将数据转换为T类型。如果转换失败，则引发ArgumentException。

        //    //Physics2DRaycaster physics2DRaycaster;
        //    //physics2DRaycaster.eventCamera; 将为此光线投射器生成光线的相机。
        //    // physics2DRaycaster.renderOrderPriority;//基于渲染顺序的光线投射器的优先级。
        //    //physics2DRaycaster.sortOrderPriority; 光线投射器的优先级基于排序顺序。
        //    //physics2DRaycaster.depth;//获取已配置摄像机的深度。
        //    //physics2DRaycaster.eventMask;//允许的射线广播事件的掩码。
        //    //physics2DRaycaster.finalEventMask;//逻辑和“相机蒙版”和“ eventMask”。
        //    //physics2DRaycaster.maxRayIntersections;//	允许找到的射线相交的最大数量。

        //    //PhysicsRaycaster physicsRaycaster;
        //    //physicsRaycaster.ComputeRayAndDistance;//返回从相机经过事件位置的光线，以及沿该光线的近，远剪切平面之间的距离。
        //    //physicsRaycaster.Raycast;

        //    //枚举FramePressState、  给定帧的按下状态。

        //    PointerEventData pointerEventData;
        //    //pointerEventData.IsPointerMoving();
        //    //pointerEventData.Used;
        //    //pointerEventData.currentInputModule;
        //    //pointerEventData.selectedObject;
        //    //pointerEventData.Reset();
        //    //pointerEventData.Use();

        //    // pointerEventData.button; 此事件的InputButton。
        //    // pointerEventData.clickCount; 连续点击数。
        //    // pointerEventData.clickTime; 上次发送点击事件的时间。
        //    // pointerEventData.delta; 自上次更新以来的指针增量。
        //    // pointerEventData.dragging; 确定用户是拖动鼠标还是触控板。
        //    // pointerEventData.enterEventCamera; 与上一个OnPointerEnter事件关联的摄像机。
        //    // pointerEventData.hovered; 悬停堆栈中的对象列表。
        //    // pointerEventData.lastPress; 上次按下事件的GameObject。
        //    // pointerEventData.pointerCurrentRaycast; 与当前事件关联的RaycastResult。
        //    // pointerEventData.pointerDrag; 正在接收OnDrag的对象。
        //    //pointerEventData.pointerEnter 收到“ OnPointerEnter”的对象。
        //    // pointerEventData.pointerId; 指针的标识。
        //    // pointerEventData.pointerPress; 收到OnPointerDown的GameObject。
        //    // pointerEventData.pointerPressRaycast; 返回与鼠标单击，游戏手柄按钮按下或屏幕触摸相关的RaycastResult。
        //    // pointerEventData.position; 当前指针位置。
        //    // pointerEventData.pressEventCamera; 与上一个OnPointerPress事件关联的摄像机。
        //    // pointerEventData.pressPosition; 最后一个指针单击的屏幕空间坐标。
        //    // pointerEventData.rawPointerPress; 按下发生的对象，即使它不能处理按下事件。
        //    // pointerEventData.scrollDelta; 自上次更新以来的滚动量。
        //    // pointerEventData.useDragThreshold; 应该使用阻力阈值吗？

        //    //PointerInputModule c;//有15个方法(略过)
        //    ////由TouchInputModule和StandaloneInputModule使用。
        //    ////PointerInputModule.kFakeTouchesId;

        //    //c//有一些受保护的方法比如copyfromto

        //    //RaycastResult re;
        //    //re.depth;//元素的相对深度
        //    //re.distance; 到命中的距离。
        //    //re.gameObject; 被光线投射击中的GameObject。
        //    //re.index; 命中索引。
        //    //re.isValid; 是否有相关的模块和热门游戏对象。
        //    //re.module; 引发点击的BaseInputModule。
        //    //re.screenPosition; 产生射线广播的屏幕位置。
        //    //re.sortingLayer; 匹配对象的SortingLayer。
        //    //re.sortingOrder; 匹配对象的SortingOrder。
        //    //re.worldNormal; 射线投射命中位置处的法线。
        //    //re.worldPosition; 射线广播击中的位置的世界位置。

        //    // 独立输入模块，鼠标，键盘和控制器相关参数略过

        //    //枚举下
        //    //EventHandle.Used;
        //    //EventTriggerType.PointerClick
        //    // MoveDirection.Down
        //}

        //private void OnBeforeTransformParentChanged()
        //{

        //}

        //private void OnCanvasGroupChanged()
        //{

        //}
        //private void OnCanvasHierarchyChanged()//可能和UI有关系
        //{

        //}

        //private void OnDidApplyAnimationProperties()//可能和UI有关系
        //{

        //}

        //private void OnRectTransformDimensionsChange()//如果关联的RectTransform的尺寸已更改，则调用此回调。
        //{

        //}

        //private void OnTransformParentChanged()
        //{

        //}
        //private void OnValidate()//在加载脚本或在检查器中更改值时调用此函数（仅在编辑器中调用）。
        //{

        //}

        //private void Reset()//编辑器下的重置
        //{

        //}

        //void Starts()
        //{
        //    EventTrigger trigger = GetComponent<EventTrigger>();
        //    EventTrigger.Entry entry = new EventTrigger.Entry();
        //    entry.eventID = EventTriggerType.PointerDown;
        //    entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
        //    trigger.triggers.Add(entry);
        //}

        //public void OnPointerDownDelegate(PointerEventData data)
        //{
        //    Debug.Log("OnPointerDownDelegate called.");
        //}

        ////trigger事件
        ////OnUpdateSelected();
        ////OnInitializePotentialDrag();
        ////OnDeselect();选择一个新对象时候
        ////OnMove();

        #endregion

        #region 常用On

        //private void OnAnimatorIK(int layerIndex)
        //{
        //    //设置动画IK（反向运动学）的回调。
        //}

        //private void OnAnimatorMove()
        //{
        //    //回调，用于处理动画运动以修改根运动。
        //}

        //private void OnApplicationFocus(bool focus)
        //{
        //    //当玩家获得或失去焦点时发送给所有GameObject。
        //}

        //private void OnApplicationPause(bool pause)
        //{
        //    //当应用程序暂停时发送给所有GameObject。
        //}
        //private void OnApplicationQuit()
        //{
        //    //在应用程序退出之前发送给所有游戏对象。
        //}
        //private void OnAudioFilterRead(float[] data, int channels)
        //{
        //    //如果实现了OnAudioFilterRead，则Unity将在音频DSP链中插入一个自定义过滤器。
        //}

        //private void OnBecameInvisible()
        //{
        //    //当渲染器不再由任何相机可见时，将调用OnBecameInvisible。
        //}

        //private void OnBecameVisible()
        //{//当渲染器被任何相机可见时，将调用OnBecameVisible。

        //}
        //private void OnConnectedToServer()
        //{
        //    //功连接到服务器后，在客户端上调用。
        //}

        //private void OnControllerColliderHit(ControllerColliderHit hit)
        //{
        //    //当控制器在执行Move时击中对撞机时，将调用OnControllerColliderHit。
        //}

        //private void OnDisconnectedFromServer(NetworkDisconnection info)
        //{
        //    //当连接断开或您与服务器断开连接时，在客户端上调用。   
        //}
        //private void OnDrawGizmos()
        //{
        //    //如果要绘制也可拾取且始终绘制的小控件，请实现OnDrawGizmos。
        //}

        //private void OnDrawGizmosSelected()
        //{
        //    //如果选择了对象，则实现OnDrawGizmosSelected来绘制Gizmo。
        //}

        //private void OnFailedToConnect(NetworkConnectionError error)
        //{
        //    //由于某种原因连接尝试失败时在客户端上调用。
        //}
        //private void OnFailedToConnectToMasterServer(NetworkConnectionError error)
        //{
        //    //连接到MasterServer时出现问题时在客户端或服务器上调用。
        //}

        //private void OnJointBreak(float breakForce)
        //{
        //    //当连接到同一游戏对象的关节破裂时调用。
        //}
        //private void OnMasterServerEvent(MasterServerEvent msEvent)
        //{
        //    //报告来自MasterServer的事件时在客户端或服务器上调用。
        //}

        //private void OnNetworkInstantiate(NetworkMessageInfo info)
        //{
        //    //调用已被Network.Instantiate实例化的对象。
        //}

        //private void OnParticleCollision(GameObject other)
        //{
        //    //当粒子撞击碰撞器时，将调用OnParticleCollision。
        //}
        //private void OnParticleSystemStopped()
        //{

        //}
        //private void OnParticleTrigger()
        //{
        //    //当“粒子系统”中的任何粒子满足触发模块中的条件时，将调用OnParticleTrigger。
        //}
        //private void OnPlayerConnected(NetworkPlayer player)
        //{
        //    //每当新播放器成功连接时，在服务器上调用。
        //}
        //private void OnPlayerDisconnected(NetworkPlayer player)
        //{
        //    //每当播放器从服务器断开连接时，在服务器上调用。
        //}
        //private void OnPostRender()
        //{
        //    //相机完成场景渲染后，将调用OnPostRender。
        //}
        //private void OnPreCull()
        //{
        //    //在摄像机剔除场景之前，将调用OnPreCull。
        //}
        //private void OnPreRender()
        //{
        //    //在照相机开始渲染场景之前调用OnPreRender。
        //}
        //private void OnRenderImage(RenderTexture source, RenderTexture destination)
        //{
        //    //在完成所有渲染以渲染图像后，将调用OnRenderImage。
        //}
        //private void OnRenderObject()
        //{
        //    //相机渲染场景后调用OnRenderObject。
        //}
        //private void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
        //{
        //    //用于自定义网络视图监视的脚本中变量的同步。
        //}
        //private void OnServerInitialized()
        //{
        //    //当Network.InitializeServer被调用并完成时，在服务器上调用。
        //}
        //private void OnTransformChildrenChanged()
        //{
        //    //当GameObject转换的子代列表更改时，将调用此函数。
        //}
        //private void OnWillRenderObject()
        //{
        //    //果对象可见而不是UI元素，则为每个摄像机调用OnWillRenderObject。
        //}

        #endregion

        #region 点乘求角度和叉乘求方向（cross还是有点问题记到不能用官方夹角，还有我以前的提问）

        //Vector3 a = new Vector3(1, 0, 0);
        //Vector3 b = new Vector3(0, 1, 0);
        ////print(Vector3.Angle(a, b));
        ////print(Vector3.Dot(b.normalized, a.normalized));
        ////var dotres = Vector3.Dot(b.normalized, a.normalized);
        ////var g = dotres / (a.magnitude * b.magnitude);
        ////var g2 = Mathf.Acos(g) * Mathf.Rad2Deg;
        ////print(g2);

        //var c = Vector3.Cross(b.normalized, a.normalized);//不满足交换律，拇指是a,食指是b
        //                                                  // 下面两个相等呢
        //                                                  //print(c.magnitude);//这个和下面的print相等也是面积关系
        //                                                  //print(a.magnitude * b.magnitude * Mathf.Sign(Vector3.Angle(a, b)));

        //// GetAngle(a, b);
        //Cross();
        //// 关于叉乘
        //private void Cross()
        //{
        //    /*
        //      叉积 
        //      叉积的定义： c = a x b  其中a,b,c均为向量。两个向量的叉积是向量， 向量的模为  |c|=|a||b|sin<a,b>
        //      且 向量 c 垂直于 a、b， c 垂直于 a、b 组成的平面， a x b = - b x a;
        //    */
        //    // 定义两个向量 a、b
        //    Vector3 a = GameObject.Find("CubeA").transform.position;
        //    Vector3 b = GameObject.Find("CubeB").transform.position;

        //    //计算向量 a、b 的叉积，结果为 向量 
        //    Vector3 c = Vector3.Cross(a, b);

        //    // 下面获取夹角的方法，只是展示用法，太麻烦不必使用
        //    // 通过反正弦函数获取向量 a、b 夹角（默认为弧度）
        //    float radians = Mathf.Asin(Vector3.Distance(Vector3.zero, Vector3.Cross(a.normalized, b.normalized)));
        //    float angle = radians * Mathf.Rad2Deg;
        //    print("angle:" + angle);//这个才是角度
        //    float angle2 = Vector3.Angle(b, a);//这个明显更大不好用
        //    print("angle2:" + angle2);

        //    // 判断顺时针、逆时针方向，是在 2D 平面内的，所以需指定一个平面，下面以X、Z轴组成的平面为例（忽略 Y 轴）
        //    // 以 Y 轴为纵轴
        //    // 在 X、Z 轴平面上，判断 b 在 a 的顺时针或者逆时针方向
        //    if (c.y > 0)
        //    {
        //        // b 在 a 的顺时针方向
        //    }
        //    else if (c.y == 0)
        //    {
        //        // b 和 a 方向相同（平行）必须是头上就是平行！！
        //    }
        //    else
        //    {
        //        // b 在 a 的逆时针方向
        //    }
        //    print(c);
        //}

        //private void GetAngle(Vector3 a, Vector3 b)
        //{
        //    Vector3 c = Vector3.Cross(a, b);
        //    float angle = Vector3.Angle(a, b);

        //    // b 到 a 的夹角
        //    float sign = Mathf.Sign(Vector3.Dot(c.normalized, Vector3.Cross(a.normalized, b.normalized)));
        //    float signed_angle = angle * sign;

        //    Debug.Log("b -> a :" + signed_angle);

        //    // a 到 b 的夹角
        //    sign = Mathf.Sign(Vector3.Dot(c.normalized, Vector3.Cross(b.normalized, a.normalized)));
        //    signed_angle = angle * sign;

        //    Debug.Log("a -> b :" + signed_angle);
        //}

        #endregion（()

        #region 相机过度，2D朝向，3D朝向

        //private Vector3 target = new Vector3(0, 2, -2);
        //private Vector3 speed = Vector3.zero;
        //Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, target, ref speed, Time.deltaTime);
        //print(speed);

        //Vector3 temp = (CylinderTarget.position - Capsule.position);
        //temp.y = 0;
        //Quaternion target = Quaternion.LookRotation(temp);
        //Capsule.rotation = Quaternion.Lerp(Capsule.rotation, target, Time.deltaTime);

        //这里一定要搞懂，设计归一化（角度不需要归一化，其他都归一化）
        //print(direction);//这里我搞懂了tan45=1，而135是-1即可判断角度。
        //print(angle +":"+Vector3.Angle(T.position,Vector3.right));
        //这里还可以使用 Quaternion.Lerp来实现平滑旋转

        //Vector3 direction = T.position - Gou.position;
        //float angle = Mathf.Atan2(direction.normalized.y, direction.normalized.x) * Mathf.Rad2Deg;
        //Quaternion target = Quaternion.AngleAxis(angle, Vector3.forward);
        //Gou.transform.rotation = Quaternion.Slerp(Gou.rotation, target, Time.deltaTime);

        #endregion

        #region 大杂烩

        //ExecuteEvents.Execute<ICustomMessageTarget>(this.gameObject, null, (x, y) => x.Message1());

        //Matrix4x4 c;
        //c.isIdentity; 返回标识矩阵（只读）。
        //c.zero; 返回一个矩阵，其中所有元素都设置为零（只读）。

        //c.decomposeProjection; 此属性获取投影矩阵，并返回定义投影视锥的六个平面坐标。
        //c.determinant; 矩阵的行列式。
        //c.inverse 此矩阵的逆数（只读）。
        //c.isIdentity; 这是身份矩阵吗？
        //c.lossyScale; 尝试从矩阵中获取比例值。
        //c.rotation; 尝试从此矩阵获取旋转四元数。
        //c[2, 2];    [行，列] 处的访问元素。
        //c.transpose;	返回此矩阵的转置（只读）。

        //c.GetColumn;获取矩阵的一列。
        //c.GetRow;	返回矩阵的一行。
        //c.MultiplyPoint;	通过此矩阵（通用）转换位置。
        //c.MultiplyPoint3x4;	通过此矩阵转换位置（快速）。
        //c.MultiplyVector;通过此矩阵变换方向。
        //c.SetColumn;设置矩阵的一列。
        //c.SetRow;	设置矩阵的一行。
        //c.SetTRS;将此矩阵设置为平移，旋转和缩放矩阵。
        //c.rotation;	返回此矩阵的格式正确的字符串。
        //c.TransformPlane;	返回在空间中变换的平面。
        //c.ValidTRS;	检查此矩阵是否为有效的变换矩阵。

        //Matrix4x4.Frustum();此函数返回带有视锥的投影矩阵，该投影矩阵的近平面由传入的坐标定义。
        //Matrix4x4.LookAt();	给定一个源点，一个目标点和一个向上向量，将计算一个转换矩阵，该矩阵与从源头查看目标的摄像机相对应，以使右侧向量垂直于向上向量。
        //Matrix4x4.Ortho();	创建一个正交投影矩阵。
        //Matrix4x4.Perspective();	创建透视投影矩阵。
        //Matrix4x4.Rotate();创建旋转矩阵。
        //Matrix4x4.Scale();	创建缩放矩阵。
        //Matrix4x4.Translate();	创建一个翻译矩阵。
        //Matrix4x4.TRS();	创建平移，旋转和缩放矩阵。

        //c*c= 将两个矩阵相乘。

        #endregion

        #region Transform(transform.Rotate旋转关系不懂、TransformVector不懂)

        //cube1.transform.RotateAround(Vector3.zero,Vector3.up, 30*Time.deltaTime);
        //var playerTrans = GameObject.FindWithTag("Player").transform;
        /*/*print(cube1.transform.forward);//它自身的蓝色轴在世界坐标的位置
        print(cube1.transform.hasChanged = false);
        print(cube1.transform.hasChanged);
        print(cube1.transform.hierarchyCapacity = 1);
        print(cube1.transform.hierarchyCount);//包括了自己呢
        print(cube1.transform.localEulerAngles);//这个方法还可以用吧，嗯哼
        print(cube1.transform.localToWorldMatrix); //Transform.TransformPoint。和这个方法是差不多的
        print(cube1.transform.lossyScale);//对象的全局比例。
        print(cube1.transform.root);
        var a = cube1.transform.worldToLocalMatrix;//讲点从世界空间转换为局部空间的矩阵这个差不多的Transform.InverseTransformPoint。
        //cube1.transform.DetachChildren();
        print(cube1.transform.GetSiblingIndex());
        print(playerTrans.IsChildOf(cube1.transform));//这个比取消子物体的碰撞体好多了感觉呢，特别是return断层的使用
        //cube1.transform.LookAt(playerTrans,Vector3.left);//第二个参数指定世界坐标向上方向是哪个#1#
        //cube1.transform.Rotate(90,0,0,Space.Self);
        //cube1.transform.SetAsLastSibling();
        //cube1.transform.SetPositionAndRotation(Vector3.zero,Quaternion.identity);
        //cube1.transform.SetSiblingIndex(1);
        var b = cube1.transform.TransformDirection(new Vector3(3, 3, 3));//返回一个指示没有距离的方向的Unit Vector。
        var b2 = cube1.transform.TransformPoint(Vector3.one); //返回一个空间点。
        var b3 = cube1.transform.TransformVector(Vector3.one*3);//返回一个包含Direction和Distance的Vector，前提是您假设0,0,0是起点（不必担心其可交换性）。
        print(b +":"+b3);*/

        #endregion

        #region RectTransform(很多还没用过呢）

        RectTransform re;
        /*re.anchoredPosition; 此RectTransform枢轴相对于锚点参考点的位置。
        re.anchoredPosition3D; 此RectTransform枢轴相对于锚点参考点的3D位置。
        re.anchorMax; 父RectTransform中右上角锚定的规范化位置。
        re.anchorMin; 父RectTransform中左下角锚定的规范化位置。
        re.offsetMax; 矩形右上角相对于右上锚的偏移量。
        re.offsetMin; 矩形左下角相对于左下锚的偏移量。
        re.pivot; 在此RectTransform中旋转的归一化位置。
        re.rect; 在Transform的局部空间中计算出的矩形。
        re.sizeDelta; 此RectTransform的大小相对于锚点之间的距离。*/
        /*if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos))
        {
            rectTransform.anchoredPosition = pos;
        }*/

        /*re.ForceUpdateRectTransforms(); 强制重新计算RectTransforms内部数据。
        re.GetLocalCorners(); 获取其Transform的局部空间中所计算矩形的角。
        re.GetWorldCorners(); 获取世界空间中计算出的矩形的角。
        re.SetInsetAndSizeFromParentEdge(); 设置此矩形相对于父矩形的指定边的距离，同时还设置其大小。
        调用此方法将同时设置anchor，anchantedPosition和sizeDelta，但仅取决于水平或垂直分量，这取决于为插图指定的边。
        该方法在编写布局行为脚本时特别有用，因为它使指定相对于父边的位置变得简单，而无需考虑锚定功能。
        re.SetSizeWithCurrentAnchors(); 使RectTransform计算的rect为指定轴上的给定大小。
        re.reapplyDrivenProperties();
        re.ReapplyDrivenProperties //	为RectTransforms调用的事件，需要重新应用其驱动属性。*/

        #endregion

        #region 移动旋转（moveposition）

        /*Vector3 temp = Vector3.MoveTowards(cube1.transform.position, cube1.transform.position + Vector3.left,
            Time.deltaTime);
        rb.MovePosition(temp);*/
        //还有一个旋转也是差不多的方法吧

        #endregion

        #region Animator(很多方法和属性)，Animation

        Animation ab;
        //ab.isActiveAndEnabled;//该行为是否已激活并已启用？
        /*ab.animatePhysics; 启用后，动画将在物理循环中执行。这仅在与运动刚体结合时才有用。
        ab.clip; 默认动画。
        ab.cullingType; 控制此Animation组件的剔除。
        ab.isPlaying;
        ab.localBounds; 本地空间中此Animation动画组件的AABB。
        ab.playAutomatically; 是否应在启动时自动开始播放？
        ab['sk']; 返回名为name的动画状态。
        ab.wrapMode; 超出剪辑播放范围的时间应该如何处理？*/

        /*ab.AddClip(); 将剪辑添加到名称为newName的动画中。
        ab.Blend(); 在接下来的几秒钟内，将名为animation的动画向targetWeight混合。
        ab.CrossFade(); 在一段时间内以名称动画淡入动画，并淡出其他动画。
        ab.CrossFadeQueued(); Cross在先前的动画播放完毕后淡入动画。
        ab.GetClipCount(); 获取当前分配给该动画的剪辑数。
        ab.IsPlaying(); 正在播放名为name的动画吗？
        ab.Play(); 播放动画而不进行融合。
        ab.PlayQueued(); 在先前的动画播放完毕后播放动画。
        ab.RemoveClip(); 从动画列表中删除剪辑。
        ab.Rewind(); 倒回动画，名称为name。
        ab.Sample(); 在当前状态下采样动画。
        ab.Stop(); 停止以此动画开始的所有播放动画。*/

        Animator animator;

        /*animator.angularVelocity; 获取最后评估的帧的头像角速度。
        animator.applyRootMotion; 是否应采用根运动？
        animator.avatar; 获取 / 设置当前头像。
        animator.bodyPosition; 身体质心的位置。
        animator.bodyRotation; 身体质心的旋转。
        animator.cullingMode; 控制此Animator组件的剔除。
        animator.deltaPosition; 获取最后评估的帧的头像增量位置。
        animator.deltaRotation; 获取最后评估的帧的头像增量旋转。
        animator.feetPivotActive; 混合身体重心和脚部枢轴之间的枢轴点
        animator.fireEvents; 设置Animator是否发送AnimationEvent类型的事件。
        animator.gravityWeight; 基于当前播放的动画的当前重力权重。
        animator.hasBoundPlayables; 如果Animator分配了任何可玩对象，则返回true。
        animator.hasRootMotion; 如果当前装备具有根运动，则返回true。
        animator.hasTransformHierarchy; 如果对象具有转换层次结构，则返回true。
        animator.humanScale; 返回人形装备的当前头像的比例（如果装备是通用的，则默认为1）。
        animator.isHuman; 如果当前装备是类人机器人，则返回true；如果是一般装备，则返回false。
        animator.isInitialized; 返回动画器是否成功初始化。
        animator.isMatchingTarget; 如果自动匹配处于活动状态。
        animator.isOptimizable; 如果当前装备可以通过AnimatorUtility.OptimizeTransformHierarchy进行优化，则返回true。
        animator.keepAnimatorControllerStateOnDisable; 当禁用GameObject时，控制Animator组件的行为。
        animator.layerCount; 返回控制器中的层数。
        animator.layersAffectMassCenter; 附加层会影响质心。
        animator.leftFeetBottomHeight; 获取左脚底部的高度。
        animator.parameterCount; 返回控制器中的参数数。
        animator.parameters; 动画师使用的AnimatorControllerParameter列表。（只读）
        animator.pivotPosition; 获取枢轴的当前位置。
        animator.pivotWeight; 获取枢轴重量。
        animator.playableGraph; Animator创建的PlayableGraph。
        animator.playbackTime; 设置记录缓冲区中的播放位置。
        animator.recorderMode; 获取动画录制器的模式。
        animator.recorderStartTime; 缓冲区的第一帧相对于调用StartRecording的帧的开始时间。
        animator.recorderStopTime; 相对于调用StartRecording时的录制剪辑的结束时间。
        animator.rightFeetBottomHeight; 获得右脚底部的高度。
        animator.rootPosition; 根位置，游戏对象的位置。
        animator.rootRotation; 根旋转，即游戏对象的旋转。
        animator.runtimeAnimatorController; 控制Animator的AnimatorController的运行时表示形式。
        animator.speed; Animator的播放速度。1是正常播放速度。
        animator.stabilizeFeet; 在过渡和混合过程中，脚自动稳定。
        animator.targetPosition; 返回SetTarget指定的目标的位置。
        animator.targetRotation; 返回SetTarget指定的目标的旋转。
        animator.updateMode; 指定动画器的更新模式。
        animator.velocity; 获取最后评估的帧的化身速度。*/

        //Animator.StringToHash();

        /*
        animator.ApplyBuiltinRootMotion(); 应用默认的“根运动”。
        animator.CrossFade(); 使用标准化时间创建从当前状态到任何其他状态的淡入淡出。
        animator.CrossFadeInFixedTime(); 使用秒数创建从当前状态到任何其他状态的淡入淡出。
        animator.GetAnimatorTransitionInfo(); 返回一个AnimatorTransitionInfo，其中包含有关当前过渡的信息。
        animator.GetBehaviour<>(); 返回匹配T或从T派生的第一个StateMachineBehaviour。如果未找到，则返回null。
        animator.GetBehaviours(); 返回与类型T匹配或从T派生的所有StateMachineBehaviour。如果未找到，则返回null。
        animator.GetBoneTransform(); 返回映射到此人体骨骼ID的Transform。
        animator.GetBool(); 返回给定布尔参数的值。
        animator.GetCurrentAnimatorClipInfo(); 返回给定层当前状态下所有AnimatorClipInfo的数组。
        animator.GetCurrentAnimatorClipInfoCount(); 返回当前状态下的AnimatorClipInfo的数量。
        animator.GetCurrentAnimatorStateInfo(); 返回带有当前状态信息的AnimatorStateInfo。
        animator.GetFloat(); 返回给定float参数的值。
        animator.GetIKHintPosition()获取IK提示的位置。
            ;
        animator.GetIKHintPositionWeight(); 获取IK提示的翻译权重（0 = IK之前的原始动画，1 = 提示）。
        animator.GetIKPosition(); 获取IK目标的位置。
        animator.GetIKHintPositionWeight(); 获取IK目标的翻译权重（0 = IK之前的原始动画，1 = 目标）。
        animator.GetIKPosition(); 获取IK目标的旋转。
        animator.GetIKPositionWeight(); 获取IK目标的旋转权重（0 = IK之前的旋转，1 = IK目标上的旋转）。
            animator.GetIKRotation()获取IK目标的旋转。
        animator.GetIKRotationWeight(); 获取IK目标的旋转权重（0 = IK之前的旋转，1 = IK目标上的旋转）。

        animator.GetInteger(); 返回给定整数参数的值。
        animator.GetLayerIndex(); 返回具有给定名称的图层的索引。
        animator.GetLayerName(); 返回图层名称。
        animator.GetLayerWeight(); 返回指定索引处图层的权重。
        animator.GetNextAnimatorClipInfo(); 返回给定图层的下一个状态中所有AnimatorClipInfo的数组。
        animator.GetNextAnimatorClipInfo(); 返回处于下一个状态的AnimatorClipInfo的数量。
        animator.GetNextAnimatorClipInfo(); 返回带有下一个状态信息的AnimatorStateInfo。
        animator.GetParameter(); 请参见AnimatorController.parameters。
        animator.HasState(); 如果状态存在于此层中，则返回true，否则返回false。
        animator.InterruptMatchTarget(); 中断自动目标匹配。
        animator.IsInTransition(); 如果给定层上存在过渡，则返回true，否则返回false。
        animator.IsParameterControlledByCurve(); 如果参数由曲线控制，则返回true，否则返回false。
        animator.MatchTarget(); 自动调整GameObject的位置和旋转。
        animator.Play(); 播放状态
        animator.PlayInFixedTime(); 播放状态
        animator.Rebind(); 将所有动画属性和网格数据与Animator重新绑定。
        animator.ResetTrigger(); 重置给定触发参数的值。
        animator.SetBoneLocalRotation(); 设置IK传递期间人骨的局部旋转。
        animator.SetBool(); 设置给定布尔参数的值。
        animator.SetFloat(); 将float值发送到Animator以影响过渡。
        animator.SetIKHintPosition();
        animator.SetIKHintPositionWeight();
        animator.SetIKPosition();
        animator.SetIKHintPositionWeight();
        ;
        animator.SetIKRotation();
        animator.SetIKRotationWeight();
        animator.SetInteger(); 设置给定整数参数的值。
        animator.SetLayerWeight(); 在给定的索引处设置图层的权重。
        animator.SetLookAtPosition(); 设置外观位置
        animator.SetLookAtWeight(); 看一下重量。
        animator.SetTarget(); 设置当前状态的AvatarTarget和targetNormalizedTime。
        animator.SetTrigger(); 设置给定触发参数的值。
        animator.StartPlayback(); 将动画师设置为播放模式。
        animator.StartRecording(); 将动画制作者设置为录制模式，并分配一个大小为frameCount的循环缓冲区。
        animator.StopPlayback(); 停止动画播放器播放模式。当播放停止时，虚拟形象恢复从游戏逻辑获得控制权。
        animator.StopRecording(); 停止动画师录制模式。
        animator.Update(); 根据deltaTime评估动画师。
        animator.WriteDefaultValues(); 强制写入存储在动画器中的默认值。
        */

        #endregion

        #region 2D的效应器，3D关节，约束

        //PointEffector2D，模拟2D排斥与吸引效果。
        //SurfaceEffector2D，模拟2D物体表面方向力。
        //AreaEffector2D，模拟2D物体内部一个方向力。
        //PlatformEffector2D，模拟2D 物体平台的方向通过性。
        //Buoyancy Effector2D，模拟2D浮力效果。

        //距离关节：摇摆运动
        //固定关节：将它们保持在相对的位置
        //摩檫力关节：摩擦接头2D可以将对象之间的线速度和角速度都减小到零（即，将它们放慢）。例如，您可以使用此关节模拟自上而下的摩擦。
        //铰链关节：和距离差不多，只不过可以旋转
        //相对位置关节：
        //滑动关节：这种关节        使受刚体物理控制的游戏对象可以在空间中沿着直线滑动，像缆车一样
        //弹簧关节：所述弹簧 Joint__ 2D__部件允许两个游戏对象由刚体物理控制为通过弹簧一起，好像连接。弹簧将沿其在两个对象之间的轴线施加力，以使它们保持一定距离。
        //目标关节：您可以使用它来拾取和移动在重力作用下的物体。没搞懂啊可能做那种绕着一点旋转的吧
        //车轮关节不说了

        //目标约束：“目标约束”使GameObject旋转以面向其源GameObjects    。它还可以为另一个轴保持一致的方向。
        //观察约束：和目标约束差不多的
        //父亲约束：父约束可移动和旋转GameObject
        //位置约束：位置约束组件将GameObject移至其源GameObjects .位置保持相对了
        //旋转约束：同位置
        //比例约束：

        #endregion
    }

    private void FixedUpdate() //fixedUpdate会统一Tiem.deltaTime
    {
    }
}