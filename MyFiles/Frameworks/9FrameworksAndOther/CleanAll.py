#!/usr/bin/env python3
# -*- coding: UTF-8 -*-
from email import header
from itertools import count
from math import fabs
import os
import re
import csv
import string
from sys import flags
import time
from traceback import print_tb
from enum import Enum
import datetime
import json;



# todo c#美化 bug
# todo 自动设置格式非bom的utf8
# todo 给python文本也用上,多语言脚本也给用上啊
# todo 添加/usr/bin这种头文件



set=None;
with open('set.json', 'r', encoding='utf-8') as f:
    set =json.loads(f.read());
targetDir =set['targetDir']; # "./tt"
saveDir=set['saveDir']; # "./CodeRepository"
curScriptSelect =set['curScriptSelect']; # ".js"
filterDir=set['filterDir'];#  ['node_modules','.vscode','.idea','__pycache__','LetScriptClean'];

# key是文件名，val是List<Dict>
#saveDict ={};


noteLine = ""
noteBlockLeft = ""
noteBlockRight = ""
blockRightFind = ""
regBlockLeft = ""
regBlockRight = ""
DelectFlag="-DEL-"
if(curScriptSelect == ".js" or curScriptSelect == ".cs" or curScriptSelect == ".ts"):
    noteLine = "//"
    noteBlockLeft = "/*"
    noteBlockRight = "*/"
    regBlockLeft = "/\*{1,2}"
    regBlockRight = "\*/"
elif(curScriptSelect==".py"):
    noteLine = "#"
    noteBlockLeft = "'''"
    noteBlockRight = "'''"
    regBlockLeft = "/\*{1,2}"
    regBlockRight = "\*/"
blockRightFind = " "+noteBlockRight

class Real(object):
    def ReadCSV(file):
        with open(file, encoding="utf-8") as f:
            f_csv = csv.reader(f)
            bigList = []
            headers = next(f_csv)
            bigList.append(headers)
            for row in f_csv:
                bigList.append(row)

            return bigList

    def WriteCSV(file, head, contents):
        with open(file, "w", newline='', encoding="utf-8") as f:
            writer = csv.writer(f)
            writer.writerow(head)
            row = contents
            for r in row:
                writer.writerow(r)

    def ParseSelfTag(tag, content):
        mode = r"\【{0}(.+?)\】".format(tag)
        m = re.search(mode, content)
        if m is not None:
            return m.group(1)
        return None

    def ParseListSelfTag(tag, content):
        mode = r"\【{0}(.+?)\】".format(tag)
        m = re.findall(mode, content)
        return m

    def ParseMLTag(tag, content):
        mode = r"<{0}>(.+)</{0}>".format(tag)
        m = re.search(mode, content)
        if m is not None:
            return m.group(1)
        return None

    def AppendFile(file, content):
        with open(file, "a", encoding="utf-8") as f:
            f.write(content)

    def WriteFile(file, content):
        with open(file, "w", encoding="utf-8") as f:
            f.write(content)

    def AppendFile(file,content):
         with open(file, "a", encoding="utf-8") as f:
            f.write(content)
       
    def ReadFile(file) -> string:
        with open(file, 'r', encoding='utf-8') as f:
            return f.read()
    
    # 文件是白名单 文件夹是黑名单
    def TraverseDir(filepath, action, exts=['.txt', '.tex'],dirs=['biantaiwenjianjia']):
        files = os.listdir(filepath)
        for fi in files:
            fi_d = os.path.join(filepath, fi)
            if os.path.isdir(fi_d):
                dirName =fi;
                #print(fi);
                if dirName not in dirs:
                    Real.TraverseDir(fi_d, action, exts,dirs)
            else:

                fileNameFull = os.path.abspath(fi_d)
                ext = os.path.splitext(fileNameFull)[1]
                if ext in exts:
                    action(fileNameFull)

####################################################
def IsEmptyLine(line):
    return len(line.strip()) == 0


def BuitifulComment(file):  # 漂亮的行注释
    # (?<=\S)[ \t]{0,}(//)[ \t]{0,}(.+)
    readStr = Real.ReadFile(file)
    # 这里单清空很好啊，因为如果是嵌套的，下面的块注释自己会帮我清空呢
    regStr = r'[ \t]{0,}(%s)[ \t]{0,}(.+)' % (noteLine)
    # 不用贪婪模式是因为不支持有一行出现里两个// 实在需要就用/**/
    fixedRealStr = re.sub(regStr, r"\1 \2", readStr, flags=re.M)
    Real.WriteFile(file, fixedRealStr)
    #print("run success")


