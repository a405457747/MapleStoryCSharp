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
    
    
    // Generate Id:66992136-5fee-4814-b1f9-deecc476c8a7
    public partial class LevelMainPanel
    {
        
        public const string NAME = "LevelMainPanel";
        
        [SerializeField()]
        public CommonButton PauseCommonButton;
        
        [SerializeField()]
        public UnityEngine.UI.Text LevelNumLCommonText;

        private LevelMainPanelData mPrivateData = null;
        
        public LevelMainPanelData mData
        {
            get
            {
                return mPrivateData ?? (mPrivateData = new LevelMainPanelData());
            }
            set
            {
                mUIData = value;
                mPrivateData = value;
            }
        }
        
        protected override void ClearUIComponents()
        {
            PauseCommonButton = null;
            LevelNumLCommonText = null;
            mData = null;
        }
    }
}