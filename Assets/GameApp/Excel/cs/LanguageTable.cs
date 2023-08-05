
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;    
  [System.Serializable]
  public class LanguageTable
  {
    
      public string ID;
    
      public string ChineseSimplified;
    
      public string English;
    
      public string ChineseTraditional;
    
      public static List<LanguageTable> Datas = new List<LanguageTable>(){
      
        new LanguageTable {
          
            
              ID = "1",
            
          
            
              ChineseSimplified = "我是第{}关{}",
            
          
            
              English = "i am  {} Level{}",
            
          
            
              ChineseTraditional = "我是第{}關卡{}",
            
          
        },
      
        new LanguageTable {
          
            
              ID = "2",
            
          
            
              ChineseSimplified = "",
            
          
            
              English = "",
            
          
            
              ChineseTraditional = "",
            
          
        },
      
    };

    // CUSTOM_REGION
    
    // CUSTOM_REGION

    public static LanguageTable Data=>Datas[0];

    public  LanguageTable data;
    public  List<LanguageTable> datas;

  }  
  