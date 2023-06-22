using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class UI_Battle : UI_Popup
{
    Sprite[] _runeAtlas;
    PlayerStat _playerStat;
    Rune _rune;
    enum Buttons
    {
        BackButton,
        SoundButton
    }

    enum Images
    {
        RuneImage,
    }

    enum Texts
    {
        RuneNameText,
        LevelText,
        HpText,
        AttackText,
        DefenseText,
        MoveSpeedText,
        RangeText,
        ExpText,
        GoldText
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));

        _runeAtlas = Managers.Resource.LoadAll<Sprite>("AtlasImages/RuneAtlas");
        GetImage((int)Images.RuneImage).sprite = _runeAtlas[(int)Managers.Game.rune];

        _playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
        _rune = Managers.Game.GetPlayer().GetComponent<PlayerStat>().GetRune();
        GetText((int)Texts.RuneNameText).text = _rune.Name;
        SetUI();

        GetButton((int)Buttons.BackButton).gameObject.BindEvent(BackButtonClicked);
        GetButton((int)Buttons.SoundButton).gameObject.BindEvent(SoundButtonClicked);

        Managers.Sound.Play("Drop the Tapes",Define.Sound.Bgm);
    }
    
    public void SetUpdateUI()
    {
        GetText((int)Texts.ExpText).text = _playerStat.Exp.ToString();
        GetText((int)Texts.GoldText).text = _playerStat.Gold.ToString();
    }

    public void SetUI()
    {
        GetText((int)Texts.HpText).text = _playerStat.MaxHp.ToString();
        GetText((int)Texts.LevelText).text = _playerStat.Level.ToString();
        GetText((int)Texts.AttackText).text = _playerStat.Attack.ToString();
        GetText((int)Texts.DefenseText).text = _playerStat.Defense.ToString();
        GetText((int)Texts.MoveSpeedText).text = _playerStat.MoveSpeed.ToString();
        GetText((int)Texts.RangeText).text = _playerStat.Range.ToString();
        SetUpdateUI();
    }

    public void BackButtonClicked(PointerEventData data)
    {
        Managers.Scene.LoadScene(Define.Scene.Login);
    }

    public void SoundButtonClicked(PointerEventData data)
    {
        Managers.Sound.OnOffBGM();
    }
}
