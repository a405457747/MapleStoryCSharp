/*⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵
☠ ©2020 Chengdu Mighty Vertex Games. All rights reserved.                                                                        
⚓ Author: SkyAllen                                                                                                                  
⚓ Email: 894982165@qq.com                                                                                  
⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵*/

public class MathHelper
{
    public static int GetCombinationNumber(int n, int m)
    {
        checked
        {
            var top = GetFactorial(n);
            var bottomLeft = GetFactorial(m);
            var bottomRight = GetFactorial(n - m);
            return top / (bottomLeft * bottomRight);
        }
    }

    private static int GetFactorial(int n)
    {
        if (n < 2)
            return 1;
        return GetFactorial(n - 1) * n;
    }


    public static bool IsEven(int num)
    {
        return (num & 1) != 1;
    }
}