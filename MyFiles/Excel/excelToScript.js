var xlsx = require('node-xlsx');
var fs = require('fs');
let ejs = require("ejs");
const path = require('path');


//生成模板的路径 
let generateTemplatePath = "../../Assets/GameApp/Scripts/Excel";
//监听excel路径位置
let readExcelPath = "./";


//文件类型
let fileTypeEnum = { empty: -1, cs: 0, js: 1, lua: 2, php: 3, java: 4, python: 5, json: 6, csv: 7,xml:8 };
//类的字段类型
let fieldTypeEnum = { empty: -1, long: 0, bool: 1, dbl: 2, str: 3, obj: 4 };
/*下面3个变量对多任务有影响吗？*/
let textMatrix = null;//处理过后的文本矩阵,包括头和内容
let matrixHeadDic = null;//textMatrix之后的头信息字典。key是字段名，val是obj，需要进一步操作。
let className = null;//excel的文件名当成类名。


//获取枚举的key也就是字符串
function getEnumStr(enumType, enumInt) {
  for (let k in enumType) {
    if (enumType[k] === enumInt) {
      return k;
    }
  }
  return undefined;
}

//主要函数main
function readExcelGetTextMatrix(excelFile) {
  let originData = xlsx.parse(excelFile)[0].data;

  let copyData = [...originData];//这也是复制

  let headLength = getHeadLength(copyData);

  FillAndTrimStr(copyData, headLength);
  FilterGrid(copyData);
  WithAEmptyRowInCenter(copyData);

  textMatrix = copyData;
  InitMatrixHeadDic(textMatrix);
  setClassNames(excelFile);
  InitFileTypeObject();
}

//写入各种类型的文件
function InitFileTypeObject() {
  new CsFileType().writeScript(fileTypeEnum.cs);
  new LuaFileType().writeScript(fileTypeEnum.lua);
  new JsonFileType().writeScript(fileTypeEnum.json);
}

function InitMatrixHeadDic(textMatrix) {
  let originHead = textMatrix[0];
  let dic = new Map();

  let getDicValObj = function (idx, typeStr, defaultStr) {
    return {
      idx: idx,
      typeStr: typeStr,
      defaultStr: defaultStr,
    }
  }

  //数组长度不一致的时候，填充""使拥有一致长度，这样方便设置值
  let fillLittleItemArr = function (littleItemArr) {
    let maxLength = 3;//类中字段顺序为：1名字2类型3默认值
    while (littleItemArr.length !== maxLength) {
      littleItemArr.push("");
    }
  }

  for (let i = 0; i < originHead.length; i++) {
    let item = originHead[i];
    let littleItemArr = strToTrimArray(item, "|");
    fillLittleItemArr(littleItemArr);//统一一下长度

    if (dic.get(littleItemArr[0]) === undefined) {
      dic.set(littleItemArr[0], getDicValObj(i, littleItemArr[1], littleItemArr[2]));
    } else {
      throw new Error("错误，头的字段名字重复了。");
    }
  }

  matrixHeadDic = dic;
}

//设置类的名字，就是Excel文件名
function setClassNames(excelFile) {
  let strArr = strToTrimArray(excelFile, ".");
  let excelName = strArr[0];
  className = excelName;
}

//先变成字符串类型，然后填充了undefiend和处理了trim之后的格子。
function FillAndTrimStr(copyData, headLength) {
  for (let item of copyData) {
    item.length = headLength;//修剪右边突出的字段,不优雅但是简单

    for (let i = 0; i < item.length; i++) {
      let cellItem = item[i];
      if (cellItem !== undefined) cellItem = cellItem.toString();//这个里面居然有int类型的数字
      
      if (isEmptyCell(cellItem)) {
        cellItem = "";
      }
      cellItem = cellItem.trim();

      item[i] = cellItem;
    }
  }
}

