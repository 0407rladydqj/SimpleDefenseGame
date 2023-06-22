using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;
        Managers.UI.ShowPopupUI<UI_LoginPopup>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Scene.LoadScene(Define.Scene.DefenseGame);
        }
    }

    public override void Clear()
    {
        Debug.Log("LodingScene Clear!");
    }
}