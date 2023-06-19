using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatus
{
    [Header("Hp設定")]
    [SerializeField] private PlayerLife _playerLife;

    [Header("スキルポイント")]
    [SerializeField] private SkillPoint _skillPoint;
    //[SerializeField] private Skill

    [SerializeField] private int _maxHp;
    [SerializeField] private int _dex;



    public SkillPoint SkillPoint => _skillPoint;
    private PlayerControl _playerControl;
    public int Dex => _dex;
    public int MaxHp => _maxHp;
    public PlayerLife PlayerLife => _playerLife;
    public PlayerControl PlayerControl => _playerControl;
    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;

        _skillPoint.Init(this);
        _playerLife.Init(this);
    }
}
