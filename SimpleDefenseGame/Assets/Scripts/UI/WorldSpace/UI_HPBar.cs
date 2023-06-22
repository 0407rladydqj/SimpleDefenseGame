using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Base
{
    enum GameObjects
    {
        HPBar,
    }

    enum Texts
    {
        LevelText,
    }

    Stat _stat;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));
        _stat = transform.parent.GetComponent<Stat>();
 
    }

    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up*(parent.GetComponent<Collider>().bounds.size.y);
        transform.rotation = Camera.main.transform.rotation;
        float ratio = _stat.Hp / (float)_stat.MaxHp;
        SetHpRatio(ratio);
        SetLevelText();
    }

    public void SetHpRatio(float ratio)
    {
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }

    int _level = 0;
    private void SetLevelText()
    {
        if (_level == _stat.Level)
            return;

        _level = _stat.Level;
        GetText((int)Texts.LevelText).text = $"LV{_level.ToString()}";
    }
}
