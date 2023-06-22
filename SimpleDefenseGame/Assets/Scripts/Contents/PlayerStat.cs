using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    protected int _exp;
    [SerializeField]
    protected int _gold;

    Rune _rune;
    public Rune GetRune() { return _rune; }

    UI_Battle _uI_Battle;
    public UI_Battle GetUI_Battle() { return _uI_Battle; }

    public int Exp
    {
        get { return _exp; }
        set
        {
            _exp = value;
            int level = Level;
            while(true)
            {
                Data.Stat stat;
                if (Managers.Data.StatDict.TryGetValue(level + 1, out stat) == false)
                    break;
                if (_exp < stat.totalExp)
                    break;
                level++;
            }

            if(level != Level)
            {
                Managers.Sound.Play("UnityChan/univ0007");
                Level = level;
                SetStats();
                _uI_Battle.SetUI();
            }
        }
    }

    public int Gold { get { return _gold; } set { _gold = value; } }
    private void Start()
    {
        _rune = gameObject.GetOrAddComponent<Rune>();
        _rune.SetRune();
        _level = 1;
        _exp = 0;
        _defense = 5;
        _moveSpeed = 5.0f;
        _range = 2.0f;
        _gold = 0;
        SetStats();
        _uI_Battle = Managers.UI.FindUI<UI_Battle>();
    }

    private void SetStats()
    {
        SetStat(_level);
        _attack += _rune.DemageUp;
        _moveSpeed += _rune.SpeedUp;
        _maxHp += _rune.HealthUp;
        _range += _rune.RangeUp;
    }

    protected override void OnDead(Stat attacker)
    {
        Debug.Log("Player Is Dead");
    }
}
