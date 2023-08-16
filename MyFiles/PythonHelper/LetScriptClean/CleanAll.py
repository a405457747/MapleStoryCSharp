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
    
    # 递归执行委托，exts是白名单，dirs是黑名单
    def TraverseDir(filepath, action, exts=['.txt', '.tex'],dirs=['biantaiwenjianjia']):
        files = os.listdir(filepath)
        for fi in files:
            fi_d = os.path.join(filepath, fi)
            if os.path.isdir(fi_d):
                dirName =fi;
                if dirName not in dirs:
                    Real.TraverseDir(fi_d, action, exts,dirs)
            else:
                fileNameFull = os.path.abspath(fi_d)
                ext = os.path.splitext(fileNameFull)[1]
                if ext in exts:
                    action(fileNameFull)


# 配置对象
set=None;
with open('set.json', 'r', encoding='utf-8') as f:
    set =json.loads(f.read());
# 删除的标志符号
DelectFlag="-DEL-"
# 要递归的目录
targetDir =set['targetDir'];
# 要保存的目录
saveDir=set['saveDir'];
# 当前选择的编程语言
curScriptSelect =set['curScriptSelect'];
# 需要过滤的文件目录列表
filterDir=set['filterDir'];#['node_modules','.vscode','.idea','__pycache__','LetScriptClean'];
#单行注释
noteLine = "";
#多行注释左边
regBlockLeft = ""
#多行注释右边
regBlockRight = ""
#根据选择的语言设置一下
if(curScriptSelect == ".js" or curScriptSelect == ".cs" or curScriptSelect == ".ts"):
    noteLine = "//"
    regBlockLeft = "/\*{1,2}"
    regBlockRight = "\*/"


# 删除的地方添加上日期哦
def addDateMessage(ListStr):
    strData=noteLine+"----------------------"+datetime.datetime.now().strftime('%Y-%m-%d %H:%M:%S')+"----------------------";
    ListStr.insert(0,strData);
    ListStr.append("\n");
    pass

# 给要生成的文件添加一点斜杠之类的，为了重名文件的考量
def fileFixed(file):
    relPath =os.path.relpath(file,targetDir);
    res= re.sub(r'[^\w.]{1,}', "_", relPath, flags=re.M);
    return res;

# 删除单行注释
def DelComment(file):
    readStr = Real.ReadFile(file)
    # 看不懂之前的注释：“这个方法还好啦不致，因为下身优化了，但是上身还在呢，并且下身并不是删除，只是变形了而已”
    regStr =r'[ \t]{0,}%s[ \t]{0,}%s.+' % (noteLine,DelectFlag) 
    findList=re.findall(regStr,readStr,flags=re.M);

    if(len(findList)>0):
        # 看不懂之前的注释：“加双清空就不支持每一行没有内容的，这也是ts默认的方式呢”
        fixedRealStr = re.sub(regStr, "", readStr, flags=re.M);
        Real.WriteFile(file, fixedRealStr)

        addDateMessage(findList);
        appendStr= '\n'.join(findList)
        appendPath=os.path.join(saveDir, fileFixed(file));

        appendStr=re.sub(DelectFlag,"",appendStr);
        Real.AppendFile(appendPath,appendStr);

# 删除多行注释，这个函数是在删除单行那个函数基础上修改的
def DelCommentBlock(file):
    readStr = Real.ReadFile(file)
    
    regStr = r'[ \t]{0,}%s[ \t]{0,}%s.+?[ \t]{0,}%s' % (regBlockLeft,DelectFlag, regBlockRight)
    findList=re.findall(regStr,readStr,flags=re.DOTALL);

    if(len(findList)>0):
        
        fixedRealStr = re.sub(regStr, "", readStr, flags=re.DOTALL);
        Real.WriteFile(file, fixedRealStr)

        addDateMessage(findList);
        appendStr= '\n'.join(findList)
        appendPath=os.path.join(saveDir, fileFixed(file));

        appendStr=re.sub(DelectFlag,"",appendStr);
        Real.AppendFile(appendPath,appendStr);

# 删除所有，对一个文件先做删除所有的单行，修改后保存再删除所有的多行
def StartDel(path):
    DelComment(path)
    DelCommentBlock(path);
    pass


if __name__ == '__main__':
    # 递归删除所有
    Real.TraverseDir(targetDir, StartDel, [curScriptSelect],filterDir);

