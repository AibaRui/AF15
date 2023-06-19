using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MaterialControl 
{

    [Header("�v���C���[�̃}�e���A����ݒ肷��I�u�W�F�N�g")]
    [SerializeField] private Renderer _mateiralPlayerObject;

    [Header("�v���C���[�̒ʏ�̃}�e���A��")]
    [SerializeField] private Material _nomalPlayerMaterial;

    [Header("�v���C���[�̓����ȃ}�e���A��")]
    [SerializeField] private Material _clearPlayerMaterial;

    private PlayerControl _playerControl;

    public enum PlayerMaterialType
    {
        /// <summary>�ʏ�</summary>
        Nomal,
        /// <summary>����</summary>
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