/*从底部到顶部，过滤掉全是空单元格的行。*/
function FilterGrid(copyData) {
  let tailEmptyRowCount = 0;
  for (let i = copyData.length - 1; i >= 0; i--) {
    let arrItem = copyData[i];
    if (arrItem.every(item => item === "")) {
      tailEmptyRowCount += 1;
    } else {
      break;//遇到一个不全是空白的就提前跳出。
    }
  }
  copyData.length = copyData.length - tailEmptyRowCount;//这样裁剪尾巴全是空单元格的行。
}

/*校验过滤后的copyData，它中间部分是不是有全是全是空单元格的行。*/
function WithAEmptyRowInCenter(copyData) {
  for (let arr_item of copyData) {
    let isEmptyRow = arr_item.every(item => item === "");
    if (isEmptyRow) {
      throw new Error("错误，出现了空的一行，请至少填写一个非空白的内容吧");
    }
  }
}

function getHeadLength(copyData) {//这个方法虽然在trim之前，但是不可能是解析成非字符串。
  let head = copyData[0];

  for (let item of head) {
    if (isEmptyCell(item)) {
      throw new Error("错误，头出现了空格子");
    }
  }

  return head.length;
}

/*是否是空格子*/
function isEmptyCell(item) {
  return (item === undefined) || (item.trim() === "");
}

//数组trim一下
function strToTrimArray(str, interval) {
  let res = str.split(interval);
  for (let i = 0; i < res.length; i++) {
    let item = res[i].trim();
    res[i] = item;
  }
  return res;
}

//监听excel，如果有文件改变就自动执行处理脚本
function watchExcelChange() {
  const opt = {
    persistent: true, // persistent <boolean> 指示如果文件已正被监视，进程是否应继续运行。默认值: true。
    recursive: false// recursive <boolean> 指示应该监视所有子目录，还是仅监视当前目录。 这适用于监视目录时，并且仅适用于受支持的平台（参见注意事项）。默认值: false。
  }
  fs.watch(readExcelPath, opt, (eventType, filename) => {
    if (filename) {
      if (eventType === "change" && filename.endsWith(".xlsx") && (!filename.includes("$"))) {
       
        readExcelGetTextMatrix(filename);
        console.log(`数据写入成功，改变的excel为: ${filename}！`);
      }
    }
  });
}

function readFile(path) {
  return fs.readFileSync(path, 'utf8');
}

function writeFile(path, content) {
  fs.writeFileSync(path, content, 'utf8');
}

class FileType {
  constructor() {
    //这个保留字段元数据信息
    this.fieldMessages = new Array(matrixHeadDic.size);
    for (let [k, v] of matrixHeadDic) {
      let idx = v.idx;
      let enumFieldType = this.getFieldTypeByTypeStr(v.typeStr);//这个是解析过后的枚举
      //如果是obj类型的字段就特殊处理，要加new {}
      let isEnumFieldTypeObj = (enumFieldType === fieldTypeEnum.obj);
      this.fieldMessages[idx] = { fieldType: this.getFieldType(enumFieldType, v.typeStr), fieldName: k, enumFieldType: enumFieldType, defaultStr: v.defaultStr, isEnumFieldTypeObj: isEnumFieldTypeObj };
    }

    //这个保留字段实际值信息
    this.fieldValueObjs = new Array(textMatrix.length - 1);//为啥是-1，因为textMatrix是包含头的咱们只要内容。
    for (let i = 0; i < this.fieldValueObjs.length; i++) {
      this.fieldValueObjs[i] = this.getFieldValueObj(i);
    }

  }

  getFieldTypeByTypeStr(typeStr) {
    //根据typeStr得到type
    let curTypeEnum = fieldTypeEnum.empty;

    //字段类型是obj吗
    let isObjTypeEnum = typeStr.includes("<") || typeStr.includes("[");

    if (isObjTypeEnum) {
      curTypeEnum = fieldTypeEnum.obj;
    } else {
      for (let key in fieldTypeEnum) {
        if ((key === typeStr)) {
          curTypeEnum = fieldTypeEnum[key];
          break;
        }
      }
    }

    if (curTypeEnum === fieldTypeEnum.empty) {
      throw new Error("错误，有字段类型解析失败，它是 " + typeStr);
    } else {
      return curTypeEnum;
    }
  }

