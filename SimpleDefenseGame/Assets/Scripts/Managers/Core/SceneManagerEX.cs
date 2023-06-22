using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEX
{
    public BaseScene CurrentScene{get{return GameObject.FindObjectOfType<BaseScene>();}}
    public void LoadScene(Define.Scene type)
    {
        //지금 씬에서 Clear함수를 실행 후 씬 로드
        Managers.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
