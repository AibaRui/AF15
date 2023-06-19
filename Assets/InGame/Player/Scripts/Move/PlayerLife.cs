using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerLife
{
    private int _nowHp;

    private PlayerStatus _playerStatus;

    public void Init(PlayerStatus playerStatus)
    {
        _playerStatus = playerStatus;

        _nowHp = _playerStatus.MaxHp;
    }

    public void Heal(int heal)
    {
        _nowHp += heal;

        if (_nowHp > _playerStatus.MaxHp)
        {
            _nowHp = heal;
        }

        _playerStatus.PlayerControl.InGameUIControl.LifeSet(_nowHp);
    }


    public bool Damage(int damage)
    {
        int setDamage = damage - _playerStatus.Dex;

        _nowHp -= setDamage;



        if (_nowHp > 0)
        {
            _playerStatus.PlayerControl.InGameUIControl.LifeSet(_nowHp);

            return false;
        }
        else
        {
            _nowHp = 0;
            _playerStatus.PlayerControl.InGameUIControl.LifeSet(_nowHp);
            return true;
        }
    }

}