  getFieldType(fieldTypeEnumValue, typeStr) {
    switch (fieldTypeEnumValue) {
      case fieldTypeEnum.long:
        return "long";
      case fieldTypeEnum.bool:
        return "bool";
      case fieldTypeEnum.dbl:
        return "double";
      case fieldTypeEnum.str:
        return "string";
      case fieldTypeEnum.obj:
        return typeStr;
    }

    throw new Error("错误，能匹配字段类型，但是取值方法还没有实现呢。")
  }


  //根据类型获取正则校验的字符串
  RegularStrByfieldType(fieldTypeEnumValue) {
    switch (fieldTypeEnumValue) {
      case fieldTypeEnum.long:
        return /^-?[1-9]\d*$|^0$/;
      case fieldTypeEnum.bool:
        return /^[01]{1}$/;
      case fieldTypeEnum.dbl:
        return /^-?\d*\.\d+$|^0$|^-?[1-9]\d*$/;
      case fieldTypeEnum.str:
        return /.*/;
      case fieldTypeEnum.obj:
        return /.*/;
    }

    throw new Error("错误，能匹配字段类型，但是取值方法还没有实现呢。")
  }


  //脚本中的正确值设置，比如字符串要加“”，0改成false,1改成true之类的,dbl变成double,说实在的做这些缩写和默认值都是方便策划。
  ScriptRightValueByfieldType(fieldTypeEnumValue, finalTextMatrixItem_val) {
    switch (fieldTypeEnumValue) {
      case fieldTypeEnum.long:
        return finalTextMatrixItem_val;
      case fieldTypeEnum.bool:
        return finalTextMatrixItem_val === "0" ? "false" : "true";
      case fieldTypeEnum.dbl:
        return finalTextMatrixItem_val;
      case fieldTypeEnum.str:
        return `"${finalTextMatrixItem_val}"`;//这个字符串要加冒号的。
      case fieldTypeEnum.obj:
        return finalTextMatrixItem_val;
    }

    throw new Error("错误，能匹配字段类型，但是取值方法还没有实现呢。")
  }


  //当默认值是空字符串时候，我手动帮它造一个格子里面的值，注意是格子里面的值哦，之后还要解析的呢，所以bool我用0或者1
  DefaultStrEmptyFixed(fieldTypeEnumValue) {
    switch (fieldTypeEnumValue) {
      case fieldTypeEnum.long:
        return "0";
      case fieldTypeEnum.bool:
        return "0";//0默认为false
      case fieldTypeEnum.dbl:
        return "0";
      case fieldTypeEnum.str:
        return "";
      case fieldTypeEnum.obj:
        return "";
    }

    throw new Error("错误，能匹配字段类型，但是取值方法还没有实现呢。DefaultStrEmptyFixed")
  }

  getFieldValueObj(idx) {
    let textMatrixItem = textMatrix[idx + 1];//idx从0开始，我们只要内容所以要加1.


    let scriptRightValueArr = [];
    for (let i = 0; i < textMatrixItem.length; i++) {
      let textMatrixItem_val = textMatrixItem[i];
      let textMatrixItem_type = this.fieldMessages[i].enumFieldType;

      //对默认值统一处理一下，不是最后，scriptRightValue才是最后要弄的值。
      let finalTextMatrixItem_val = textMatrixItem_val;
      if (textMatrixItem_val === "") {//是空字符串说明，这里要采用了默认值了。

        //如果默认值你没有写，我要手动帮你造一个值，没办法我对策划太好了。
        if (this.fieldMessages[i].defaultStr === "") {
          finalTextMatrixItem_val = this.DefaultStrEmptyFixed(textMatrixItem_type);
        } else {
          finalTextMatrixItem_val = this.fieldMessages[i].defaultStr;
        }
      }

      //用正则字段校验字段值的，反正obj其中的子元素我就不校验了，因为麻烦的说。
      let regularObj = this.RegularStrByfieldType(textMatrixItem_type);
      if (regularObj.test(finalTextMatrixItem_val)) {
      } else {
        console.warn("警告请必须修正哦，因为正则校验失败，有个字段是错误的格式，它是 " + finalTextMatrixItem_val);
      }

      let scriptRightValue = this.ScriptRightValueByfieldType(textMatrixItem_type, finalTextMatrixItem_val);
      scriptRightValueArr[i] = scriptRightValue;
    }

    return scriptRightValueArr;
  }

