using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLockOn : MonoBehaviour
{
    [SerializeField] private EnemyChecker _enemyChecker;

    [SerializeField] private GameObject _targetUI;

    // �I�u�W�F�N�g���f���J����
    [SerializeField] private Camera _targetCamera;

    // �I�u�W�F�N�g�ʒu�̃I�t�Z�b�g
    [SerializeField] private Vector3 _worldOffset;

    [Header("-----�G���-----")]
    [Header("�G���̃p�l��")]
    [SerializeField] private GameObject _enemyInfo;

    [Header("�G�̖��O��Text")]
    [SerializeField] private Text _enemyInfoName;

    [Header("�G��Hp�̃X���C�_�[")]
    [SerializeField] private Slider _enemyInfoHp;


    [Header("-----�A�C�R��-----")]

    [Header("��{�B�A�C�R��")]
    [SerializeField] private GameObject _enemyInfoWeak;

    [Header("���̃A�C�R��")]
    [SerializeField] private GameObject _swordIcon;

    [Header("�匕�̃A�C�R��")]
    [SerializeField] private GameObject _largeSwordIcon;

    [Header("�o���̃A�C�R��")]
    [SerializeField] private GameObject _twinSwordIcon;

    [Header("���̃A�C�R��")]
    [SerializeField] private GameObject _spearIcon;

    [Header("�e�̃A�C�R��")]
    [SerializeField] private GameObject _gunIcon;

    [Header("���̃A�C�R��")]
    [SerializeField] private GameObject _fireIcon;

    [Header("�X�̃A�C�R��")]
    [SerializeField] private GameObject _iceIcon;

    [Header("���̃A�C�R��")]
    [SerializeField] private GameObject _thenderIcon;


    [SerializeField] private PlayerControl _playerControl;

    private RectTransform _parentUI;
    private void Awake()
    {
        // �J�������w�肳��Ă��Ȃ���΃��C���J�����ɂ���
        if (_targetCamera == null)
            _targetCamera = Camera.main;

        // �eUI��RectTransform��ێ�
        _parentUI = _targetUI.transform.parent.GetComponent<RectTransform>();
    }

    //�J�����̍X�V�ƍ��킹�邽�߁AFixed�ŌĂԂ��Ƃɂ���
    private void FixedUpdate()
    {
        if (_enemyChecker.NowEnemy != null)
        {
            if (!_targetUI.activeSelf)
            {
                _targetUI.SetActive(true);
            }

            if (!_enemyInfo.activeSelf)
            {
                _enemyInfo.SetActive(true);
            }
            UpdateLockOnUIPosition();
            EnemyInfoUIUpdata();

        }
        else
        {
            //���b�N�I������G�����Ȃ�������}�[�J�[���\��
            if (_targetUI.activeSelf)
            {
                _targetUI.SetActive(false);
            }

            if (_enemyInfo.activeSelf)
            {
                _enemyInfo.SetActive(false);
            }

        }
    }

    // UI�̈ʒu���X�V����
    private void UpdateLockOnUIPosition()
    {
        var cameraTransform = Camera.main.transform;

        // �J�����̌����x�N�g��
        var cameraDir = cameraTransform.forward;

        // �I�u�W�F�N�g�̈ʒu
        var targetWorldPos = _enemyChecker.NowEnemy.transform.position + _worldOffset;

        // �J��������^�[�Q�b�g�ւ̃x�N�g��
        var targetDir = targetWorldPos - cameraTransform.position;

        // ���ς��g���ăJ�����O�����ǂ����𔻒�
        var isFront = Vector3.Dot(cameraDir, targetDir) > 0;

        // �J�����O���Ȃ�UI�\���A����Ȃ��\��
        _targetUI.gameObject.SetActive(isFront);
        if (!isFront) return;

        // �I�u�W�F�N�g�̃��[���h���W���X�N���[�����W�ϊ�
        var targetScreenPos = _targetCamera.WorldToScreenPoint(targetWorldPos);

        // �X�N���[�����W�ϊ���UI���[�J�����W�ϊ�
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _parentUI,
            targetScreenPos,
            null,
            out var uiLocalPos
        );

        // RectTransform�̃��[�J�����W���X�V
        _targetUI.transform.localPosition = uiLocalPos;
    }

    private void EnemyInfoUIUpdata()
    {
        //�擾�����R���C�_�[�̈�ԏ�̐e�I�u�W�F�N�g���擾
        var enemyParent = _enemyChecker.NowEnemy.transform.root.gameObject;

        bool isGet = enemyParent.TryGetComponent<EnemyControl>(out EnemyControl tryGetEnemyControl);

        if (!isGet)
        {
            return;
        }

        //�G�̏��̃p�l���̐ݒ�
        _enemyInfoName.text = tryGetEnemyControl.EnemyData.EnemyName;
        _enemyInfoHp.maxValue = tryGetEnemyControl.EnemyData.MaxHp;
        _enemyInfoHp.value = tryGetEnemyControl.EnemyLife.NowHp;

        if (_enemyChecker.IsTargetting && _enemyChecker.NowEnemy != null)
        {
            _enemyInfoWeak.SetActive(true);
            IconSet(tryGetEnemyControl);
        }
        else
        {
            _enemyInfoWeak.SetActive(false);
        }


        var cameraTransform = Camera.main.transform;

        // �J�����̌����x�N�g��
        var cameraDir = cameraTransform.forward;

        // �I�u�W�F�N�g�̈ʒu
        var targetWorldPos = _enemyChecker.NowEnemy.transform.position + tryGetEnemyControl.EnemyData.UIOffSet;

        // �J��������^�[�Q�b�g�ւ̃x�N�g��
        var targetDir = targetWorldPos - cameraTransform.position;

        // ���ς��g���ăJ�����O�����ǂ����𔻒�
        var isFront = Vector3.Dot(cameraDir, targetDir) > 0;

        // �J�����O���Ȃ�UI�\���A����Ȃ��\��
        _enemyInfo.gameObject.SetActive(isFront);
        if (!isFront) return;

        // �I�u�W�F�N�g�̃��[���h���W���X�N���[�����W�ϊ�
        var targetScreenPos = _targetCamera.WorldToScreenPoint(targetWorldPos);

        // �X�N���[�����W�ϊ���UI���[�J�����W�ϊ�
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _parentUI,
            targetScreenPos,
            null,
            out var uiLocalPos
        );

        // RectTransform�̃��[�J�����W���X�V
        _enemyInfo.transform.localPosition = uiLocalPos;
    }

    public void IconSet(EnemyControl enemyControl)
    {
        _swordIcon.SetActive(false);
        _largeSwordIcon.SetActive(false);
        _twinSwordIcon.SetActive(false);
        _spearIcon.SetActive(false);
        _gunIcon.SetActive(false);
        _fireIcon.SetActive(false);
        _iceIcon.SetActive(false);
        _thenderIcon.SetActive(false);

        foreach (var a in enemyControl.EnemyData.WeakWeapons)
        {
            if (a == AllWeapons.WeaponType.Sword)
            {
                _spearIcon.SetActive(true);
            }
            else if (a == AllWeapons.WeaponType.LargeSword)
            {
                _largeSwordIcon.SetActive(true);
            }
            else if (a == AllWeapons.WeaponType.twinSword)
            {
                _twinSwordIcon.SetActive(true);
            }
            else if (a == AllWeapons.WeaponType.Spear)
            {
                _spearIcon.SetActive(true);
            }
            else if (a == AllWeapons.WeaponType.Gun)
            {
                _gunIcon.SetActive(true);
            }
        }

        foreach (var a in enemyControl.EnemyData.WeakAttributes)
        {
            if (a == Attribute.Fire)
            {
                _fireIcon.SetActive(true);
            }
            else if (a == Attribute.Ice)
            {
                _iceIcon.SetActive(true);
            }
            else if (a == Attribute.Thunder)
            {
                _thenderIcon.SetActive(true);
            }
        }

    }
}
