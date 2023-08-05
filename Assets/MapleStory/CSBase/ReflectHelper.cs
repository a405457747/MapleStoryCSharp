
namespace MapleStory
{
    public static class ReflectHelper
    {
        public static T GetField<T>(object ins, string name)
        {
            //var AssemblyCSharp = AppDomain.CurrentDomain.GetAssemblies().First(v =>v.FullName.StartsWith("Assembly-CSharp-Editor,"));

            var temp = ins.GetType().GetField(name).GetValue(ins);
            return (T)temp;
        }
    }
}