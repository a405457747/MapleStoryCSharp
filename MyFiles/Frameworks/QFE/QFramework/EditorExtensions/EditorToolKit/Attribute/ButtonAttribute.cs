﻿// /****************************************************************************
//  * Copyright (c) 2018 ZhongShan KPP Technology Co
//  * Date: 2018-03-14 20:39
//  ****************************************************************************/

using System;

namespace UnityEngine
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ButtonAttribute : PropertyAttribute
    {
        public readonly string[] funcNames;
        public float height = 16;
        public ShowIfRunTime showIfRunTime = ShowIfRunTime.All;

        public ButtonAttribute(params string[] funcNames)
        {
            this.funcNames = funcNames;
        }
    }
}