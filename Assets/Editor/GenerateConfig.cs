using IniParser;
using IniParser.Model;
using IniParser.Parser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GenerateConfig
{
    private static string _nodeconfigFile = Application.streamingAssetsPath + "/NodeConfig.ini";

    [MenuItem("Assets/Generate")]
    public static void Generate()
    {
        GameObject activeObject = Selection.activeObject as GameObject;
        if (null == activeObject)
        {
            return;
        }

        Debug.Log("GenerateConfig.Generate() : 开始生成配置文件");
        Transform[] childArray = TranGetChild(activeObject.transform);
        Write(childArray);
        Debug.Log("GenerateConfig.Generate() : 配置文件生成完成 ！ ！ ！");
    }

    static Transform[] TranGetChild(Transform tran)
    {
        Queue<Transform> result = new Queue<Transform>();
        Queue<Transform> queue = new Queue<Transform>();
        queue.Enqueue(tran);

        while (queue.Count > 0)
        {
            Transform front = queue.Dequeue();
            result.Enqueue(front);

            for (int i = 0; i < front.childCount; i++)
            {
                queue.Enqueue(front.GetChild(i));
            }
        }
        return result.ToArray();
    }

    static void CreateConfigDirectory()
    {
        FileInfo info = new FileInfo(_nodeconfigFile);
        if (File.Exists(info.FullName))
        {
            File.Delete(info.FullName);
        }

        Directory.CreateDirectory(info.DirectoryName);
        FileStream stream = File.Create(info.FullName);
        stream.Close();
    }

    static void Write(Transform[] childArray)
    {
        CreateConfigDirectory();

        //初始化INIParser
        FileIniDataParser parser = new FileIniDataParser();
        parser.Parser.Configuration.AllowDuplicateKeys = true;
        parser.Parser.Configuration.OverrideDuplicateKeys = true;
        parser.Parser.Configuration.AllowDuplicateSections = true;

        IniData iniData = parser.ReadFile(_nodeconfigFile);
        SectionDataCollection sectionCollection = new SectionDataCollection();
        iniData.Sections = sectionCollection;
        
        for (int i = 0; i < childArray.Length; i++)
        {
            Transform child = childArray[i];
            if (child.tag == "pw")
            {
                sectionCollection.AddSection(child.name);
                iniData[child.name]["ID"] = i.ToString();
                iniData[child.name]["Info"] = "请录入信息_" + i;
            }
        }
        
        parser.WriteFile(_nodeconfigFile, iniData);
    }
}