  //获取类的模板,暂时把数据都做在一起这样清爽一些
  getEjsTemplate(customStr = "") {
    return "";
  }

  getCustomStr(filePath) {
    let res=null;

    let fullStr = readFile(filePath);
    let regexModel = /^[\t\s]{1,}\/\/ CUSTOM_REGION(.*)[\t\s]{1,}\/\/ CUSTOM_REGION$/ms; //todo warn CUSTOM_REGION  两个地方有重复。

    let execArr = regexModel.exec(fullStr)

    if ((execArr!=null)&& execArr.length > 0) {
      res= execArr[1];
    } else {
      res="";
      console.warn("错误必须要处理：你这属于正则捕获不成功，一定要改改")
    }

    return res. trim();
  }

  writeScript(curFileType) {

    let filePath = path.resolve(generateTemplatePath, getEnumStr(fileTypeEnum, curFileType)+"/"+className + "." + getEnumStr(fileTypeEnum, curFileType));

    //判断这个文件是否存在
    let fileExist = fs.existsSync(filePath);
    let customStr = fileExist ? this.getCustomStr(filePath) : "";//写入数据，先读取文件并找到自定义的代码字符串。
    let strContent = this.getEjsTemplate(customStr);
    writeFile(filePath, strContent);
  }

}

class CsFileType extends FileType {

  getEjsTemplate(customStr = "") {
    let str = `
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;    
  [System.Serializable]
  public class <%=className%>
  {
    <% for(let i = 0;i < fieldMessages.length;i++){ %>
      public <%- fieldMessages[i].fieldType %> <%= fieldMessages[i].fieldName %>;
    <% } %>
      public static List<<%=className%>> Datas = new List<<%=className%>>(){
      <% for(let i = 0;i < fieldValueObjs.length;i++){ %>
        new <%=className%> {
          <% for(let j = 0;j < fieldMessages.length;j++){ %>
            <% if(fieldMessages[j].isEnumFieldTypeObj) { %>
              <%= fieldMessages[j].fieldName %> =new <%- fieldMessages[j].fieldType %> { <%- fieldValueObjs[i][j] %> },
            <%  }else { %>
              <%= fieldMessages[j].fieldName %> = <%- fieldValueObjs[i][j] %>,
            <% } %>
          <% } %>
        },
      <% } %>
    };

    <%=RegionComment%>
    <%-customStr%>
    <%=RegionComment%>

    public static <%=className%> Data=>Datas[0];

    public  <%=className%> data;
    public  List<<%=className%>> datas;

  }  
  `;
    let fieldMessages = this.fieldMessages;
    let fieldValueObjs = this.fieldValueObjs;

    return ejs.render(str, { className: className, fieldMessages: fieldMessages, fieldValueObjs: fieldValueObjs, RegionComment: "// CUSTOM_REGION", customStr: customStr });
  }
}

class LuaFileType extends FileType {

