#!/usr/bin/env python3
# -*- coding: UTF-8 -*-
from io import StringIO
import os;
import re;
import csv
from xml.dom.minicompat import NodeList;

def WriteCSV(file,head,contents):
    with open(file, "w", newline='',encoding="utf-8") as f:
        writer = csv.writer(f)
        writer.writerow(head);
        row =contents;
        for r in row:
            writer.writerow(r)

def ReadCSV(file):
    with open(file,encoding="utf-8") as f:
        f_csv =csv.reader(f);
        bigList=[];
        headers =next(f_csv);
        bigList.append(headers);
        for row in f_csv:
            bigList.append(row);

        return bigList;



bigList= ReadCSV("todo.txt");

tmpBigList =bigList[1:];
lineNumDic ={};

lineNum =1;
for item in tmpBigList:
    lineNum =lineNum+1;
    item_0=item[0].strip();
    if  item_0!="":
        if(item_0 not in lineNumDic.keys()):
            lineNumDic[item_0] =lineNum;
        else:
            print("error have repeat events");


haveContentList =[elem for elem in tmpBigList if (elem[0].strip()!="")];


noCompleteList =[elem for elem in haveContentList if(elem[1].strip()=="")];


# key是排序0-10吧，val是list集合
noCompleteListDic ={};

for item in noCompleteList:
    wStr =item[3].strip();
    wInt =0;
    if(wStr!=""):
        wInt=int(wStr);
    
    if wInt not in  noCompleteListDic.keys():
        tmpList =[];
        tmpList.append(item[:]);
        noCompleteListDic[wInt]=tmpList;
    else:
        tmpList =noCompleteListDic[wInt];
        tmpList.append(item[:]);

noCompleteListDicKeys = list(noCompleteListDic.keys());
noCompleteListDicKeys.sort(reverse=True);

def printCoderMsg(strss):
    key =strss[0].strip();
    return key+":"+ str(lineNumDic[key]);
    

print("未完成的事项有")
for item in noCompleteListDicKeys:
    tmpList =noCompleteListDic[item];
    for it in tmpList:
        print(printCoderMsg(it));

os.system("pause");