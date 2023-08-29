/****************************************************************************
 * Copyright (c) 2019 ~ 2019.12 S.Allen
 * 
 * 894982165@qq.com
 * https://github.com/a405457747
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 ****************************************************************************/

using QFramework;
using UnityEditor;

namespace CallPalCatGames.QFrameworkExtension
{
    public class EditorExpand
    {
#if UNITY_EDITOR
        [MenuItem("QFrameworkExtension/PrintPlayerPrefs")]
        private static void PrintPlayerPrefs()
        {
            SaveManager.Instance.PrintPlayerPrefs();
        }
#endif
#if UNITY_EDITOR
        [MenuItem("QFrameworkExtension/GameWin")]
        private static void GameWin()
        {
            QEventSystem.SendEvent(GameEvent.GameWin);
        }
#endif
#if UNITY_EDITOR
        [MenuItem("QFrameworkExtension/GameLose")]
        private static void GameLose()
        {
            QEventSystem.SendEvent(GameEvent.GameOver);
        }
#endif
    }
}