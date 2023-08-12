
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
            
          
            
              ChineseSimplified = "中国222",
            
          
            
              English = "chinese222",
            
          
            
              ChineseTraditional = "cccww222",
            
          
        },
      
    };

    // CUSTOM_REGION

    // CUSTOM_REGION

    public static LanguageTable Data=>Datas[0];

    public  LanguageTable data;
    public  List<LanguageTable> datas;

  }  
  