
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
    
      public Dictionary<string, List<List<double>>> NNNK;
    
      public static List<ExcelDemo> Datas = new List<ExcelDemo>(){
      
        new ExcelDemo {
          
            
              ID = "1",
            
          
            
              English = "333hell",
            
          
            
              Chinese = "33333333",
            
          
            
              Bike = 2,
            
          
            
              Nike = false,
            
          
            
              CWK = 0,
            
          
            
              BB = 555,
            
          
            
              AA =new List<int> { 1,2,3 },
            
          
            
              DD =new List<bool> { true },
            
          
            
              EE =new List<string> { "a","b" },
            
          
            
              WW =new Dictionary<string, bool> { {"k232",true},{"b",false},{"c",true} },
            
          
            
              XX =new Dictionary<string, List<double>> { {"a",new List<double>{2d}},{"b",new List<double>{}},{"c",new List<double>{1d,2d,3d}} },
            
          
            
              NNNK =new Dictionary<string, List<List<double>>> {  },
            
          
        },
      
        new ExcelDemo {
          
            
              ID = "2",
            
          
            
              English = "S444",
            
          
            
              Chinese = "77733332",
            
          
            
              Bike = 66,
            
          
            
              Nike = true,
            
          
            
              CWK = 0,
            
          
            
              BB = 555,
            
          
            
              AA =new List<int> { 2,3 },
            
          
            
              DD =new List<bool> { false },
            
          
            
              EE =new List<string> { "c" },
            
          
            
              WW =new Dictionary<string, bool> { {"a332",true},{"b",false},{"c",true} },
            
          
            
              XX =new Dictionary<string, List<double>> { {"a",new List<double>{2d}},{"b",new List<double>{}},{"c",new List<double>{1d,2d,3d}} },
            
          
            
              NNNK =new Dictionary<string, List<List<double>>> {  },
            
          
        },
      
        new ExcelDemo {
          
            
              ID = "3",
            
          
            
              English = "5555",
            
          
            
              Chinese = "88822",
            
          
            
              Bike = 66,
            
          
            
              Nike = true,
            
          
            
              CWK = 0,
            
          
            
              BB = 555,
            
          
            
              AA =new List<int> { 1,5 },
            
          
            
              DD =new List<bool> { false },
            
          
            
              EE =new List<string> { "d" },
            
          
            
              WW =new Dictionary<string, bool> { {"a3333",true},{"b",false},{"c",true} },
            
          
            
              XX =new Dictionary<string, List<double>> { {"a",new List<double>{2d}},{"b",new List<double>{}},{"c",new List<double>{1d,2d,3d}} },
            
          
            
              NNNK =new Dictionary<string, List<List<double>>> {  },
            
          
        },
      
        new ExcelDemo {
          
            
              ID = "5",
            
          
            
              English = "33333333",
            
          
            
              Chinese = "111133",
            
          
            
              Bike = 53,
            
          
            
              Nike = false,
            
          
            
              CWK = 0,
            
          
            
              BB = 2,
            
          
            
              AA =new List<int> { 3,2 },
            
          
            
              DD =new List<bool> { true },
            
          
            
              EE =new List<string> { "e" },
            
          
            
              WW =new Dictionary<string, bool> { {"a",true},{"b",false},{"c",true} },
            
          
            
              XX =new Dictionary<string, List<double>> { {"a",new List<double>{2d}},{"b",new List<double>{}},{"c",new List<double>{1d,2d,3d}} },
            
          
            
              NNNK =new Dictionary<string, List<List<double>>> {  },
            
          
        },
      
    };

    // CUSTOM_REGION
    public void DDE()
    {
          
          
    }

    public void KK()
    {
          
    }
    // CUSTOM_REGION

    public static ExcelDemo Data=>Datas[0];

    public  ExcelDemo data;
    public  List<ExcelDemo> datas;

  }  
  