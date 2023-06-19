using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformationManager : MonoBehaviour
{
    [Header("•Ší‚Ìˆê——")]
    [SerializeField] private WeaponInventoryControl _weaponInventory;

    [SerializeField] private WeaponSetUIControl _weaponSetUIControl;

    [SerializeField] private ItemGetUIControl _itemGetUIControl;

    private static PlayerInformationManager _instance;

    private PlayerControl _playerControl;

    public WeaponSetUIControl WeaponSetUIControl => _weaponSetUIControl;
    public PlayerControl PlayerControl => _playerControl;
    public WeaponInventoryControl WeaponInventoryControl => _weaponInventory;
    public ItemGetUIControl ItemGetUIControl => _itemGetUIControl;
    public static PlayerInformationManager Instance => _instance;

    private void Awake()
    {
        _instance = this;
        _weaponInventory.Init(this);
    }

    public void SelectWeaponNum(int num)
    {
        _weaponInventory.SetWeaponSpace(num);
    }
}
