
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;    
  [System.Serializable]
  public class ExcelDemo
  {
    
      public string ID;
    
      public string English;
    
      public string Chinese;
    
      public double Bike;
    
      public bool Nike;
    
      public double CWK;
    
      public static List<ExcelDemo> Datas = new List<ExcelDemo>(){
      
        new ExcelDemo {
          
            
              ID = "1",
            
          
            
              English = "airww22",
            
          
            
              Chinese = "3333",
            
          
            
              Bike = 32333,
            
          
            
              Nike = true,
            
          
            
              CWK = 23,
            
          
        },
      
        new ExcelDemo {
          
            
              ID = "2",
            
          
            
              English = "SSD222222",
            
          
            
              Chinese = "777",
            
          
            
              Bike = 42323,
            
          
            
              Nike = false,
            
          
            
              CWK = 33,
            
          
        },
      
        new ExcelDemo {
          
            
              ID = "3",
            
          
            
              English = "333ss222233",
            
          
            
              Chinese = "888",
            
          
            
              Bike = 5332,
            
          
            
              Nike = true,
            
          
            
              CWK = 4,
            
          
        },
      
        new ExcelDemo {
          
            
              ID = "5",
            
          
            
              English = "33333333",
            
          
            
              Chinese = "1111",
            
          
            
              Bike = 6,
            
          
            
              Nike = true,
            
          
            
              CWK = 2,
            
          
        },
      
    };

    // CUSTOM_REGION
    
    // CUSTOM_REGION

    public static ExcelDemo Data=>Datas[0];

    public  ExcelDemo data;
    public  List<ExcelDemo> datas;

  }  
  