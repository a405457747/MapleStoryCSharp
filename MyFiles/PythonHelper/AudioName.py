# -*- coding: utf-8 -*-
import os;
import shutil;
import csv;
import  json;
import re;
import uuid
from jinja2 import Template;
import sys;

# 文件模版
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
# 音频资源的加载目录
dirPath =os.path.abspath(sys.argv[1]);


def readText(file, mode="r"):
    with open(file, mode, encoding='utf-8') as f:
        return f.read();

def writeText(file,content,mode="w"):
    with open(file,mode,encoding="utf-8") as f:
        f.write(content);

# 递归获取音频文件的加载路径
def traverse_folder(folder_path, files_dict):
    for file_name in os.listdir(folder_path):
        file_path = os.path.join(folder_path, file_name)
        if os.path.isdir(file_path):
            traverse_folder(file_path, files_dict)
        else:
            if(file_path.endswith(".meta")==False):
                suffix =os.path.splitext(file_path)[-1];
                dictKey = re.sub(suffix,"",file_name);
                files_dict[dictKey] =re.sub(suffix,"", os.path.relpath(file_path,dirPath));

def main():
    # 键是文件名，值是加载路径
    files_dict = {}
    traverse_folder(dirPath, files_dict)
    writeText(os.path.join(dirPath, "../../../Scripts/LoadPath/AudioName.cs"),Template(replaceStr).render(files_dict=files_dict));
    ...

if __name__ == '__main__':
    main();