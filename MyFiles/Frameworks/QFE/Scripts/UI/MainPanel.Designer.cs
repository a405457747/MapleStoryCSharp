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
    
    
    // Generate Id:bed19a22-9893-4eb6-b521-3d571fe00eae
    public partial class MainPanel
    {
        
        public const string NAME = "MainPanel";
        
        [SerializeField()]
        public UnityEngine.UI.Text TitleXXLCommonText;

        [SerializeField()]
        public UnityEngine.UI.Text VerMCommonText;

        [SerializeField()]
        public CommonButton ContinueCommonButton;
        
        [SerializeField()]
        public CommonButton StartCommonButton;
        
        [SerializeField()]
        public CommonButton SetCommonButton;
        
        [SerializeField()]
        public CommonButton QuitCommonButton;
        
        private MainPanelData mPrivateData = null;
        
        public MainPanelData mData
        {
            get
            {
                return mPrivateData ?? (mPrivateData = new MainPanelData());
            }
            set
            {
                mUIData = value;
                mPrivateData = value;
            }
        }
        
        protected override void ClearUIComponents()
        {
            TitleXXLCommonText = null;
            ContinueCommonButton = null;
            StartCommonButton = null;
            SetCommonButton = null;
            QuitCommonButton = null;
            mData = null;
            VerMCommonText = null;
        }
    }
}
