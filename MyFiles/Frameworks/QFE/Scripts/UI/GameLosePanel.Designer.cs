//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CallPalCatGames.QFrameworkExtension
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;
    
    
    // Generate Id:89a7205a-5a21-4fdd-9853-c3dfced3ce77
    public partial class GameLosePanel
    {
        
        public const string NAME = "GameLosePanel";
        
        [SerializeField()]
        public CommonButton CrossCommonButton;
        [SerializeField()]
        public CommonButton ReplayCommonButton;
        private GameLosePanelData mPrivateData = null;
        
        public GameLosePanelData mData
        {
            get
            {
                return mPrivateData ?? (mPrivateData = new GameLosePanelData());
            }
            set
            {
                mUIData = value;
                mPrivateData = value;
            }
        }
        
        protected override void ClearUIComponents()
        {
            CrossCommonButton = null;
            ReplayCommonButton = null;
            mData = null;
        }
    }
}
