using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillPoint
{
    [Header("�����̃X�L���|�C���g")]
    [SerializeField] private int _firstSkillPoint;

    private int _nowSkilPoit = 0;

    public int NowSkillPoint { get => _nowSkilPoit; set => _nowSkilPoit = value; }

    private PlayerStatus _playerStatus;

    public void Init(PlayerStatus playerStatus)
    {
        _playerStatus = playerStatus;
        _nowSkilPoit = _firstSkillPoint;
    }

}
