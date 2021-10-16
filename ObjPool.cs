using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : MonoBehaviour
{
    public static Dictionary<string,Stack<object>> ObjPoolDic = new Dictionary<string,Stack<object>>();
    public static List<object> EnableObjList = new List<object>();

    public static void Register(object target,int count){//追加・登録
        if(!ObjPoolDic.ContainsKey(target.name)){
            ObjPoolDic.Add(target.name,new Stack<object>());
        }
        for(int a = 0;a<count;a++){
            object pObj = Instantiate(target);
            pObj.SetActive(false);
            ObjPoolDic[target.name].Push(pObj);
        }
    }

    public static object Create(string name,Vector3? pos=  null,Quaternion? rot = null){//生成
        if(!ObjPoolDic.ContainsKey(name)){
            print("You must need Register() to use Create() method");
        }
        object returnobj = ObjPoolDic[name].Pop();
        returnobj.SetActive(true);
        EnableObjList.Add(returnobj);
        if(pos != null){
            returnobj.transform.position = (Vector3)pos;
        }
        if(rot != null){
            returnobj.transform.rotation = (Quaternion)rot;
        }
        return returnobj;
    }

    public static void Sleep(object target){//使い終わった後の処理
        target.SetActive(false);
        EnableObjList.Remove(target);
        ObjPoolDic[target.name].Push(target);
    }

}
