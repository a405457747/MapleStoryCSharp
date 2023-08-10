//Author: SkyAllen                                                                                                                  
//Email: 894982165@qq.com      

namespace PurpleGarlic
{
    public static class Const
    {
        public const string LowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
        public const string Digits = "0123456789";

        public const int FontSizeS = 30;
        public const int FontSizeM = 40;
        public const int FontSizeL = 50;

        public const float Second = 1;
        public const float Minute = Second * 60;
        public const float Hour = Minute * 60;

        public const string NowFormatStr = "yyyy_MMdd_HHmm";

        public static readonly int[] Rotate90 = {0, 90, 180, 270};
    }

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

    public enum TimeTotalType
    {
        Null,
        Second,
        Minute,
        Hour
    }

    public enum ColorType
    {
        Null,
        Red,
        Blue,
        Yellow,
        Green,
        Purple
    }

    public enum CardQuality
    {
        Null,
        White,
        Blue,
        Purple,
        Orange
    }
}