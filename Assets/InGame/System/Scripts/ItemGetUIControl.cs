using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGetUIControl : MonoBehaviour
{
    [Header("ñ⁄àÛÇÃUI")]
    [SerializeField] private GameObject _signPanel;

    [SerializeField] private GameObject _getItemPanel;
    [SerializeField] private Text _text;
    [SerializeField] private Image _image;

    [SerializeField] private PlayerControl _playerControl;
    [SerializeField] private WeaponSetUIControl _weaponSetUIControl;

    public void SetActiveFalse()
    {
        _signPanel.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (_playerControl.PickUp.IsCanPickUpÅ@&& _playerControl.EnemyCheck.NowEnemy==null)
        {
            if (_playerControl.PickUp.Collider == null)
            {
                _signPanel.SetActive(false);
                return;
            }

            if (!_signPanel.activeSelf)
            {
                _signPanel.SetActive(true);
            }
            _signPanel.transform.position = _playerControl.PickUp.Collider.gameObject.transform.position;
        }
        else
        {
            if (_signPanel.activeSelf)
            {
                _signPanel.SetActive(false);
            }
        }
    }


    public void OnGetItemPanel(ScritableWeaponData weaponData)
    {
        _text.text = $"{weaponData.WeaponName}: ÇéËÇ…ì¸ÇÍÇ‹ÇµÇΩ!";

        _image.sprite = _weaponSetUIControl.GetIconData(weaponData.WeaponType);

        _getItemPanel.SetActive(true);
    }

    public void OffGetItemPanel()
    {
        _getItemPanel.SetActive(false);
    }


}
