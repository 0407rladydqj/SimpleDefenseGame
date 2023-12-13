using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;
using UnityAsyncAwaitUtil;
using Cysharp.Threading.Tasks;
using System.Text;

public class UI_LoginPopup : UI_Popup
{
    Sprite[] _runeAtlas;

    enum Images
    {
        RuneImage,
    }

    enum Buttons
    {
        EasyButton,
        NormalButton,
        HardButton,
        ChooseRuneButton,
    }

    public override void Init()
    {
        base.Init();
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.EasyButton).gameObject.BindEvent(EasyButtonClicked);
        GetButton((int)Buttons.NormalButton).gameObject.BindEvent(NormalButtonClicked);
        GetButton((int)Buttons.HardButton).gameObject.BindEvent(HardButtonClicked);
        GetButton((int)Buttons.ChooseRuneButton).gameObject.BindEvent(ChooseRuneButtonClicked);

        _runeAtlas = Managers.Resource.LoadAll<Sprite>("AtlasImages/RuneAtlas");
        SetRuneImage();

        Managers.Sound.Play("High Noon", Define.Sound.Bgm);
    }

    private async void Start()
    {
        await MyUniTask();
    }

    public void SetRuneImage()
    {
        GetImage((int)Images.RuneImage).sprite = _runeAtlas[(int)Managers.Game.rune];
    }

    public void EasyButtonClicked(PointerEventData data)
    {
        LevelButtonClicked(Define.GameLevel.Easy);
    }

    public void NormalButtonClicked(PointerEventData data)
    {
        LevelButtonClicked(Define.GameLevel.Normal);
    }

    public void HardButtonClicked(PointerEventData data)
    {
        LevelButtonClicked(Define.GameLevel.Hard);
    }

    void LevelButtonClicked(Define.GameLevel gameLevel)
    {
        Managers.Game.gameLevel = gameLevel;
        Managers.Scene.LoadScene(Define.Scene.DefenseGame);
    }

    public void ChooseRuneButtonClicked(PointerEventData data)
    {
        Managers.UI.ShowPopupUI<UI_ChooseRune>();
    }

    StringBuilder myStringBuilder = new StringBuilder("UniTask running every ");
    int i = 0;
    private async UniTask MyUniTask()
    {
        while (true)
        {
            await UniTask.Delay(1000);  // Zero Allocation
            i++;
            Debug.Log(myStringBuilder.Append(i));
            myStringBuilder.Remove(22, i.ToString().Length);
        }
    }
}
