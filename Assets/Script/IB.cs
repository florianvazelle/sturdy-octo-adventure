using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;
using Sirenix.OdinInspector;
using UnityEditor;
using System.Collections.Generic;


public class IB : MonoBehaviour
{
    GameObject lastCreature;
    public Transform positionToInstantiate;
    [Button]
    public void callObj()
    {

        try
        {
            Process myProcess = new Process();
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.FileName = "Assets/Script/callBlenderScript.bat";
            myProcess.EnableRaisingEvents = true;
            myProcess.Start();
            myProcess.WaitForExit();
            int ExitCode = myProcess.ExitCode;
            print(ExitCode);
            print("Monster Generated");
            AssetDatabase.Refresh();
            UnityEngine.Debug.Log(Application.dataPath + "/Monster.fbx");
        }
        catch (Exception e)
        {
            print(e);
        }
        var seed = GUID.Generate();
        FileUtil.MoveFileOrDirectory("Assets/Monster.fbx", "Assets/FBX/Monster_" + seed + ".fbx");
        AssetDatabase.Refresh();
        lastCreature = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/FBX/Monster_" + seed + ".fbx", typeof(GameObject));
        var creature = Instantiate(lastCreature, positionToInstantiate.position, Quaternion.identity);
        creature.AddComponent<AddRagdoll>();

    }
}
