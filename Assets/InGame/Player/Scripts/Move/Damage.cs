using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class Damage
{
    [Header("�|���ɂ�����_���[�W")]
    [SerializeField] private int _downToDamage = 5;

    [Header("�m�b�N�o�b�N�̗�")]
    [SerializeField] private float _knockBackPower = 5;

    [Header("�s���s�\�̕b��")]
    [SerializeField] private float _downTime = 2;

    [Header("�|���A�j���[�V������")]
    [SerializeField] private string _downAnimName;

    [Header("�N���オ��Ƃ��̃A�j���[�V�������O")]
    [SerializeField] private string _upAnimName;

    [Header("TimeLine")]
    [SerializeField] private GameObject _timeLine;

    /// <summary>�_���[�W�������I��������ǂ���</summary>
    private bool _isEndDown = false;

    private bool _isDamageNow = false;

    private bool _isDamage = false;

    private bool _isGetUpStart = false;

    private PlayerControl _playerControl;

    public int DownToDamage => _downToDamage;
    public bool IsDamage { get => _isDamage; set => _isDamage = value; }
    public bool IsEndDown => _isEndDown;

    public bool IsDamageNow => _isDamageNow;

    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }


    /// <summary>�����ݒ�</summary>
    public void FirstSetting()
    {
        _isDamage = false;

        _isDamageNow = true;

        _isEndDown = false;

        _playerControl.Rb.useGravity = true;
        _playerControl.Rb.velocity = Vector3.zero;
        Vector3 dir = -_playerControl.PlayerT.forward;
        _playerControl.Rb.velocity = dir * _knockBackPower;

        _playerControl.LayerControl.SetPlayerGodLayer();

        _playerControl.AnimControl.PlayerAnimPlay(_downAnimName);
    }


    public void Dead()
    {
        _isDamageNow = true;
        _timeLine.SetActive(true);
    }

    public void PlayerRotate()
    {

    }

    public void EndTimeLine()
    {
        _playerControl.PlayerStatus.PlayerLife.Heal(_playerControl.PlayerStatus.MaxHp);
        _isDamageNow = false;
        _isDamage = false;
        _playerControl.AnimControl.PlayerAnimPlay(_upAnimName);
    }


    /// <summary>�I�����̐ݒ�</summary>

    public void EndDamage()
    {
        _isDamageNow = false;

    }

    public void GetUpEnd()
    {
        _isEndDown = true;
    }
}
