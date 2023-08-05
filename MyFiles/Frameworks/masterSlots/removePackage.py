#!/usr/bin/env python3
import os;

path =os.getcwd();

os.chdir(path);

fileList =os.listdir(path);
for fileItem in fileList:
    baseName =os.path.basename(fileItem);
    arr =baseName.split('.');
    if(len(arr)>1):
        second =baseName.split('.')[1];
        if second== "aab" or second=="apk" or second=="ipa":
            print("删除了"+fileItem);
            os.remove(fileItem);
