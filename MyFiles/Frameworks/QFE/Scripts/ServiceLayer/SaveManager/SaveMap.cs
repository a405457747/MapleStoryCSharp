using UniRx;

namespace CallPalCatGames.QFrameworkExtension
{
    public class SaveMap
    {
        public IntReactiveProperty CurrentGameLevel = new IntReactiveProperty(1); //初始化从1开始了
        public BoolReactiveProperty IsShake = new BoolReactiveProperty(true);
        public IntReactiveProperty LanguageKey = new IntReactiveProperty(0);
        public BoolReactiveProperty MusicOn = new BoolReactiveProperty(true);
        public BoolReactiveProperty SoundOn = new BoolReactiveProperty(true);
        public IntReactiveProperty TimestampSeconds = new IntReactiveProperty(0);
    }
}