using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[System.Serializable]
public class WeaponInventoryControl
{
    /// <summary>���������̕���</summary>
    [SerializeField] private ScritableWeaponData[] _myFirsrSetWeapons = new ScritableWeaponData[4];

    [SerializeField] private List<ScritableWeaponData> _firstSetWeapons = new List<ScritableWeaponData>();

    [SerializeField] private WeaponSetting _weaponSetting;

    private PlayerInformationManager _infoManager;

    /// <summary>�����Ă镐��ꗗ</summary>
    private List<ScritableWeaponData> _getWeapons = new List<ScritableWeaponData>();

    private ScritableWeaponData[] _mySetWeapons = new ScritableWeaponData[4];

    /// <summary>���ݑI�����Ă��镐��̘g</summary>
    private int _selectSpace;

    [SerializeField] private InGameWeaponSelect _inGameWeaponSelect; 

    public ScritableWeaponData[] MySetWeapons => _mySetWeapons;

    public void Init(PlayerInformationManager informationManager)
    {
        _infoManager = informationManager;

        _mySetWeapons = _myFirsrSetWeapons;

        for (int i = 0; i < 4; i++)
        {
            AddWeapon(_myFirsrSetWeapons[i]);

            //�ݒ�{�^���̌����ڕύX
            _infoManager.WeaponSetUIControl.SetWeaponButtunChangeImage(i + 1, _myFirsrSetWeapons[i]);

            //�C���Q�[���̃A�C�R���ύX
            _inGameWeaponSelect.SetIcon(i + 1, _myFirsrSetWeapons[i]);
        }   //��������̐ݒ�

        foreach (var a in _firstSetWeapons)
        {
            AddWeapon(a);
        }   //������������̐ݒ�


    }   //����������

    /// <summary>����̒ǉ�����</summary>
    /// <param name="weapon">�ǉ�����������</param>
    public void AddWeapon(ScritableWeaponData weapon)
    {
        if (!_getWeapons.Contains(weapon))
        {
            _getWeapons.Add(weapon);
            _infoManager.WeaponSetUIControl.AddWeaponPanel(weapon);
        }   //�����ǉ�
    }

    /// <summary>�Z�b�g�������ꏊ��I��</summary>
    /// <param name="num"></param>
    public void SetWeaponSpace(int num)
    {
        _selectSpace = num;
    }

    /// <summary>������Z�b�g</summary>
    /// <param name="weapon"></param>
    public void SetWeapon(ScritableWeaponData weapon)
    {
        foreach (var a in _mySetWeapons)
        {
            if (a == weapon)
            {
                int num = Array.IndexOf(_mySetWeapons, weapon);

                if (num != _selectSpace - 1)
                {
                    _mySetWeapons[num] = null;
                    _weaponSetting.SetWeapon(num + 1, _mySetWeapons[num]);

                    //�ݒ�{�^���̌����ڕύX
                    _infoManager.WeaponSetUIControl.SetWeaponButtunChangeImage(num + 1, null);
                    //�C���Q�[���̃A�C�R���ύX
                    _inGameWeaponSelect.SetIcon(num + 1, null);

                }   //�ʂ̘g�ɑ������Ă��镐�킾�����炻�̘g�͂Ȃ��ɂ���
            }
        }
        _mySetWeapons[_selectSpace - 1] = weapon;
        _weaponSetting.SetWeapon(_selectSpace, _mySetWeapons[_selectSpace - 1]);
        //�ݒ�{�^���̌����ڕύX
        _infoManager.WeaponSetUIControl.SetWeaponButtunChangeImage(_selectSpace, weapon);
        //�C���Q�[���̃A�C�R���ύX
        _inGameWeaponSelect.SetIcon(_selectSpace, weapon);
    }


    void Start()
    {

    }


    void Update()
    {

    }


}