def BuitifulCommentBlock(file):
    readStr = Real.ReadFile(file)
    # 这个方法还好啦不致，因为下身优化了，但是上身还在呢，并且下身并不是删除，只是变形了而已
    regStr = r'[ \t]{0,}(%s)[ \t]{0,}(.+?)[ \t]{0,}(%s)' % (regBlockLeft,
                                                            regBlockRight)
    # 加双清空就不支持每一行没有内容的，这也是ts默认的方式呢
    fixedRealStr = re.sub(regStr, r"\1 \2 \3", readStr, flags=re.DOTALL)
    Real.WriteFile(file, fixedRealStr)


def IsCommentLine(line):
    return line.startswith(noteLine)

# 这个函数本来就不完美算了吧暂时先这样了


def IsCommentLineCommon(line):
    fixedLine = line.lstrip()
    bo1 = fixedLine.startswith(noteLine)
    bo2 = fixedLine.startswith(noteBlockLeft)
    bo3 = noteBlockRight in fixedLine  # fixed但是还是在呢

    # 以星开头默认是块注释的一部分,并且*屁股后面的我不一起移动了，因为我的向下查找是考虑用块注释来注释代码的所以没必要也没精力搞这个。
    bo4= fixedLine.startswith("*");
    return bo1 or bo2 or bo3 or bo4;


def fillGetWanted(item):
    res =(not IsCommentLineCommon(item)) and (not IsEmptyLine(item))
    return res;


def fillGetWantedBlock(item):
    res= (not IsCommentLineCommon(item)) and (not IsEmptyLine(item))
    return res;


def empty_str(line):
    res = ""
    for item in line:
        if(item == '\t'):
            res += '\t'
        elif(item == " "):
            res += " "
        else:
            break
    return res


def fillGetCount(arr, arrIndex):
    wantLine = ""
    arrTmpIndex = arrIndex+1

    while(arrTmpIndex < len(arr)):
        wantLine = arr[arrTmpIndex]
        if(fillGetWanted(wantLine)):
            break
        else:
            arrTmpIndex += 1
    return empty_str(wantLine)


def fillGetCountBlock(arr, arrIndex):
    wantLine = ""
    arrTmpIndex = arrIndex+1

    while(arrTmpIndex < len(arr)):
        wantLine = arr[arrTmpIndex]
        if(fillGetWantedBlock(wantLine)):
            break
        else:
            arrTmpIndex += 1
    return empty_str(wantLine)


def fillStr(item, arr, arrIndex):
    partStr = fillGetCount(arr, arrIndex)
    if(curScriptSelect==".cs"):partStr+=partStr;
    #print("item","partStr",item,":"+partStr+":");
    res = partStr + item
    return res


def fillStrBlock(item, arr, arrIndex):
    partStr = fillGetCountBlock(arr, arrIndex)
    if(curScriptSelect==".cs"):partStr+=partStr;
    res = partStr + item
    return res


def fillGetCountBlockLeft(arr, arrIndex):
    wantLine = ""
    arrTmpIndex = arrIndex-1  # 向上查找

    while(arrTmpIndex >= 0):
        wantLine = arr[arrTmpIndex]
        if(noteBlockLeft in wantLine):
            break
        else:
            arrTmpIndex -= 1
    return empty_str(wantLine)


def fillstrBlockLeft(item, arr, arrIndex):
    partStr = fillGetCountBlockLeft(arr, arrIndex)
    res = partStr + item
    return res


def IsCommentLineBlock(line):
    return line.startswith(noteBlockLeft)


def IsCommentLineBlockLeft(line):
    return line.startswith(blockRightFind)


def BuitifulCommentBlock2(file):
    readStr = Real.ReadFile(file)
    arr = readStr.split('\n')
    tempList = []
    arrIndex = 0
    for item in arr:
        fixedItem = item
        if(IsCommentLineBlock(item)):  # 全部或者left
            fixedItem = fillStrBlock(item, arr, arrIndex)
            # print(fixedItem);
        tempList.append(fixedItem)
        arrIndex = arrIndex+1

    fixedRealStr = '\n'.join(tempList)
    Real.WriteFile(file, fixedRealStr)


def BuitifulCommentBlock3(file):
    readStr = Real.ReadFile(file)
    arr = readStr.split('\n')
    tempList = []
    arrIndex = 0
    for item in arr:
        fixedItem = item
        if(IsCommentLineBlockLeft(item)):
            fixedItem = fillstrBlockLeft(item, arr, arrIndex)
        tempList.append(fixedItem)
        arrIndex = arrIndex+1
    fixedRealStr = '\n'.join(tempList)
    Real.WriteFile(file, fixedRealStr)


