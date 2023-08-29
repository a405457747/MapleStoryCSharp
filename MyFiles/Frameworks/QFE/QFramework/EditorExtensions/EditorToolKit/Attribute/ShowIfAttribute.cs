using System;

namespace UnityEngine
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ShowIfAttribute : PropertyAttribute
    {
        public enum Mode
        {
            Or,
            And
        }

        public Mode mode = Mode.Or;

        public Target[] targets;

        public ShowIfAttribute(params string[] targetNames)
        {
            if (targetNames == null) return;

            targets = new Target[targetNames.Length];
            for (var i = 0; i < targetNames.Length; i++)
            {
                var targetName = targetNames[i];
                targets[i] = TargetResolver(targetName);
            }
        }

        internal static Target TargetResolver(string inputString)
        {
            //如果不是以@开头的，格式不对，不给找变量
            var correct = inputString != null && inputString.Length > 1 && inputString[0] == '@';
            if (!correct) return null;

            //如果不是以@!开头的，就是反向的，前面跳过@!，否则只跳过@
            var targetShow = inputString[1] != '!';
            if (!targetShow && inputString.Length <= 1) //排除只写了"@!"
                return null;

            var start = targetShow ? 1 : 2;
            var targetName = inputString.Substring(start, inputString.Length - start);
            return new Target(targetName, targetShow);
        }

        public class Target
        {
            public readonly string name;
            public readonly bool show = true;

            public Target(string name, bool show)
            {
                this.name = name;
                this.show = show;
            }
        }
    }
}