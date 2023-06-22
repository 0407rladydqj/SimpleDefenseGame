using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
    protected int _level;
    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _maxHp;
    [SerializeField]
    protected int _attack;
    [SerializeField]
    protected int _defense;
    [SerializeField]
    protected float _moveSpeed;
    [SerializeField]
    protected float _range;

    //»ý·«

    public int Level 
    { get { return _level; } set { _level = value; } }
    public int Hp 
    { get { return _hp; } set { _hp = value; } }
    public int MaxHp 
    { get { return _maxHp; } set { _maxHp = value; } }
    public int Attack 
    { get { return _attack; } set { _attack = value; } }
    public int Defense 
    { get { return _defense; } set { _defense = value; } }
    public float MoveSpeed 
    { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float Range 
    { get { return _range; } set { _range = value; } }

    private void Start()
    {
        SetStat(1);
        /*
        _level = 1;
        _hp = 100;
        _maxHp = 100;
        _attack = 10;
        _defense = 5;
        _moveSpeed = 5.0f;
        _range = 2.0f;
        */
    }

    public void SetStat(int level)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        Data.Stat stat = dict[level];

        _level = stat.level;
        _hp = stat.maxHp;
        _maxHp = stat.maxHp;
        _attack = stat.attack;

        _moveSpeed = 5.0f;
        _range = 2.0f;
    }

    public virtual void OnAttacked(Stat attacker)
    {
        Managers.Sound.Play("Hit");
        int damage = Mathf.Max(0, attacker.Attack - Defense);
        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            OnDead(attacker);
        }
    }

    protected virtual void OnDead(Stat attacker)
    {
        PlayerStat playerStat = attacker as PlayerStat;
        if (playerStat != null)
        {
            playerStat.Exp += 5;
            playerStat.Gold += 8;
            playerStat.GetUI_Battle().SetUpdateUI();
        }

        Managers.Game.Despawn(gameObject);
    }
}