def BuitifulComment2(file):
    readStr = Real.ReadFile(file)
    arr = readStr.split('\n')
    tempList = []
    arrIndex = 0
    for item in arr:
        fixedItem = item
        if(IsCommentLine(item)):
            fixedItem = fillStr(item, arr, arrIndex)
            print("fixedItem",fixedItem);
        tempList.append(""+fixedItem)
        arrIndex = arrIndex+1

    fixedRealStr = '\n'.join(tempList)
    Real.WriteFile(file, fixedRealStr)
    #print("run success2");


def StartBitiful(path):
    BuitifulComment(path)
    BuitifulComment2(path)
    BuitifulCommentBlock(path)
    BuitifulCommentBlock2(path)
    BuitifulCommentBlock3(path)
################################

def RemoveEmptyLine(file):
    readStr = Real.ReadFile(file)
    arr = readStr.split('\n')
    #print("len is"+str( len(arr)));
    # print(type(arr));
    counter = 0
    tempList = []
    for item in arr:
        isEmptyLine = IsEmptyLine(item)
        if(isEmptyLine == True):
            if(counter == 0):
                tempList.append(item)
                counter = counter+1
                # print(item);
        else:
            counter = 0
            tempList.append(item)
            # print(item);
        # print(item)

    #fixedRealStr =re.sub(r'^[\n]{2,}',"\n",readStr,flags=re.M);
    fixedRealStr = '\n'.join(tempList)
    Real.WriteFile(file, fixedRealStr)
    #print("run success")
####################################



def addDateMessage(ListStr):
    strData=noteLine+"----------------------"+datetime.datetime.now().strftime('%Y-%m-%d %H:%M:%S')+"----------------------";
    ListStr.insert(0,strData);
    ListStr.append("\n");
    pass


def fileFixed(file):
    relPath =os.path.relpath(file,targetDir);
    #print(relPath);
    res= re.sub(r'[^\w.]{1,}', "_", relPath, flags=re.M);
    return res;

# 这个方法虽然方便但是不要嵌套哦，嵌套就不优雅了，虽然也没啥问题
def DelComment(file):
    readStr = Real.ReadFile(file)
    # 这个方法还好啦不致，因为下身优化了，但是上身还在呢，并且下身并不是删除，只是变形了而已
    regStr =r'[ \t]{0,}%s[ \t]{0,}%s.+' % (noteLine,DelectFlag) 
    findList=re.findall(regStr,readStr,flags=re.M);

    if(len(findList)>0):
        # 加双清空就不支持每一行没有内容的，这也是ts默认的方式呢
        fixedRealStr = re.sub(regStr, "", readStr, flags=re.M);
        Real.WriteFile(file, fixedRealStr)

        addDateMessage(findList);
        appendStr= '\n'.join(findList)
        appendPath=os.path.join(saveDir, fileFixed(file));

        appendStr=re.sub(DelectFlag,"",appendStr);
        Real.AppendFile(appendPath,appendStr);

def DelCommentBlock(file):
    readStr = Real.ReadFile(file)
    # 这个方法还好啦不致，因为下身优化了，但是上身还在呢，并且下身并不是删除，只是变形了而已
    regStr = r'[ \t]{0,}%s[ \t]{0,}%s.+?[ \t]{0,}%s' % (regBlockLeft,DelectFlag, regBlockRight)
    findList=re.findall(regStr,readStr,flags=re.DOTALL);

    if(len(findList)>0):
        # 加双清空就不支持每一行没有内容的，这也是ts默认的方式呢
        fixedRealStr = re.sub(regStr, "", readStr, flags=re.DOTALL);
        Real.WriteFile(file, fixedRealStr)

        addDateMessage(findList);
        appendStr= '\n'.join(findList)
        appendPath=os.path.join(saveDir, fileFixed(file));

        appendStr=re.sub(DelectFlag,"",appendStr);
        Real.AppendFile(appendPath,appendStr);

def StartDel(path):

    DelComment(path)
    DelCommentBlock(path);
    pass


#删评论
Real.TraverseDir(targetDir, StartDel, [curScriptSelect],filterDir);
#移除空行
Real.TraverseDir(targetDir, RemoveEmptyLine, [ curScriptSelect],filterDir);
# 美化注释
Real.TraverseDir(targetDir, StartBitiful, [ curScriptSelect],filterDir);


