using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WeaponSetting : MonoBehaviour
{
    [Header("武器の最大数")]
    [SerializeField] private int _maxWeaponNumber = 4;

    [Header("武器をしまう場所の大本")]
    [SerializeField] private Transform _weaponPositionCenter;

    [Header("武器をしまう場所")]
    [SerializeField] private List<Transform> _weaponPosition = new List<Transform>();

    [Header("現在のスロットの武器")]
    //private List<ScritableWeaponData> _setWeapons = new List<ScritableWeaponData>();
    private ScritableWeaponData[] _setWeapons = new ScritableWeaponData[4];

    [SerializeField] private ParticleSystem _particleOnSelectWeapon;

    [SerializeField] private float _radias = 1;

    private Dictionary<string, GameObject> _useW = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> _lookWeaponObjects = new Dictionary<string, GameObject>();

    /// <summary>選択時に登場する見せかけの武器</summary>
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
        //現在持っている武器を非表示
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
            //実際に使う武器を生成
            var useWeaponObject = Instantiate(weapon.WeaponObject);
            useWeaponObject.SetActive(false);
            useWeaponObject.transform.SetParent(_playerControl.AnimControl.ModelRightHandPos);
            Quaternion r = Quaternion.Euler(0, 0, 0);
            useWeaponObject.transform.localRotation = r;

            _useW.Add(weapon.WeaponName, useWeaponObject);
        }

        if (!_lookWeaponObjects.ContainsKey(weapon.WeaponName))
        {
            //見かけ上の武器を生成
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
            //現在使っていた武器を非表示にする
            _nowUseWeaponObject.SetActive(false);

            //現在使う武器の情報を変更
            _nowWeapon = _playerControl.WeaponSetting.MySetWeapon[_nowSelectWeaponIndex];

            //現在使う武器を変更
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

        //武器が1つしか無い状態の対処
        if (setSlotNum == _nowSelectWeaponIndex || count == 3)
        {
            //使っていた武器の値をリセット
            _playerControl.AllWeapons.WeaponGetActionData(_playerControl.WeaponSetting.NowWeapon.WeaponType).ReSetValue();

            //現在使っていた武器を非表示にする
            _nowUseWeaponObject.SetActive(false);

            //現在使う武器の情報を変更
            _nowWeapon = _playerControl.WeaponSetting.MySetWeapon[setSlotNum - 1];

            //現在使う武器を変更
            _nowUseWeaponObject = _useW[_nowWeapon.WeaponName];

            _isChageWeapon = false;
        }


    }


    public void AddWeapon(ScritableWeaponData weapon, int num)
    {
        _setWeapons[num - 1] = weapon;
        if (weapon == null) return;



        //実際に使う武器を生成
        var useWeaponObject = Instantiate(weapon.WeaponObject);

        //非表示
        useWeaponObject.SetActive(false);

        //プレイヤーの手の位置にもっていく
        useWeaponObject.transform.SetParent(_playerControl.AnimControl.ModelRightHandPos);

        Quaternion r = Quaternion.Euler(0, 0, 0);

        useWeaponObject.transform.localRotation = r;

        _useW.Add(weapon.WeaponName, useWeaponObject);

        //見かけ上の武器を生成
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

        // 360度を物の個数で等分した角度を計算
        float angleInterval = 360f / _maxWeaponNumber;

        // 物を円状に配置
        for (int i = 0; i < _maxWeaponNumber; i++)
        {
            // 物体の角度を計算
            float angle = i * angleInterval;

            // 物体の位置を計算
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


    /// <summary>武器を入れ替えたかどうかを確認</summary>
    public void WeaponSelectInput()
    {
        if (_playerControl.ShiftBreak.IsShiftBreake || _playerControl.PanelStack.IsOpen) return;

        _isWeaponChangeAnimation = false;

        if (_playerControl.InputManager.Weapon1 && _nowSelectWeaponIndex != 0 && _lookWeapons[0] != null)
        {
            _playerControl.SoundManager.PlaySound(AudioType.Selsect);

            //見せかけの武器を表示
            foreach (var a in _lookWeapons)
            {
                if (a != null)
                {
                    a.SetActive(true);
                }
            }

            //見せかけの武器の回転の経過時間をリセット
            elapsedTime = 0;

            //武器の番号を決定
            _nowSelectWeaponIndex = 0;

            //次の武器をセット
            _nextWeapon = _setWeapons[_nowSelectWeaponIndex];


            _isWeaponChangeAnimation = true;

            _isChangeLookWeaponRotation = true;

            _isChageWeapon = true;
            _inGameWeaponSelect.SelectWeaponUI(_nowSelectWeaponIndex);
        }
        else if (_playerControl.InputManager.Weapon2 && _nowSelectWeaponIndex != 1 && _lookWeapons[1] != null)
        {
            _playerControl.SoundManager.PlaySound(AudioType.Selsect);
            //見せかけの武器を表示

            foreach (var weapon in _lookWeapons)
            {
                if (weapon != null)
                {
                    weapon.SetActive(true);
                }
            }

            //見せかけの武器の回転の経過時間をリセット
            elapsedTime = 0;

            //武器の番号を決定
            _nowSelectWeaponIndex = 1;

            //次の武器をセット
            _nextWeapon = _setWeapons[_nowSelectWeaponIndex];

            _isWeaponChangeAnimation = true;

            _isChangeLookWeaponRotation = true;

            _isChageWeapon = true;
            _inGameWeaponSelect.SelectWeaponUI(_nowSelectWeaponIndex);
        }
        else if (_playerControl.InputManager.Weapon3 && _nowSelectWeaponIndex != 2 && _lookWeapons[2] != null)
        {
            _playerControl.SoundManager.PlaySound(AudioType.Selsect);
            //見せかけの武器を表示
            foreach (var weapon in _lookWeapons)
            {
                if (weapon != null)
                {
                    weapon.SetActive(true);
                }
            }

            //見せかけの武器の回転の経過時間をリセット
            elapsedTime = 0;

            //武器の番号を決定
            _nowSelectWeaponIndex = 2;

            //次の武器をセット
            _nextWeapon = _setWeapons[_nowSelectWeaponIndex];

            _isWeaponChangeAnimation = true;

            _isChangeLookWeaponRotation = true;

            _isChageWeapon = true;
            _inGameWeaponSelect.SelectWeaponUI(_nowSelectWeaponIndex);
        }
        else if (_playerControl.InputManager.Weapon4 && _nowSelectWeaponIndex != 3 && _lookWeapons[3] != null)
        {
            _playerControl.SoundManager.PlaySound(AudioType.Selsect);
            //見せかけの武器を表示
            foreach (var weapon in _lookWeapons)
            {
                if (weapon != null)
                {
                    weapon.SetActive(true);
                }
            }

            //見せかけの武器の回転の経過時間をリセット
            elapsedTime = 0;

            //武器の番号を決定
            _nowSelectWeaponIndex = 3;

            //次の武器をセット
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

            // 経過時間を更新
            elapsedTime += Time.deltaTime;

            // 経過時間に対する回転の割合を計算
            float t = Mathf.Clamp01(elapsedTime / rotationTime);

            // 回転を補間して適用
            _weaponPositionCenter.transform.localRotation = Quaternion.Slerp(startRotation, endRotation, t);

            // 回転完了時の処理
            if (t >= 1f)
            {
                foreach (var weapon in _lookWeapons)
                {
                    if (weapon != null)
                    {
                        weapon.SetActive(false);
                    }
                }

                // 最終的な回転を適用
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

        //使っていた武器の値をリセット
        _playerControl.AllWeapons.WeaponGetActionData(_playerControl.WeaponSetting.NowWeapon.WeaponType).ReSetValue();

        //現在使っていた武器を非表示にする
        _nowUseWeaponObject.SetActive(false);

        //現在使う武器の情報を変更
        _nowWeapon = _playerControl.WeaponSetting.MySetWeapon[_nowSelectWeaponIndex];

        //現在使う武器を変更
        _nowUseWeaponObject = _useW[_nowWeapon.WeaponName];

        _isChageWeapon = false;

    }



    /// <summary>様々なアクション終了時に武器が切り替えられたかどうかを確認する</summary>
    /// <returns>武器が変更されたかどうか</returns>
    public bool CheckWeapon()
    {
        if (_isChageWeapon && _nowWeapon != _playerControl.WeaponSetting.MySetWeapon[_nowSelectWeaponIndex])
        {
            //使っていた武器の値をリセット
            _playerControl.AllWeapons.WeaponGetActionData(_playerControl.WeaponSetting.NowWeapon.WeaponType).ReSetValue();

            //現在使っていた武器を非表示にする
            _nowUseWeaponObject.SetActive(false);

            //現在使う武器の情報を変更
            _nowWeapon = _playerControl.WeaponSetting.MySetWeapon[_nowSelectWeaponIndex];

            //現在使う武器を変更
            _nowUseWeaponObject = _useW[_nowWeapon.WeaponName];

            _isChageWeapon = false;

            return true;
        }

        return false;
    }

    /// <summary>武器を取り出すか非表示にするか</summary>
    /// <param name="isActive"></param>
    public void WeaponActives(bool isActive)
    {

        if (isActive)
        {
            if (_nowUseWeaponObject.activeSelf)
            {
                return;
            }

            //武器を出現
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
