using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DG.Tweening;
using QFramework;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CallPalCatGames.QFrameworkExtension
{
    public enum Direction
    {
        Null,
        Left,
        Right,
        Up,
        Down
    }

    public enum AxisDirection
    {
        Null,
        Up,
        Right,
        Forward
    }

    public enum CostType
    {
        Null,
        GoldCoin,
        PhysicalPower,
        Diamond
    }

    public enum Game2DTurnDir
    {
        Null,
        Left,
        Right
    }

    public enum Vector2ListType
    {
        Null,
        Cross,
        Fork
    }

    public enum TimeTotalType
    {
        Null,
        Second,
        Minute,
        Hour
    }

    public class Tool
    {
        public static int GetUnixStartToNowTimeTotal(TimeTotalType type)
        {
            var unixStartToNowTimeTotal = 0;
            var ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0);
            switch (type)
            {
                case TimeTotalType.Second:
                    unixStartToNowTimeTotal = Convert.ToInt32(ts.TotalSeconds);
                    break;
                case TimeTotalType.Minute:
                    unixStartToNowTimeTotal = Convert.ToInt32(ts.TotalMinutes);
                    break;
                case TimeTotalType.Hour:
                    unixStartToNowTimeTotal = Convert.ToInt32(ts.TotalHours);
                    break;
            }

            return unixStartToNowTimeTotal;
        }

        public static void ClearMemory()
        {
            GC.Collect();
            Resources.UnloadUnusedAssets();
        }

        public static Color GetColor(float r, float g, float b, float a)
        {
            return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
        }

        public static void Swap<T>(ref T a, ref T b) where T : struct
        {
            var temp = a;
            a = b;
            b = temp;
        }

        public static string GetShortForNumber(long num, string digits = "f1")
        {
            var numBit = num.ToString().Length;
            var numFloat = num;
            if (numBit < 4) return num.ToString();
            if (numBit >= 4 && numBit < 7)
            {
                numFloat /= 1000L;
                return $"{numFloat.ToString(digits)}K";
            }

            if (numBit >= 7 && numBit < 10)
            {
                numFloat /= 1000000L;
                return $"{numFloat.ToString(digits)}M";
            }

            if (numBit >= 10 && numBit < 13)
            {
                numFloat /= 1000000000L;
                return $"{numFloat.ToString(digits)}B";
            }

            if (numBit >= 13 && numBit < 16)
            {
                numFloat /= 1000000000000L;
                return $"{numFloat.ToString(digits)}T";
            }

            if (numBit >= 16 && numBit < 19)
            {
                numFloat /= 1000000000000000L;
                return $"{numFloat.ToString(digits)}aa";
            }

            if (numBit >= 19 && num < long.MaxValue)
            {
                numFloat /= 1000000000000000000L;
                return $"{numFloat.ToString(digits)}bb";
            }

            //cc uu
            return "Max";
        }

        public static string GetFixedValueStr(ref int currentValue, int maxValue, int minValue,
            out bool isArriveMaxValue, out bool isArriveMinValue)
        {
            currentValue = Mathf.Clamp(currentValue, minValue, maxValue);
            isArriveMaxValue = currentValue == maxValue ? true : false;
            isArriveMinValue = currentValue == minValue ? true : false;
            if (currentValue == maxValue) return "Max";
            if (currentValue == minValue) return "Min";
            return currentValue.ToString();
        }

        public static int GetNumberByString(string str)
        {
            if (string.IsNullOrEmpty(str)) throw new NullReferenceException();
            return int.Parse(Regex.Replace(str, @"[^0-9]+", ""));
        }

        public static long GetTheSumByCumulativeLevel(int baseNum, int level) //来自累积的数值
        {
            var arr = new long[level];
            for (var i = 0; i < level; i++) arr[i] = baseNum * (i + 1);
            return arr.Sum();
        }

        public static void PrintCollectionItem<T>(ICollection<T> collection)
        {
            foreach (var item in collection) Log.I(item);
        }

        public static bool BigListIsMatchSmallList(List<int> bigMatchList, List<int> smallList)
        {
            var tempSmallList = new List<int>(smallList);
            for (var i = 0; i < bigMatchList.Count; i++)
                if (tempSmallList.Contains(bigMatchList[i]))
                    tempSmallList.Remove(bigMatchList[i]);
            return tempSmallList.Count == 0;
        }

        public static float GetAnimationClipLengthByName(string name, Animator animator)
        {
            var length = 0f;
            var clips = animator.runtimeAnimatorController.animationClips;
            foreach (var clip in clips)
                if (clip.name.Equals(name))
                {
                    length = clip.length;
                    break;
                }

            return length;
        }

        public static bool IsEqualityFloat(float f1, float f2)
        {
            return Math.Abs(f1 - f2) < 0.000001f;
        }

        public static T GetEnumByInt<T>(long val) where T : System.Enum
        {
            return (T) Enum.Parse(typeof(T), val.ToString());
        }

        #region 位运算

        public static bool IsEvenNum(int a)
        {
            return (a & 1) == 0 ? true : false;
        }

        #endregion

        #region 初中数学（圆台、棱锥、棱柱、棱台先省略）

        public static float GetSphereBulk(float r)
        {
            return 4f / 3 * Mathf.PI * Mathf.Pow(r, 3);
        }

        public static float GetSphereArea(float r)
        {
            return 4f * Mathf.PI * Mathf.Pow(r, 2);
        }

        public static float GetCircleArea(float r)
        {
            return (float) (Mathf.PI * Math.Pow(r, 2));
        }

        public static float GetCircularBulk(float s, float h) //圆锥体积
        {
            return 1f / 3 * s * h;
        }

        public static float GetCircularArea(float r, float l) //圆锥表面积，l是母线
        {
            return Mathf.PI * r * l + Mathf.PI * Mathf.Pow(r, 2);
        }

        public static float GetSectorArea(float l, float r) //扇形面积l是弧长
        {
            return l * r / 2f;
        }

        public static float GetSectorArcLenth(float n, float r) //n是角度
        {
            return n * Mathf.PI * r / 180f;
        }

        public static float GetCylinderArea(float h, float r) //圆柱表面积
        {
            var s1 = (float) (2 * Math.PI * r * h);
            var s2 = 2 * Mathf.PI * Mathf.Pow(r, 2);
            return s1 + s2;
        }

        public static float GetRhombusArea(float c, float d) //c、d对角线
        {
            return c * d / 2;
        }

        public static float GetEllipsePerimeter(float a, float b) //a表示椭圆长半轴的长,b表示椭圆短半轴的长
        {
            return 2 * Mathf.PI * b + 4 * (a - b);
        }

        public static float GetEllipseArea(float a, float b) //a表示椭圆长半轴的长,b表示椭圆短半轴的长
        {
            return Mathf.PI * b * a;
        }

        //正弦定理：a/sinA=b/sinB=c/sinC=2R=D(外接圆半径R)
        //余弦定理：c^2=a^2+b^2-2abcosC(c边的对角是ab的夹角)
        //切割线定理：PT^2 =PA*PB
        //圆标准方程：(x-a)^2+(y-b)^2 =r^2;(a,b是圆心坐标)
        //抛物线方程：x^2=-2py(p>0)参数p的几何意义，是焦点到准线的距离,暂时搞不懂呢
        //公式：(a+b)*(a-b)=a^2-b^2;
        //公式：(a+b)^2=a^2+b^2+2ab;

        #endregion

        #region 高中数学

        #endregion

        #region 初中物理

        //HZ表示每秒振动一次
        //20000hz是超声波
        //3*10^8,水为3/4比列
        //光不同介质折射
        //颜料三原色红黄蓝
        //眼睛的变厚
        //g.8n/kg
        //力作用点，方向，大小
        //滑动摩擦力和运动方向相反，滚动摩擦，静摩擦(手握住瓶子)
        //牛一：一起物理总是静止或者匀速直线运动，也是惯性定律
        //力不是使物体运动的原因，力也不是维持物体运动状态的原因，维持物体运动状态不变的是惯性，力是改变物体运动状态的原因。
        //压强密度和水深
        //p=f/s=pgh;密度深度和g
        //阿基米德原理：物体所受浮力=排开水重力=水密度*g*排开水体积
        //流速大的地方压强小，流速小的地方压强大
        //杠杆平衡动力*动力臂=阻力*阻力臂
        //机械能=动能+势能；
        //焦耳定律：Q=I^2RT;
        //通电周围存在磁场
        //电磁感应机械能转化电能
        private static float Getensity(float m, float v)
        {
            return m / v;
        }

        private static float Getravity(float m) //获得重力
        {
            return m * 9.8f;
        }

        private static float GetAchievement(float f, float s)
        {
            return f * s;
        }

        #endregion

        #region 高中物理

        //速度位移公式　2*g*h =vy^2;
        //牛顿第二定律：F=ma;
        //牛顿第三定律：力和反作用力相等
        //曲线运动
        //平抛（分解两运动）tanTheta =2tanAlpha
        //周期*频率=1
        //向心力指向圆心，
        //线速度是圆周运动快慢物理量，通过的弧长
        //匀速圆周运动v=r*角速度=2*3.14*r/T
        //平面内的圆周运动！！
        //轻sheng模型，轻杆模型忽略
        //圆锥摆动F =mgtanTheta=mlw^2=m*v^2/l
        //万有引力定律F=Gm1m2/r^2,F向=F引，R^2g=GM
        //动量定律Ft=p2-p1;物体所受合外力的冲量等于动量的变化，求位移
        //动量守恒定律：一个系统不受外力或所受外力和为0，这个系统总动量不变
        //w=fs;
        //p=fv;瞬时功率
        //汽车运动曲线
        //动能和动量不一样ek=0.5*mv^2;两者有联系
        //动能定律：w=0.5*mvt^2-0.5*mv0^2
        //重力势能和重力做功都是w=mgh;
        //弹性势能=弹力做功
        //机械能守恒定律E=Ek+Ep;完全非弹性碰撞过程机械能不守恒
        //简谐运动T=2*3.14*math.sqr(m/k);k是弹簧系数
        //单摆动运动T=2*3.14*math.sqr(l/g);
        //驱动力的频率等于振动系统固有单频率
        //机械波
        //波动和振动研究不一样
        //波动衍射条件，小孔比波长小，波具有独立性，波还有干涉现象，公式忽略
        //热力学w+q=u;内能=做功和吸收的热量
        //能量守恒
        //电荷守恒
        //电场：带电周围存在一种物质，电荷作用电媒体
        //电势差功/电量
        //电阻定律R=pL/s;
        //超导现象：零度电阻减少为0
        //焦耳定律Q=I^2Rt;
        //电动势
        //闭合电路欧姆定律I=E/（R+r）；
        //电效率和功率
        //磁场对磁体、电流、运动电荷有力作用
        //安培力F=BIL
        //洛伦兹力
        //楞次定律
        //电磁感应定律
        //自感电动势
        //电流有效值
        //电感和电容
        //变压器
        //电磁转换和电磁波
        //光两种影子
        //折射率公式=c/v光速/速度
        //全反射
        //光波长和频率
        //红光折射率和速度都比紫光大
        //光波动性和微粒性
        //光的干涉、衍射
        //光是电磁波
        //光电效应Ek=hv-w逃
        //v高的光子表现出粒子性，低为波动性，光子动量p=h/波长
        private static float GetAccelerated(float vt, float v0, float t, float to) //加速度是描述速度快慢的物理量
        {
            return (vt - v0) / (t - to);
        }

        private static float GetSpeed(float v0, float a, float t)
        {
            return v0 + a * t;
        }

        private static float GetAccelerated(float vt, float v0, float s) //初速度，末速度
        {
            return (Mathf.Pow(vt, 2) - Mathf.Pow(v0, 2)) / (2 * s);
        }

        private static float GetDisplacement(float vo, float t, float a)
        {
            return vo * t + 0.5f * a * Mathf.Pow(t, 2);
        }

        private static float GetMomentum(float m, float v)
        {
            return m * v;
        }

        private static float GetImpulse(float f, float t)
        {
            return f * t;
        }

        #endregion

        #region Vector相关

        public static Vector3 GetCursorPos()
        {
            var cursorPos = Camera.main.ScreenToWorldPoint(new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                -Camera.main.transform.position.z
            ));
            return cursorPos;
        }

        public void ShakeTheCamera(Vector3 initCameraPos)
        {
            Camera.main.DOShakePosition(0.5f)
                .OnComplete(() => { Camera.main.transform.localPosition = initCameraPos; });
        }

        //得到爆炸伤害系数根据于距中心爆炸点距离
        public static float GetBoomDemageRateByDistanceFromTheBoomCenter(float boomRadius, Vector3 boomCenterPos,
            Vector3 hurtPos)
        {
            var dis = Mathf.Abs(Vector3.Distance(boomCenterPos, hurtPos));
            return (boomRadius - dis) / boomRadius;
        }

        public static Vector2[] GetFourDirVector2List(Vector2 centerPoint, Vector2ListType type)
        {
            Vector2[] tempVector2List = null;
            switch (type)
            {
                case Vector2ListType.Cross:
                    tempVector2List = new Vector2[4]
                    {
                        new Vector2(centerPoint.x + 1, centerPoint.y),
                        new Vector2(centerPoint.x - 1, centerPoint.y),
                        new Vector2(centerPoint.x, centerPoint.y + 1), new Vector2(centerPoint.x, centerPoint.y - 1)
                    };
                    break;
                case Vector2ListType.Fork:
                    tempVector2List = new Vector2[4]
                    {
                        new Vector2(centerPoint.x + 1, centerPoint.y + 1),
                        new Vector2(centerPoint.x - 1, centerPoint.y - 1),
                        new Vector2(centerPoint.x + 1, centerPoint.y - 1),
                        new Vector2(centerPoint.x - 1, centerPoint.y + 1)
                    };
                    break;
            }

            return tempVector2List;
        }

        #endregion

        #region Tween

        public static Tween GetNoKillNoRunTween(Tween tween)
        {
            return tween.SetAutoKill(false).Pause();
        }

        public static Tween StopTween(Tween tween)
        {
            if (tween.active) tween.Pause();
            return tween;
        }

        #endregion

        #region Rule工具

        public static T GetRuleThing<T>(T[] thingArrray, string flag, Dictionary<string, int> ruleIndexDic)
        {
            if (ruleIndexDic == null) ruleIndexDic = new Dictionary<string, int>();
            if (!ruleIndexDic.ContainsKey(flag)) ruleIndexDic.Add(flag, 0);
            ruleIndexDic[flag] = ruleIndexDic[flag] % thingArrray.Length;
            var res = thingArrray[ruleIndexDic[flag]];
            ruleIndexDic[flag] += 1;
            return res;
        }

        public static void ResetRuleThing(string flag, Dictionary<string, int> ruleIndexDic)
        {
            if (ruleIndexDic != null) ruleIndexDic[flag] = 0;
        }

        #endregion

        #region 随机工具

        public static List<int> Shuffle(int count)
        {
            var oldIndexs = Enumerable.Range(0, count).ToList(); //存储是Index
            var newIndexs = new List<int>(); //空的
            while (oldIndexs.Any())
            {
                var randomVal = oldIndexs[Random.Range(0, oldIndexs.Count)]; //值是Index
                newIndexs.Add(randomVal);
                oldIndexs.Remove(randomVal);
            }

            return newIndexs;
        }

        public static int GetRandomOneOrNegativeOne(out bool birthTrue)
        {
            birthTrue = CanBirthPrecision1000(0.5f);
            return birthTrue ? -1 : 1;
        }

        //数组随机返回东西的方法
        public static T GetArrayRandomItem<T>(T[] container)
        {
            var randomValue = Random.Range(0, container.Length);
            return container[randomValue];
        }

        //获取浮点数的随机偏移值
        public static float GetRandomPositiveAndNegative(float baseValue)
        {
            return Random.Range(-baseValue, baseValue);
        }

        /// <summary>
        ///     参数birthProbability为1时候，代表100%几率
        /// </summary>
        /// <param name="birthProbability"></param>
        /// <returns></returns>
        public static bool CanBirthPrecision1000(float birthProbability)
        {
            var random = Random.Range(1, 1001);
            var birthValue = (int) (birthProbability * 1000);
            return random <= birthValue;
        }

        public static string GetProbabilityPercentage(ref float birthProbability)
        {
            var tempValue = (int) (birthProbability * 100);
            tempValue = Mathf.Clamp(tempValue, 0, 100);
            birthProbability = tempValue / 100f;
            return $"{tempValue}%";
        }

        public static Vector2 GetRandomVector2AccuracyOf1(Vector2 vector2) //其中参数必须是Vector2.Zero
        {
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) + vector2;
        }

        #endregion

        #region csv

        public static void LoadCsvTxt(string contentStr, out Dictionary<string, Dictionary<string, string>> big)
        {
            var lineAarry = contentStr.Split('\n');
            var row = lineAarry.Length - 1;
            var column = lineAarry[0].Split(',').Length;
            AnalysisCsvTxtStep(lineAarry, row, column, out big);
        }

        private static void AnalysisCsvTxtStep(string[] lineAarry, int row, int column,
            out Dictionary<string, Dictionary<string, string>> big)
        {
            var dic = new string[row, column];
            for (var i = 0; i < row; i++)
            for (var j = 0; j < column; j++)
                dic[i, j] = lineAarry[i].Split(',')[j].Trim();
            ChangeToDic(dic, out big);
        }

        private static void ChangeToDic(string[,] dic, out Dictionary<string, Dictionary<string, string>> big)
        {
            big = new Dictionary<string, Dictionary<string, string>>();
            var row = dic.GetLength(0);
            var col = dic.GetLength(1);
            for (var i = 0; i < row; i++)
            for (var j = 0; j < col; j++)
                if (i == 0)
                {
                    var small = new Dictionary<string, string>();
                    big.Add(dic[i, j], small);
                }

            foreach (var headfield in big.Keys)
            {
                if (headfield == "ID") continue;
                for (var i = 1; i <= row - 3; i++)
                    big[headfield].Add(i.ToString(), GetAnalysisCsvTxtWord(dic, headfield, i));
            }
        }

        //根据头字段和Id获取内容
        private static string GetAnalysisCsvTxtWord(string[,] dic, string headFieldName, int id)
        {
            //获取字段所在的行数
            var row = 0;
            var col = 0;
            for (var j = 0; j < dic.GetLength(1); j++)
                if (dic[0, j] == headFieldName)
                {
                    col = j;
                    break;
                }

            //获取id所在的列数
            for (var i = 0; i < dic.GetLength(0); i++)
                if (dic[i, 0] == id.ToString())
                {
                    row = i;
                    break;
                }

            var word = dic[row, col];
            return word == "" ? dic[2, col] : word;
        }

        #endregion
    }
}