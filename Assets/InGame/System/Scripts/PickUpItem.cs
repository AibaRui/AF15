using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour,IGetable
{
    [SerializeField] private ScritableWeaponData _weaponData;

    void IGetable.PickUpItem()
    {
        PlayerInformationManager.Instance.WeaponInventoryControl.AddWeapon(_weaponData);
        PlayerInformationManager.Instance.ItemGetUIControl.OnGetItemPanel(_weaponData);
        Destroy(gameObject);
    }
}
