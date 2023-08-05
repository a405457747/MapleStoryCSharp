
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;    
  [System.Serializable]
  public class AppVersion
  {
    
      public string ID;
    
      public static List<AppVersion> Datas = new List<AppVersion>(){
      
        new AppVersion {
          
            
              ID = "0",
            
          
        },
      
    };

    // CUSTOM_REGION
    
    // CUSTOM_REGION

    public static AppVersion Data=>Datas[0];

    public  AppVersion data;
    public  List<AppVersion> datas;

  }  
  