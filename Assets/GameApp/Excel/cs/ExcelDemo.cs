
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
    
      public long BB;
    
      public List<int> AA;
    
      public List<bool> DD;
    
      public List<string> EE;
    
      public Dictionary<string, bool> WW;
    
      public Dictionary<string, List<double>> XX;
    
      public static List<ExcelDemo> Datas = new List<ExcelDemo>(){
      
        new ExcelDemo {
          
            
              ID = "1",
            
          
            
              English = "airww22",
            
          
            
              Chinese = "3333",
            
          
            
              Bike = 32333,
            
          
            
              Nike = true,
            
          
            
              CWK = 0,
            
          
            
              BB = 3,
            
          
            
              AA =new List<int> { 1,2,3 },
            
          
            
              DD =new List<bool> { true },
            
          
            
              EE =new List<string> { "a","b" },
            
          
            
              WW =new Dictionary<string, bool> { {"a",true},{"b",false},{"c",true} },
            
          
            
              XX =new Dictionary<string, List<double>> { {"a",new List<double>{2d}},{"b",new List<double>{}},{"c",new List<double>{1d,2d,3d}} },
            
          
        },
      
        new ExcelDemo {
          
            
              ID = "2",
            
          
            
              English = "SSD222222",
            
          
            
              Chinese = "777",
            
          
            
              Bike = 42323,
            
          
            
              Nike = false,
            
          
            
              CWK = 0,
            
          
            
              BB = 0,
            
          
            
              AA =new List<int> { 2,3 },
            
          
            
              DD =new List<bool> { false },
            
          
            
              EE =new List<string> { "c" },
            
          
            
              WW =new Dictionary<string, bool> {  },
            
          
            
              XX =new Dictionary<string, List<double>> {  },
            
          
        },
      
        new ExcelDemo {
          
            
              ID = "3",
            
          
            
              English = "333ss222233",
            
          
            
              Chinese = "888",
            
          
            
              Bike = 5332,
            
          
            
              Nike = true,
            
          
            
              CWK = 0,
            
          
            
              BB = 3,
            
          
            
              AA =new List<int> { 1,5 },
            
          
            
              DD =new List<bool> { false },
            
          
            
              EE =new List<string> { "d" },
            
          
            
              WW =new Dictionary<string, bool> {  },
            
          
            
              XX =new Dictionary<string, List<double>> {  },
            
          
        },
      
        new ExcelDemo {
          
            
              ID = "5",
            
          
            
              English = "33333333",
            
          
            
              Chinese = "1111",
            
          
            
              Bike = 6,
            
          
            
              Nike = true,
            
          
            
              CWK = 0,
            
          
            
              BB = 2,
            
          
            
              AA =new List<int> { 3,2 },
            
          
            
              DD =new List<bool> { true },
            
          
            
              EE =new List<string> { "e" },
            
          
            
              WW =new Dictionary<string, bool> {  },
            
          
            
              XX =new Dictionary<string, List<double>> {  },
            
          
        },
      
    };

    // CUSTOM_REGION
    
    // CUSTOM_REGION

    public static ExcelDemo Data=>Datas[0];

    public  ExcelDemo data;
    public  List<ExcelDemo> datas;

  }  
  