  getEjsTemplate(customStr = "") {
    let str = `
    <%=className%>={
      <% for(let i = 0;i <1;i++){ %>
        
        <% for(let j = 0;j < fieldMessages.length;j++){ %>
          <% if(fieldMessages[j].isEnumFieldTypeObj) { %>
            <%= fieldMessages[j].fieldName %> =new <%- fieldMessages[j].fieldType %> { <%- fieldValueObjs[i][j] %> },
          <%  }else { %>
            <%= fieldMessages[j].fieldName %> = <%- fieldValueObjs[i][j] %>,
          <% } %>
        <% } %>
      
      <% } %>


      datas={


        
          <% for(let i = 0;i < fieldValueObjs.length;i++){ %>
            {
              <% for(let j = 0;j < fieldMessages.length;j++){ %>
                <% if(fieldMessages[j].isEnumFieldTypeObj) { %>
                  <%= fieldMessages[j].fieldName %> =new <%- fieldMessages[j].fieldType %> { <%- fieldValueObjs[i][j] %> },
                <%  }else { %>
                  <%= fieldMessages[j].fieldName %> = <%- fieldValueObjs[i][j] %>,
                <% } %>
              <% } %>
            },
          <% } %>
        }
    };
    <%=className%>.data =<%=className%>.datas[1];   
  `;
    let fieldMessages = this.fieldMessages;
    let fieldValueObjs = this.fieldValueObjs;

    return ejs.render(str, { className: className, fieldMessages: fieldMessages, fieldValueObjs: fieldValueObjs, RegionComment: "// CUSTOM_REGION", customStr: customStr });
  }
}

class JsonFileType extends FileType {

  getEjsTemplate(customStr = "") {
    let str = `
    {
      <% for(let i = 0;i <1;i++){ %>
        
          <% for(let j = 0;j < fieldMessages.length;j++){ %>
            <% if(fieldMessages[j].isEnumFieldTypeObj) { %>
              <%= fieldMessages[j].fieldName %> =new <%- fieldMessages[j].fieldType %> { <%- fieldValueObjs[i][j] %> },
            <%  }else { %>
              "<%= fieldMessages[j].fieldName %>" : <%- fieldValueObjs[i][j] %>,
            <% } %>
          <% } %>
        
        <% } %>

        


      "datas":[
        
          <% for(let i = 0;i < fieldValueObjs.length;i++){ %>
            {
              <% for(let j = 0;j < fieldMessages.length;j++){ %>
                <% if(fieldMessages[j].isEnumFieldTypeObj) { %>
                  <%= fieldMessages[j].fieldName %> =new <%- fieldMessages[j].fieldType %> { <%- fieldValueObjs[i][j] %> },
                <%  }else { %>
                  "<%= fieldMessages[j].fieldName %>" : <%- fieldValueObjs[i][j] %>
                    <% if(j!=fieldMessages.length-1) { %>
                      ,
                    <%  }else { %>
                    <% } %>
                <% } %>
              <% } %>
            }
            <% if(i!=fieldValueObjs.length-1) { %>
              ,
            <%  }else { %>
            <% } %>
          <% } %>
          ],
      "data":
      <% for(let i = 0;i <1;i++){ %>
        {
          <% for(let j = 0;j < fieldMessages.length;j++){ %>
            <% if(fieldMessages[j].isEnumFieldTypeObj) { %>
              <%= fieldMessages[j].fieldName %> =new <%- fieldMessages[j].fieldType %> { <%- fieldValueObjs[i][j] %> },
            <%  }else { %>
              "<%= fieldMessages[j].fieldName %>" : <%- fieldValueObjs[i][j] %>
                <% if(j!=fieldMessages.length-1) { %>
                  ,
                <%  }else { %>
                <% } %>
            <% } %>
          <% } %>
        }
        <% } %>
    }
  `;
    let fieldMessages = this.fieldMessages;
    let fieldValueObjs = this.fieldValueObjs;

    return ejs.render(str, { className: className, fieldMessages: fieldMessages, fieldValueObjs: fieldValueObjs, RegionComment: "// CUSTOM_REGION", customStr: customStr });
  }
}

function main() {
  try {
    watchExcelChange();
  } catch (error) {
    console.log("捕获到错误，错误信息为："+error)
  }
}

main();
