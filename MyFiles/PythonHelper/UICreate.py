# -*- coding: utf-8 -*-

import os;
import shutil;
import csv;
import  json;
import re;
import uuid
from jinja2 import Template;
import sys;

def readText(file, mode="r"):
    with open(file, mode, encoding='utf-8') as f:
        return f.read();
def writeText(file,content,mode="w"):
    with open(file,mode,encoding="utf-8") as f:
        f.write(content);

def getReplaceContent(data):
    t='''
    {%- for k,v in data1.items() %}
    [SerializeField] public {{v}} {{k}} = null;
    {% if v=="Button" %}
    public void {{k}}OnClick(UnityAction clickAction)=> {{k}}.onClick.AddListener(clickAction);
    {% elif v=="Text" %}
    public void {{k}}Text(string txt)=>{{k}}.text = txt;
    {% elif v=="Image" %}
    public void {{k}}Sprite(Sprite sp)=> {{k}}.sprite = sp;
    {% elif v=="RectTransform" %}
    public void {{k}}Parent(Transform parent, bool worldPositionStays)=>{{k}}.SetParent(parent,worldPositionStays);
    {% elif v=="InputField" %}
    public string {{k}}Input()=>{{k}}.text; 
    {% elif v=="Toggle" %}
    public void {{k}}OnChanged(UnityAction<bool> toggleAction)=> {{k}}.onValueChanged.AddListener(toggleAction);
    {% endif %}
    {%- endfor %}
    void FindAllComponent()
    {
        {% for k,v in data1.items() -%}
        {{k}}=ToolRoot.FindComponent<{{v}}>(this,"{{k}}");
        {% endfor %}
    }
    '''
    tt=Template(t);

    return tt.render(data1=data['data1']);
    

def fileAction(path,data):
    csText =readText(path)

    replaceContent=getReplaceContent(data);

    #print("replaceContent",replaceContent);
    csText2=re.sub('( {4}///AC\n)(.*)( {4}///AC\n)',"    ///AC\n"+replaceContent+"\n    ///AC\n",csText,flags=re.DOTALL);# warn 硬编码
    if( csText!=csText2  ): #or (len(data['data1'])==0)
        writeText(path,csText2);
    else:# warn 这里时序不够优雅
        if("FindAllComponent"  in csText2):
            return;
        t='''

    ///AC
    void FindAllComponent()
    {
    }    
    ///AC
    void Awake()
    {
        FindAllComponent();
    }        
'''
        csText3=re.sub('^\}',t+"}",csText2,flags=re.MULTILINE);
        #print(csText3);
        writeText(path,csText3);
    


#print("i am python",sys.argv[1],json.loads(sys.argv[1]))
#SendObjText=readText("./SendObj.txt");
#dict=json.loads(sys.argv[1])
#str ='"' +sys.argv[1]+'"';
#print(str,"i am python",json.loads(sys.argv[1]),strict=False);


readT=readText(os.path.abspath(sys.argv[1]));
#print(readT);


dict=json.loads(readT);
fileAction(dict['csFilePath'],dict);

