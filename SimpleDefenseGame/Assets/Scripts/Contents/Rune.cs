using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
    [SerializeField]
    protected string _name;
    [SerializeField]
    protected int _damageUp;
    [SerializeField]
    protected float _speedUp;
    [SerializeField]
    protected int _healthUp;
    [SerializeField]
    protected float _rangeUp;

    public string Name { get { return _name; } }
    public int DemageUp { get { return _damageUp; } }
    public float SpeedUp { get { return _speedUp; } }
    public int HealthUp { get { return _healthUp; } }
    public float RangeUp { get { return _rangeUp; } }

    public void SetRune()
    {
        _name = Managers.Game.rune.ToString();
        _damageUp = Managers.SQL.SimpleDBReadIntOne
            ("DamageUp", Define.DBTableName.Rune, "Name", _name);
        _speedUp = Managers.SQL.SimpleDBReadIntOne
            ("SpeedUp", Define.DBTableName.Rune, "Name", _name);
        _healthUp = Managers.SQL.SimpleDBReadIntOne
            ("HelthUp", Define.DBTableName.Rune, "Name", _name);
        _rangeUp = Managers.SQL.SimpleDBReadIntOne
            ("RangeUp", Define.DBTableName.Rune, "Name", _name);
    }
}
