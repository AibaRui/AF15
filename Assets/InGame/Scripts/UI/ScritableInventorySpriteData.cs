using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "InventorySpriteData")]
public class ScritableInventorySpriteData : ScriptableObject
{
    [Header("剣のアイコン")]
    [SerializeField] private Sprite _swordIconReal;
    [SerializeField] private Sprite _swordIcon;

    [Header("大剣のアイコン")]
    [SerializeField] private Sprite _largeSwordIconReal;
    [SerializeField] private Sprite _largeSwordIcon;

    [Header("双剣のアイコン")]
    [SerializeField] private Sprite _twinSwordIconReal;
    [SerializeField] private Sprite _twinSwordIcon;

    [Header("槍のアイコン")]
    [SerializeField] private Sprite _spearIconReal;
    [SerializeField] private Sprite _spearIcon;

    [Header("銃のアイコン")]
    [SerializeField] private Sprite _gunIconReal;
    [SerializeField] private Sprite _gunIcon;

    [Header("火属性のアイコン")]
    [SerializeField] private Sprite _fireIcon;

    [Header("氷属性のアイコン")]
    [SerializeField] private Sprite _iceIcon;

    [Header("雷属性のアイコン")]
    [SerializeField] private Sprite _thnderIcon;

    public Sprite FireIcon => _fireIcon;
    public Sprite IceIcon => _iceIcon;
    public Sprite ThenderIcon => _thnderIcon;

    public Sprite SwordIconReal => _swordIconReal;
    public Sprite SwordIcon => _swordIcon;

    public Sprite LargeSwordIconReal => _largeSwordIconReal;
    public Sprite LargeSwordIcon => _largeSwordIcon;

    public Sprite TwinSwordIconReal => _twinSwordIconReal;
    public Sprite TwinSwordIcon => _twinSwordIcon;


    public Sprite SpearRealIcon => _spearIconReal;
    public Sprite SpearIcon => _spearIcon;

    public Sprite GunRealIcon => _gunIconReal;
    public Sprite GunIcon => _gunIcon;

}
