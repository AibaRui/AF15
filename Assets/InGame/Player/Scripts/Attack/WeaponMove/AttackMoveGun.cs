using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackMoveGun : WeaponAttackMoveBase
{
    //[Header("�U���̃A�j���[�^�[��")]
    //[SerializeField] private string _fireAnimName;

    [Header("�����[�h�̃A�j���[�^�[��")]
    [SerializeField] private string _reLodeAnimName;

    /// <summary>�����[�h�����ǂ���</summary>
    private bool _isReLode = false;

    private Dictionary<string, int> _bulletNum = new Dictionary<string, int>();

    public override void AttackAir()
    {

    }

    public override void AttackFirst()
    {
        _playerControl.Attack.IsAttackNow = true;

        _thisWeaponAttackCount++;

        if (_bulletNum.ContainsKey(_playerControl.WeaponSetting.NowWeapon.WeaponName))
        {
            _bulletNum[_playerControl.WeaponSetting.NowWeapon.WeaponName]++;
        }
        else
        {
            _bulletNum.Add(_playerControl.WeaponSetting.NowWeapon.WeaponName, 1);
        }
        int bulletMax = 0;

        if (_playerControl.WeaponSetting.NowWeapon is ScritableWeaponGun gun)
        {
            bulletMax = gun.MaxBulletNum;
        }

        //�U���񐔂̐ݒ�i�A�j���[�^�[�j
        _playerControl.AnimControl.SetAttackCount(_thisWeaponAttackCount);

        if (bulletMax < _bulletNum[_playerControl.WeaponSetting.NowWeapon.WeaponName])
        {
            _isReLode = true;
            _playerControl.Attack.IsCanAttack = false;
            _playerControl.AnimControl.AttackAnim();
            _playerControl.AnimControl.PlayerAnimPlay(_reLodeAnimName);
        }   //�����[�h
        else
        {
            _playerControl.Animator.SetInteger("AttackType", 5);
            _playerControl.AnimControl.AttackAnim();
        }   //���C   

        //��]�̐ݒ�
        _playerControl.AttackAsist.AssistSet(_playerControl.EnemyCheck.NowEnemy);
    }

    public override void EndAttack()
    {
        _isAttack = false;

        if (_isReLode)
        {
            _bulletNum[_playerControl.WeaponSetting.NowWeapon.WeaponName] = 0;
            _thisWeaponAttackCount = 0;
            _isReLode = false;
        }
    }

    public override void ReSetValue()
    {
        if (_bulletNum.Count > 0 && _thisWeaponAttackCount > 0)
        {
            _bulletNum[_playerControl.WeaponSetting.NowWeapon.WeaponName] = 0;
            Debug.Log("�e��:" + _bulletNum[_playerControl.WeaponSetting.NowWeapon.WeaponName]);
        }

        _thisWeaponAttackCount = 0;
    }

    public override void SelectWeapon()
    {

    }

    public override void ShiftBreakAttack()
    {

    }

    public override void UseEndWeapon()
    {

    }

    public override void WeaponFixedUpdata()
    {
        if (!_isReLode)
            _playerControl.AttackAsist.AttackAssistRotation();
    }

    public override void WeaponUpData()
    {

    }

    public void ReLodeBullet()
    {
        _bulletNum[_playerControl.WeaponSetting.NowWeapon.WeaponName] = 0;
    }

    public override void ShiftBreakAttackEnd()
    {
        throw new System.NotImplementedException();
    }

    public override void StopAttackForDamage()
    {
        _isAttack = false;
    }
}
