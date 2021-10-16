using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : MonoBehaviour
{
    public static Dictionary<string,Stack<GameObject>> ObjPoolDic = new Dictionary<string,Stack<GameObject>>();
    public static List<GameObject> EnableObjList = new List<GameObject>();

    public static void Register(GameObject target,int count){//追加・登録
        if(!ObjPoolDic.ContainsKey(target.name)){
            ObjPoolDic.Add(target.name,new Stack<GameObject>());
        }
        for(int a = 0;a<count;a++){
            GameObject pObj = Instantiate(target);
            pObj.SetActive(false);
            ObjPoolDic[target.name].Push(pObj);
        }
    }

    public static GameObject Create(string name){//生成
        if(!ObjPoolDic.ContainsKey(name)){
            print("You must need Register() to use Create() method");
        }
        GameObject returnobj = ObjPoolDic[name].Pop();
        returnobj.SetActive(true);
        EnableObjList.Add(returnobj);
        return returnobj;
    }

    public static void Sleep(GameObject target){//使い終わった後の処理
        target.SetActive(false);
        EnableObjList.Remove(target);
        ObjPoolDic[target.name].Push(target);
    }

}
