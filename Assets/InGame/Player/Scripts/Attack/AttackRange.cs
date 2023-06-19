using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackRange
{
    [Header("�������镐��̍U���͈�")]
    [SerializeField] private AllWeapons.WeaponType _weaponType;

    [Header("Gizomo��`�悷�邩�ǂ���")]
    [SerializeField] private bool _isDrowGizumo = true;

    [Header("�G�̃��C���[")]
    [SerializeField] private LayerMask _layer;

    [Header("=== �U���͈͂̐ݒ� ===")]
    [Header("��---")]
    [SerializeField] private Vector3 _sizeSword;
    [SerializeField] private Vector3 _centerSword;

    [Header("��---")]
    [SerializeField] private Vector3 _sizeSpear;
    [SerializeField] private Vector3 _centerSpear;

    [Header("�匕---")]
    [SerializeField] private Vector3 _sizeLargeSword;
    [SerializeField] private Vector3 _centerLargeSword;

    [Header("�e---")]
    [SerializeField] private Vector3 _sizeGun;
    [SerializeField] private Vector3 _centerGun;

    [Header("�o��---")]
    [SerializeField] private Vector3 _sizeTwinSword;
    [SerializeField] private Vector3 _centerTwinSword;


    /// <summary>���݁A�ݒ肳��Ă���͈͂̑傫��</summary>
    private Vector3 _nowBoxSize;
    /// <summary>���݁A�ݒ肳��Ă���͈͂̒��S</summary>
    private Vector3 _nowCenter;

    private bool _isHitAttack = false;

    private bool _isCriticalHit = false;
    public bool IsHitAttack => _isHitAttack;

    private AllWeapons _allWeapons;

    public void Init(AllWeapons allWeapons)
    {
        _allWeapons = allWeapons;
    }


    /// <summary>�U���͈͂̐ݒ�</summary>
    public void SetRange(AllWeapons.WeaponType weaponType)
    {
        _isHitAttack = false;

        if (weaponType == AllWeapons.WeaponType.Sword)
        {
            _nowBoxSize = _sizeSword;
            _nowCenter = _centerSword;
        }
        else if (weaponType == AllWeapons.WeaponType.Spear)
        {
            _nowBoxSize = _sizeSpear;
            _nowCenter = _centerSpear;
        }
        else if (weaponType == AllWeapons.WeaponType.LargeSword)
        {
            _nowBoxSize = _sizeLargeSword;
            _nowCenter = _centerLargeSword;
        }
        else if (weaponType == AllWeapons.WeaponType.Gun)
        {
            _nowBoxSize = _sizeGun;
            _nowCenter = _centerGun;
        }
        else if (weaponType == AllWeapons.WeaponType.twinSword)
        {
            _nowBoxSize = _sizeTwinSword;
            _nowCenter = _centerTwinSword;
        }
    }

    public bool CheckRange()
    {
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, _nowCenter);
        Vector3 offset = rotation * _nowCenter;

        Collider[] hits = Physics.OverlapBox(_allWeapons.PlayerControl.PlayerT.position + offset, _nowBoxSize, _allWeapons.PlayerControl.PlayerT.rotation, _layer);

        if (hits.Length == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>�U��</summary>
    public void AttackHitCheck()
    {
        //Quaternion.L
        Quaternion rotation = Quaternion.LookRotation(_allWeapons.PlayerControl.PlayerT.forward, Vector3.up);
        Vector3 offset = rotation * _nowCenter;

        Collider[] hits = Physics.OverlapBox(_allWeapons.PlayerControl.PlayerT.position + offset, _nowBoxSize, _allWeapons.PlayerControl.PlayerT.rotation, _layer);

        List<GameObject> enemy = new List<GameObject>();

        foreach (var a in hits)
        {
            //�擾�����R���C�_�[�̈�ԏ�̐e�I�u�W�F�N�g���擾
            var enemyParent = a.transform.root.gameObject;

            enemyParent.TryGetComponent<IEnemyDamageble>(out IEnemyDamageble damage);

            //�����G�̃R���C�_�[(���ʂ���)�𕡐��擾���ĉ��x���U�����������Ȃ�����
            if (damage != null && !enemy.Contains(enemyParent))
            {
                //�����𔻒f
                HitType hitType = HitType.Nomal;
                if (_isCriticalHit && _allWeapons.PlayerControl.WeaponSetting.NowWeapon.Attribute == Attribute.Nomal)
                {
                    hitType = HitType.Clitical;
                }
                else
                {
                    if (_allWeapons.PlayerControl.WeaponSetting.NowWeapon.Attribute == Attribute.Fire)
                    {
                        hitType = HitType.Fire;
                    }   //�Α���
                    else if (_allWeapons.PlayerControl.WeaponSetting.NowWeapon.Attribute == Attribute.Ice)
                    {
                        hitType = HitType.Ice;
                    }   //�X����
                    else if (_allWeapons.PlayerControl.WeaponSetting.NowWeapon.Attribute == Attribute.Thunder)
                    {
                        hitType = HitType.Thunder;
                    }   //������
                }

                //����̓����������ɃG�t�F�N�g���o�������̂ŁARay�łȂ�ׂ������������ɂ��悤�Ǝv��
                RaycastHit hit;
                Vector3 dir = a.transform.position - _allWeapons.PlayerControl.PlayerMidlePos.position;
                Physics.Raycast(_allWeapons.PlayerControl.PlayerMidlePos.position, dir, out hit, 2, _layer);

                Vector3 effectPos = hit.collider != null ? hit.point : a.transform.position;

                //�U���������������̃G�t�F�N�g���o��
                _allWeapons.PlayerControl.ParticleControl.AttackHitParticles.UseAttackHitEffect(
                    effectPos,
                    _allWeapons.PlayerControl.WeaponSetting.NowWeapon.WeaponType,
                    hitType
                    );

                damage.AddDamageEnemy(_allWeapons.PlayerControl.WeaponSetting.NowWeapon.AttackPower, _allWeapons.PlayerControl.WeaponSetting.NowWeapon.WeaponType, _allWeapons.PlayerControl.WeaponSetting.NowWeapon.Attribute);
                enemy.Add(enemyParent);
            }
        }

        if (hits.Length == 0)
        {
            _isHitAttack = false;
        }
        else
        {
            Debug.Log("HITAttack");
            _isHitAttack = true;
        }
    }



    public void OnDrowGizmo(Transform player)
    {
        if (_isDrowGizumo)
        {
            Gizmos.matrix = Matrix4x4.TRS(player.position, player.rotation, player.localScale);


            Vector3 size = Vector3.zero;
            Vector3 center = Vector3.zero;

            if (_weaponType == AllWeapons.WeaponType.Sword)
            {
                size = _sizeSword;
                center = _centerSword;
            }
            else if (_weaponType == AllWeapons.WeaponType.Spear)
            {
                size = _sizeSpear;
                center = _centerSpear;
            }
            else if (_weaponType == AllWeapons.WeaponType.LargeSword)
            {
                size = _sizeLargeSword;
                center = _centerLargeSword;
            }
            else if (_weaponType == AllWeapons.WeaponType.Gun)
            {
                size = _sizeGun;
                center = _centerGun;
            }
            else if (_weaponType == AllWeapons.WeaponType.twinSword)
            {
                size = _sizeTwinSword;
                center = _centerTwinSword;
            }

            Gizmos.color = Color.red;

            Gizmos.DrawWireCube(center, size);
            Gizmos.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one);
        }
    }

}
