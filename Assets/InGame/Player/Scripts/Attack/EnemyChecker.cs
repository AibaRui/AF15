using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class EnemyChecker : MonoBehaviour
{
    [Header("�G�̒T�m�͈�")]
    [SerializeField] private float _radius = 5;

    [Header("�G�̒T�m���C���[")]
    [SerializeField] private LayerMask _layer;

    [Header("��Q���Ƃ݂Ȃ����ׂẴ��C���[")]
    [SerializeField] private LayerMask _obstacleLayer;

    [Header("�퓬�G���A�̃��C���[")]
    [SerializeField] private LayerMask _buttuleErea;

    [SerializeField] private PlayerControl _playerControl;

    /// <summary>���݃^�[�Q�b�e�B���O���Ă��邩�ǂ��� </summary>
    private bool _isTarget;

    private bool _isInBattleArea;

    private Collider _buttuleAreaCollider;


    /// <summary>���݃��b�N�I�����Ă���G</summary>
    private GameObject _nowLockOnEnemy;

    private List<GameObject> _obj = new List<GameObject>();

    public List<GameObject> RockOns => _obj;
    public bool IsInButtuleArea => _isInBattleArea;
    public bool IsTargetting => _isTarget;

    public GameObject NowEnemy => _nowLockOnEnemy;


    /// <summary�^�[�Q�e�B���O���Ă��邩�ǂ��������߂�</summary>
    public void EnemyTargetting()
    {
        if (_playerControl.InputManager.IsRightMouseClickDown && _nowLockOnEnemy != null)
        {
            _isTarget = true;
        }   //�^�[�Q�e�B���O�{�^���������Ă�����^�[�Q�e�B���O���̂���

        if (_playerControl.InputManager.IsRightMouseClickUp)
        {
            _isTarget = false;
        }   //�^�[�Q�e�B���O�{�^���������ċ��Ȃ�������^�[�Q�e�B���O���ɂ��Ȃ�
    }

    /// <summary>�G��T�m����</summary>
    public void EnemyCheck()
    {
        CheckEnemy();
        ObstacleCheck();
        SearchButtuleArea();
    }

    public void SearchButtuleArea()
    {
        var a = _playerControl.ColliderSearcher.Search(_buttuleErea);

        if (a.Length > 0)
        {
            if (_buttuleAreaCollider == null)
            {
                _isInBattleArea = true;
                _buttuleAreaCollider = a[0];
                _buttuleAreaCollider.GetComponent<IInButtuleAreable>()?.InPlayer(_playerControl);
            }
        }
        else
        {
            if (_buttuleAreaCollider != null)
            {
                _isInBattleArea = false;
                _buttuleAreaCollider.GetComponent<IInButtuleAreable>()?.OutPlayer();
                _buttuleAreaCollider = null;
            }
        }
    }

    /// <summary>���b�N�I�����Ă���G�܂ł̏�Q�����m�F����</summary>
    public void ObstacleCheck()
    {
        RaycastHit hitObstacle;

        if (_nowLockOnEnemy == null || !_isInBattleArea) return;

        //���b�N�I�����Ă���G�Ɍ�����Ray���΂�
        bool hit = Physics.Linecast(_playerControl.PlayerMidlePos.position, _nowLockOnEnemy.transform.position, out hitObstacle, _obstacleLayer);

        if (hit)
        {
            //�擾�����R���C�_�[�̈�ԏ�̐e�I�u�W�F�N�g���擾
            var nowEnemyParent = _nowLockOnEnemy.transform.root.gameObject;

            //���b�N�I�����Ă���G�̈�ԏ�̐e�I�u�W�F�N�g���擾
            var hitEnemyParent = hitObstacle.collider.gameObject.transform.root.gameObject;

            //�o���������łȂ��ꍇ�A���b�N�I�����Ă���G�Ƃ̊Ԃɏ�Q����
            //����Ƃ݂ă��b�N�I������������
            if (hit && nowEnemyParent != hitEnemyParent) _nowLockOnEnemy = null;
        }
    }

    /// <summary>�G�����邩�ǂ������m�F����</summary>
    public void CheckEnemy()
    {
        //1.�G�����o
        //2.�v���C���[�̎��_�̐�𐳖ʂƂ��āA�E���Ɋp�x�����v�Z���Ă���
        //3.�p�x���̏��������̂����b�N�I��

        if (_isTarget || !_isInBattleArea) return;

        //�G�����o
        Collider[] getColliders = Physics.OverlapSphere(_playerControl.PlayerMidlePos.position, _radius, _layer);
        List<EnemyAnglePair> enemyAngles = new List<EnemyAnglePair>();



        //�G�����Ȃ�
        if (getColliders.Length == 0)
        {
            _nowLockOnEnemy = null;
            return;
        }
        else
        {

        }

        List<GameObject> enemys = new List<GameObject>();

        //�G�ƃv���C���[�̊p�x���v�Z
        foreach (Collider collider in getColliders)
        {
            Vector3 direction = collider.transform.position - _playerControl.PlayerT.position;
            float angle = Vector3.Angle(_playerControl.PlayerT.forward, direction);
            enemyAngles.Add(new EnemyAnglePair(collider, angle));
            enemys.Add(collider.gameObject);
        }

        //�p�x�����Ⴂ���Ƀ\�[�g
        enemyAngles.Sort((a, b) => a.Angle.CompareTo(b.Angle));

        //���b�N�I������G��ݒ�
        _nowLockOnEnemy = enemyAngles[0].Collider.gameObject;

        _obj = enemys;
    }
}
