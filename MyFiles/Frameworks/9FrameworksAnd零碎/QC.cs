using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using System;
using System.Linq;
using UniRx;
using  UnityEngine.UI;

namespace QFrameworkStudy
{
    public class QC : MonoBehaviour
    {
        public static QC Instance { get; private set; }
        public QFrameworkContainer QFrameworkContainer;
        
        void Awake()
        {
            Instance = this;
            QFrameworkContainer = new QFrameworkContainer();
            EnemyMode enemyMode = new EnemyMode(1200);
            QFrameworkContainer.RegisterInstance(enemyMode);
        }
    }
}