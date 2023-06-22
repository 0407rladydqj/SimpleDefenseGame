using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class UI_ChooseRune : UI_Popup
{
    Sprite[] _runeAtlas;

    enum Buttons
    {
        CloseButton,
        UnknownButton,
        DamageButton,
        SpeedButton,
        HealthButton,
        RangeButton,
        AbsoluteButton
    }

    enum Images
    {
        NowRuneImage,
    }

    enum Texts
    {
        RuneInformationText
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));

        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(CloseButtonClicked);

        _runeAtlas = Managers.Resource.LoadAll<Sprite>("AtlasImages/RuneAtlas");
        RuneUpdate(Managers.Game.rune);

        GetButton((int)Buttons.UnknownButton).gameObject.BindEvent(UnknownButtonClicked);
        GetButton((int)Buttons.DamageButton).gameObject.BindEvent(DamageButtonClicked);
        GetButton((int)Buttons.SpeedButton).gameObject.BindEvent(SpeedButtonClicked);
        GetButton((int)Buttons.HealthButton).gameObject.BindEvent(HealthButtonClicked);
        GetButton((int)Buttons.RangeButton).gameObject.BindEvent(RangeButtonClicked);
        GetButton((int)Buttons.AbsoluteButton).gameObject.BindEvent(AbsoluteButtonClicked);
    }

    public void CloseButtonClicked(PointerEventData data)
    {
        Managers.UI.ClosePopupUI();
    }

    public void UnknownButtonClicked(PointerEventData data)
    {
        RuneUpdate(Define.Rune.Unknown);
    }

    public void DamageButtonClicked(PointerEventData data)
    {
        RuneUpdate(Define.Rune.Damage);
    }

    public void SpeedButtonClicked(PointerEventData data)
    {
        RuneUpdate(Define.Rune.Speed);
    }

    public void HealthButtonClicked(PointerEventData data)
    {
        RuneUpdate(Define.Rune.Health);
    }

    public void RangeButtonClicked(PointerEventData data)
    {
        RuneUpdate(Define.Rune.Range);
    }

    public void AbsoluteButtonClicked(PointerEventData data)
    {
        RuneUpdate(Define.Rune.Absolute);
    }

    /*»ý·«*/
    private void RuneUpdate(Define.Rune rune)
    {
        Managers.Game.rune = rune;
        GetImage((int)Images.NowRuneImage).sprite = _runeAtlas[(int)Managers.Game.rune];
        Managers.UI.FindUI<UI_LoginPopup>().SetRuneImage();
        GetText((int)Texts.RuneInformationText).text = 
            Managers.SQL.SimpleDBReadStringOne("Information",
            Define.DBTableName.Rune,"Name", rune.ToString());
    }
}
