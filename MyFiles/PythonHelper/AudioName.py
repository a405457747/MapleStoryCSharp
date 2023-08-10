# -*- coding: utf-8 -*-

import os;
import shutil;
import csv;
import  json;
import re;
import uuid
from jinja2 import Template;
import sys;



dirPath =os.path.abspath(sys.argv[1]);    

#relative_path = os.path.relpath(path, start) 

'''
set=None;
with open('set.json', 'r', encoding='utf-8') as f:
    set =json.loads(f.read());
'''

def readText(file, mode="r"):
    with open(file, mode, encoding='utf-8') as f:
        return f.read();
def writeText(file,content,mode="w"):
    with open(file,mode,encoding="utf-8") as f:
        f.write(content);



replaceStr='''
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class AudioName
{

        {% for k,v in files_dict.items() -%}

        public static string  {{k}}=@"{{v}}";

        {% endfor %}
    
}
'''

def traverse_folder(folder_path, files_dict):
    for file_name in os.listdir(folder_path):
        file_path = os.path.join(folder_path, file_name)
        if os.path.isdir(file_path):
            traverse_folder(file_path, files_dict)
        else:
            if(file_path.endswith(".meta")==False):
                suffix =os.path.splitext(file_path)[-1];
                dictKey = re.sub(suffix,"",file_name);
                #dictValue =re.sub(r"%s"%dirPath,"",re.sub(suffix,"",file_path))
                #print("suffix",suffix);
                #print(re.sub(dirPath,"", os.path.dirname(file_path)));
                files_dict[dictKey] =re.sub(suffix,"", os.path.relpath(file_path,dirPath));
                #print(dictKey,file_path,os.path.relpath(file_path,dirPath));
                
            

# 创建一个空字典用于保存文件名和路径
files_dict = {}
# 调用递归函数来遍历文件夹
traverse_folder(dirPath, files_dict)
# 打印字典中的文件名和路径
#print(files_dict);


tt=Template(replaceStr);
afterStr=tt.render(files_dict=files_dict);
writeText(os.path.join(dirPath, "../AudioName.cs"),afterStr);
