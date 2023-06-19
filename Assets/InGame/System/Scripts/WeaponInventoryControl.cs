using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[System.Serializable]
public class WeaponInventoryControl
{
    /// <summary>初期装備の武器</summary>
    [SerializeField] private ScritableWeaponData[] _myFirsrSetWeapons = new ScritableWeaponData[4];

    [SerializeField] private List<ScritableWeaponData> _firstSetWeapons = new List<ScritableWeaponData>();

    [SerializeField] private WeaponSetting _weaponSetting;

    private PlayerInformationManager _infoManager;

    /// <summary>持ってる武器一覧</summary>
    private List<ScritableWeaponData> _getWeapons = new List<ScritableWeaponData>();

    private ScritableWeaponData[] _mySetWeapons = new ScritableWeaponData[4];

    /// <summary>現在選択している武器の枠</summary>
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

            //設定ボタンの見た目変更
            _infoManager.WeaponSetUIControl.SetWeaponButtunChangeImage(i + 1, _myFirsrSetWeapons[i]);

            //インゲームのアイコン変更
            _inGameWeaponSelect.SetIcon(i + 1, _myFirsrSetWeapons[i]);
        }   //初期武器の設定

        foreach (var a in _firstSetWeapons)
        {
            AddWeapon(a);
        }   //初期所持武器の設定


    }   //初期化処理

    /// <summary>武器の追加処理</summary>
    /// <param name="weapon">追加したい武器</param>
    public void AddWeapon(ScritableWeaponData weapon)
    {
        if (!_getWeapons.Contains(weapon))
        {
            _getWeapons.Add(weapon);
            _infoManager.WeaponSetUIControl.AddWeaponPanel(weapon);
        }   //武器を追加
    }

    /// <summary>セットしたい場所を選択</summary>
    /// <param name="num"></param>
    public void SetWeaponSpace(int num)
    {
        _selectSpace = num;
    }

    /// <summary>武器をセット</summary>
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

                    //設定ボタンの見た目変更
                    _infoManager.WeaponSetUIControl.SetWeaponButtunChangeImage(num + 1, null);
                    //インゲームのアイコン変更
                    _inGameWeaponSelect.SetIcon(num + 1, null);

                }   //別の枠に装備してある武器だったらその枠はなしにする
            }
        }
        _mySetWeapons[_selectSpace - 1] = weapon;
        _weaponSetting.SetWeapon(_selectSpace, _mySetWeapons[_selectSpace - 1]);
        //設定ボタンの見た目変更
        _infoManager.WeaponSetUIControl.SetWeaponButtunChangeImage(_selectSpace, weapon);
        //インゲームのアイコン変更
        _inGameWeaponSelect.SetIcon(_selectSpace, weapon);
    }


    void Start()
    {

    }


    void Update()
    {

    }


}
