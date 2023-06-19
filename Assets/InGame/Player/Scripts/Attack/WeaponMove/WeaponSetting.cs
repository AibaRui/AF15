using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WeaponSetting : MonoBehaviour
{
    [Header("����̍ő吔")]
    [SerializeField] private int _maxWeaponNumber = 4;

    [Header("��������܂��ꏊ�̑�{")]
    [SerializeField] private Transform _weaponPositionCenter;

    [Header("��������܂��ꏊ")]
    [SerializeField] private List<Transform> _weaponPosition = new List<Transform>();

    [Header("���݂̃X���b�g�̕���")]
    //private List<ScritableWeaponData> _setWeapons = new List<ScritableWeaponData>();
    private ScritableWeaponData[] _setWeapons = new ScritableWeaponData[4];

    [SerializeField] private ParticleSystem _particleOnSelectWeapon;

    [SerializeField] private float _radias = 1;

    private Dictionary<string, GameObject> _useW = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> _lookWeaponObjects = new Dictionary<string, GameObject>();

    /// <summary>�I�����ɓo�ꂷ�錩�������̕���</summary>
    private GameObject[] _lookWeapons = new GameObject[4];

    private GameObject _nowUseWeaponObject;

    private int _weaponIndex = 0;

    private ScritableWeaponData _nowWeapon;

    private ScritableWeaponData _nextWeapon;


    private int _nowSelectWeaponIndex = 0;

    private bool _isChageWeapon = false;

    [SerializeField] private InGameWeaponSelect _inGameWeaponSelect;
    [SerializeField] private PlayerControl _playerControl;

    private bool _nowChangeWeaponAnimation = false;

    private bool _isChangeLookWeaponRotation = false;

    private float rotationTime = 0.5f;

    private float elapsedTime;

    private bool _isWeaponChangeAnimation = false;

    public GameObject NowUseWeaponObject => _nowUseWeaponObject;

    public bool IsWeaponChangeAnimation { get => _isWeaponChangeAnimation; set => _isWeaponChangeAnimation = value; }

    public bool IsChangeWeapon { get => _isChageWeapon; set => _isChageWeapon = value; }

    public ScritableWeaponData[] MySetWeapon => _setWeapons;
    public ScritableWeaponData NowWeapon { get => _nowWeapon; set => _nowWeapon = value; }

    public bool NowChangeWeaponAnimation { get => _nowChangeWeaponAnimation; set => _nowChangeWeaponAnimation = value; }

    private void Awake()
    {

    }

    public void SetWeapon(int setSlotNum, ScritableWeaponData weapon)
    {
        //���ݎ����Ă��镐����\��
        _useW[_nowWeapon.WeaponName].SetActive(false);

        if (weapon == null)
        {
            if (_lookWeapons[setSlotNum - 1] != null)
            {
                _lookWeapons[setSlotNum - 1].SetActive(false);
            }

            _lookWeapons[setSlotNum - 1] = null;
            _setWeapons[setSlotNum - 1] = null;
            return;
        }

        _playerControl.AllWeapons.WeaponGetActionData(_playerControl.WeaponSetting.NowWeapon.WeaponType).UseEndWeapon();

        if (!_useW.ContainsKey(weapon.WeaponName))
        {
            //���ۂɎg������𐶐�
            var useWeaponObject = Instantiate(weapon.WeaponObject);
            useWeaponObject.SetActive(false);
            useWeaponObject.transform.SetParent(_playerControl.AnimControl.ModelRightHandPos);
            Quaternion r = Quaternion.Euler(0, 0, 0);
            useWeaponObject.transform.localRotation = r;

            _useW.Add(weapon.WeaponName, useWeaponObject);
        }

        if (!_lookWeaponObjects.ContainsKey(weapon.WeaponName))
        {
            //��������̕���𐶐�
            var lookWeaponObject = Instantiate(weapon.WeaponLookObject);
            lookWeaponObject.transform.SetParent(_weaponPosition[setSlotNum - 1]);
            _lookWeapons[setSlotNum - 1] = lookWeaponObject;
            _lookWeapons[setSlotNum - 1].transform.localPosition = Vector3.zero;
            lookWeaponObject.SetActive(false);

            _lookWeaponObjects.Add(weapon.WeaponName, lookWeaponObject);
        }

        if (_lookWeapons[setSlotNum - 1] != null)
        {
            _lookWeapons[setSlotNum - 1].SetActive(false);
        }
        _lookWeapons[setSlotNum - 1] = _lookWeaponObjects[weapon.WeaponName];
        _lookWeapons[setSlotNum - 1].transform.SetParent(_weaponPosition[setSlotNum - 1]);
        _lookWeapons[setSlotNum - 1].transform.localPosition = Vector3.zero;


        _setWeapons[setSlotNum - 1] = weapon;

        if (setSlotNum-1 == _nowSelectWeaponIndex)
        {
            //���ݎg���Ă���������\���ɂ���
            _nowUseWeaponObject.SetActive(false);

            //���ݎg������̏���ύX
            _nowWeapon = _playerControl.WeaponSetting.MySetWeapon[_nowSelectWeaponIndex];

            //���ݎg�������ύX
            _nowUseWeaponObject = _useW[_nowWeapon.WeaponName];
        }

        int count = 0;
        foreach (var w in _playerControl.WeaponSetting.MySetWeapon)
        {
            if (w == null)
            {
                count++;
            }
        }

        //���킪1����������Ԃ̑Ώ�
        if (setSlotNum == _nowSelectWeaponIndex || count == 3)
        {
            //�g���Ă�������̒l�����Z�b�g
            _playerControl.AllWeapons.WeaponGetActionData(_playerControl.WeaponSetting.NowWeapon.WeaponType).ReSetValue();

            //���ݎg���Ă���������\���ɂ���
            _nowUseWeaponObject.SetActive(false);

            //���ݎg������̏���ύX
            _nowWeapon = _playerControl.WeaponSetting.MySetWeapon[setSlotNum - 1];

            //���ݎg�������ύX
            _nowUseWeaponObject = _useW[_nowWeapon.WeaponName];

            _isChageWeapon = false;
        }


    }


    public void AddWeapon(ScritableWeaponData weapon, int num)
    {
        _setWeapons[num - 1] = weapon;
        if (weapon == null) return;



        //���ۂɎg������𐶐�
        var useWeaponObject = Instantiate(weapon.WeaponObject);

        //��\��
        useWeaponObject.SetActive(false);

        //�v���C���[�̎�̈ʒu�ɂ����Ă���
        useWeaponObject.transform.SetParent(_playerControl.AnimControl.ModelRightHandPos);

        Quaternion r = Quaternion.Euler(0, 0, 0);

        useWeaponObject.transform.localRotation = r;

        _useW.Add(weapon.WeaponName, useWeaponObject);

        //��������̕���𐶐�
        var lookWeaponObject = Instantiate(weapon.WeaponLookObject);

        lookWeaponObject.transform.SetParent(_weaponPosition[num - 1]);
        _lookWeapons[num - 1] = lookWeaponObject;
        _lookWeapons[num - 1].transform.localPosition = Vector3.zero;

        lookWeaponObject.SetActive(false);

        if (num == 1)
        {
            _nowWeapon = _setWeapons[0];

            _nowUseWeaponObject = _useW[weapon.WeaponName];
        }

        _lookWeaponObjects.Add(weapon.WeaponName, lookWeaponObject);
    }

    public void UseWeapon()
    {

    }



    void Start()
    {
        Vector3 centerPosition = _weaponPositionCenter.position;

        // 360�x�𕨂̌��œ��������p�x���v�Z
        float angleInterval = 360f / _maxWeaponNumber;

        // �����~��ɔz�u
        for (int i = 0; i < _maxWeaponNumber; i++)
        {
            // ���̂̊p�x���v�Z
            float angle = i * angleInterval;

            // ���̂̈ʒu���v�Z
            Vector3 position = Quaternion.Euler(0f, angle, 0f) * Vector3.forward * _radias;

            _weaponPosition[i].localPosition = position;
        }

        if (PlayerInformationManager.Instance.WeaponInventoryControl.MySetWeapons != null)
        {
            for (int i = 1; i <= 4; i++)
            {
                AddWeapon(PlayerInformationManager.Instance.WeaponInventoryControl.MySetWeapons[i - 1], i);
            }
        }
    }

    void Update()
    {
        if (!GameManager.Instance.PauseManager.IsPause)
            WeaponSelectInput();
    }


    /// <summary>��������ւ������ǂ������m�F</summary>
    public void WeaponSelectInput()
    {
        if (_playerControl.ShiftBreak.IsShiftBreake || _playerControl.PanelStack.IsOpen) return;

        _isWeaponChangeAnimation = false;

        if (_playerControl.InputManager.Weapon1 && _nowSelectWeaponIndex != 0 && _lookWeapons[0] != null)
        {
            _playerControl.SoundManager.PlaySound(AudioType.Selsect);

            //���������̕����\��
            foreach (var a in _lookWeapons)
            {
                if (a != null)
                {
                    a.SetActive(true);
                }
            }

            //���������̕���̉�]�̌o�ߎ��Ԃ����Z�b�g
            elapsedTime = 0;

            //����̔ԍ�������
            _nowSelectWeaponIndex = 0;

            //���̕�����Z�b�g
            _nextWeapon = _setWeapons[_nowSelectWeaponIndex];


            _isWeaponChangeAnimation = true;

            _isChangeLookWeaponRotation = true;

            _isChageWeapon = true;
            _inGameWeaponSelect.SelectWeaponUI(_nowSelectWeaponIndex);
        }
        else if (_playerControl.InputManager.Weapon2 && _nowSelectWeaponIndex != 1 && _lookWeapons[1] != null)
        {
            _playerControl.SoundManager.PlaySound(AudioType.Selsect);
            //���������̕����\��

            foreach (var weapon in _lookWeapons)
            {
                if (weapon != null)
                {
                    weapon.SetActive(true);
                }
            }

            //���������̕���̉�]�̌o�ߎ��Ԃ����Z�b�g
            elapsedTime = 0;

            //����̔ԍ�������
            _nowSelectWeaponIndex = 1;

            //���̕�����Z�b�g
            _nextWeapon = _setWeapons[_nowSelectWeaponIndex];

            _isWeaponChangeAnimation = true;

            _isChangeLookWeaponRotation = true;

            _isChageWeapon = true;
            _inGameWeaponSelect.SelectWeaponUI(_nowSelectWeaponIndex);
        }
        else if (_playerControl.InputManager.Weapon3 && _nowSelectWeaponIndex != 2 && _lookWeapons[2] != null)
        {
            _playerControl.SoundManager.PlaySound(AudioType.Selsect);
            //���������̕����\��
            foreach (var weapon in _lookWeapons)
            {
                if (weapon != null)
                {
                    weapon.SetActive(true);
                }
            }

            //���������̕���̉�]�̌o�ߎ��Ԃ����Z�b�g
            elapsedTime = 0;

            //����̔ԍ�������
            _nowSelectWeaponIndex = 2;

            //���̕�����Z�b�g
            _nextWeapon = _setWeapons[_nowSelectWeaponIndex];

            _isWeaponChangeAnimation = true;

            _isChangeLookWeaponRotation = true;

            _isChageWeapon = true;
            _inGameWeaponSelect.SelectWeaponUI(_nowSelectWeaponIndex);
        }
        else if (_playerControl.InputManager.Weapon4 && _nowSelectWeaponIndex != 3 && _lookWeapons[3] != null)
        {
            _playerControl.SoundManager.PlaySound(AudioType.Selsect);
            //���������̕����\��
            foreach (var weapon in _lookWeapons)
            {
                if (weapon != null)
                {
                    weapon.SetActive(true);
                }
            }

            //���������̕���̉�]�̌o�ߎ��Ԃ����Z�b�g
            elapsedTime = 0;

            //����̔ԍ�������
            _nowSelectWeaponIndex = 3;

            //���̕�����Z�b�g
            _nextWeapon = _setWeapons[_nowSelectWeaponIndex];


            _isWeaponChangeAnimation = true;

            _isChangeLookWeaponRotation = true;

            _isChageWeapon = true;

            _inGameWeaponSelect.SelectWeaponUI(_nowSelectWeaponIndex);
        }
        else if (_playerControl.InputManager.Weapon1 || _playerControl.InputManager.Weapon2 || _playerControl.InputManager.Weapon3 || _playerControl.InputManager.Weapon4)
        {
            Debug.Log("fvk");

            _isWeaponChangeAnimation = false;

            _isChageWeapon = false;
            _isChangeLookWeaponRotation = false;
        }

        if (_isChangeLookWeaponRotation)
        {
            Quaternion startRotation = _weaponPositionCenter.transform.localRotation;

            float angleInterval = 360f / _maxWeaponNumber;

            float angle = 0;

            if (_nowSelectWeaponIndex == 0)
            {
                angle = 0;
            }
            else
            {
                angle = 360 - angleInterval * _nowSelectWeaponIndex;
            }

            Quaternion endRotation = Quaternion.Euler(0, angle, 0);

            // �o�ߎ��Ԃ��X�V
            elapsedTime += Time.deltaTime;

            // �o�ߎ��Ԃɑ΂����]�̊������v�Z
            float t = Mathf.Clamp01(elapsedTime / rotationTime);

            // ��]���Ԃ��ēK�p
            _weaponPositionCenter.transform.localRotation = Quaternion.Slerp(startRotation, endRotation, t);

            // ��]�������̏���
            if (t >= 1f)
            {
                foreach (var weapon in _lookWeapons)
                {
                    if (weapon != null)
                    {
                        weapon.SetActive(false);
                    }
                }

                // �ŏI�I�ȉ�]��K�p
                _weaponPositionCenter.transform.localRotation = endRotation;

                elapsedTime = 0;

                _isChangeLookWeaponRotation = false;

                _isWeaponChangeAnimation = false;
            }
        }
    }


    public void CheckWeaponOnSelect()
    {
        _isWeaponChangeAnimation = false;

        _particleOnSelectWeapon.Play();

        //�g���Ă�������̒l�����Z�b�g
        _playerControl.AllWeapons.WeaponGetActionData(_playerControl.WeaponSetting.NowWeapon.WeaponType).ReSetValue();

        //���ݎg���Ă���������\���ɂ���
        _nowUseWeaponObject.SetActive(false);

        //���ݎg������̏���ύX
        _nowWeapon = _playerControl.WeaponSetting.MySetWeapon[_nowSelectWeaponIndex];

        //���ݎg�������ύX
        _nowUseWeaponObject = _useW[_nowWeapon.WeaponName];

        _isChageWeapon = false;

    }



    /// <summary>�l�X�ȃA�N�V�����I�����ɕ��킪�؂�ւ���ꂽ���ǂ������m�F����</summary>
    /// <returns>���킪�ύX���ꂽ���ǂ���</returns>
    public bool CheckWeapon()
    {
        if (_isChageWeapon && _nowWeapon != _playerControl.WeaponSetting.MySetWeapon[_nowSelectWeaponIndex])
        {
            //�g���Ă�������̒l�����Z�b�g
            _playerControl.AllWeapons.WeaponGetActionData(_playerControl.WeaponSetting.NowWeapon.WeaponType).ReSetValue();

            //���ݎg���Ă���������\���ɂ���
            _nowUseWeaponObject.SetActive(false);

            //���ݎg������̏���ύX
            _nowWeapon = _playerControl.WeaponSetting.MySetWeapon[_nowSelectWeaponIndex];

            //���ݎg�������ύX
            _nowUseWeaponObject = _useW[_nowWeapon.WeaponName];

            _isChageWeapon = false;

            return true;
        }

        return false;
    }

    /// <summary>��������o������\���ɂ��邩</summary>
    /// <param name="isActive"></param>
    public void WeaponActives(bool isActive)
    {

        if (isActive)
        {
            if (_nowUseWeaponObject.activeSelf)
            {
                return;
            }

            //������o��
            _nowUseWeaponObject.SetActive(true);

            _nowUseWeaponObject.transform.localPosition = Vector3.zero;
        }
        else
        {
            //  Debug.Log("e");
            _nowUseWeaponObject.SetActive(false);
            // Debug.Log(_nowUseWeaponObject.name);
        }
    }

    public void WeaponChangeGroundAnim()
    {
        _nowChangeWeaponAnimation = true;

        _playerControl.Animator.Play(_playerControl.AllWeapons.GetWeaponData(_playerControl.WeaponSetting.NowWeapon.WeaponType).SettingAnimName, -1, 0f);
        // _playerControl.Animator.SetTrigger("WeaponChange");
    }
}
