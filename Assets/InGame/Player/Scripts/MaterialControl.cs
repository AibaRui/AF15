using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MaterialControl 
{

    [Header("プレイヤーのマテリアルを設定するオブジェクト")]
    [SerializeField] private Renderer _mateiralPlayerObject;

    [Header("プレイヤーの通常のマテリアル")]
    [SerializeField] private Material _nomalPlayerMaterial;

    [Header("プレイヤーの透明なマテリアル")]
    [SerializeField] private Material _clearPlayerMaterial;

    private PlayerControl _playerControl;

    public enum PlayerMaterialType
    {
        /// <summary>通常</summary>
        Nomal,
        /// <summary>透明</summary>
        Clear,

    }

    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }


    public void ChangeMaterial(PlayerMaterialType type)
    {
        Material setMaterial = default;
        if(type == PlayerMaterialType.Nomal)
        {
            setMaterial = _nomalPlayerMaterial;
        }
        else if(type == PlayerMaterialType.Clear)
        {
            setMaterial = _clearPlayerMaterial;
        }

        _mateiralPlayerObject.material = setMaterial;
    }

}
