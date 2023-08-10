
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
    
      public double Nihao;
    
      public bool buyao;
    
      public static List<LanguageTable> Datas = new List<LanguageTable>(){
      
        new LanguageTable {
          
            
              ID = "1",
            
          
            
              ChineseSimplified = "中国222",
            
          
            
              English = "chinese222",
            
          
            
              ChineseTraditional = "cccww222",
            
          
            
              Nihao = 231,
            
          
            
              buyao = true,
            
          
        },
      
        new LanguageTable {
          
            
              ID = "2",
            
          
            
              ChineseSimplified = "ss",
            
          
            
              English = "dd",
            
          
            
              ChineseTraditional = "0",
            
          
            
              Nihao = 0,
            
          
            
              buyao = true,
            
          
        },
      
    };

    // CUSTOM_REGION
    void sssk()
    {

    }
    void skkw()
    {
       //
    }
    // CUSTOM_REGION

    public static LanguageTable Data=>Datas[0];

    public  LanguageTable data;
    public  List<LanguageTable> datas;

  }  
  