
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;    
  [System.Serializable]
  public class GlobalConfig
  {
    
      public string ID;
    
      public bool isRelease;
    
      public static List<GlobalConfig> Datas = new List<GlobalConfig>(){
      
        new GlobalConfig {
          
            
              ID = "0",
            
          
            
              isRelease = false,
            
          
        },
      
    };

    // CUSTOM_REGION
    
    // CUSTOM_REGION

    public static GlobalConfig Data=>Datas[0];

    public  GlobalConfig data;
    public  List<GlobalConfig> datas;

  }  
  