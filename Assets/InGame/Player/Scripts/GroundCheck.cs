using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GroundCheck
{
    [Header("地面のチェック")]
    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    private Vector3 _size;

    [Header("地面に近いことを示すBoxチェックのOffSet")]
    [SerializeField]
    private Vector3 _offsetBoxNearGround;
    [Header("地面に近いことを示すBoxチェックのBoxの大きさ")]
    [SerializeField]
    private Vector3 _sizeBoxNearGround;


    [SerializeField]
    private LayerMask _targetLayer;

    [SerializeField]
    private bool _isDrawGizmo = true;

    private PlayerControl _playerControl;

    /// <summary>
    /// 初期化処理、このクラスを使用するときは、
    /// 最初にこの処理を実行する。
    /// </summary>
    /// <param name="origin"> 原点 </param>
    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }

    /// <summary>
    /// 範囲内にあるコライダーを取得する
    /// </summary>
    /// <returns> 移動方向 :正の値, 負の値 </returns>
    public Collider[] GetCollider()
    {
        var posX = _playerControl.PlayerT.position.x + _offset.x;
        var posY = _playerControl.PlayerT.position.y + _offset.y;
        var posz = _playerControl.PlayerT.position.z + _offset.z;

        return Physics.OverlapBox(new Vector3(posX, posY, posz), _size, Quaternion.identity, _targetLayer);
    }

    /// <summary>
    /// 範囲内にあるコライダーを取得する
    /// </summary>
    /// <returns> 移動方向 :正の値, 負の値 </returns>
    public bool IsHit()
    {
        if (GetCollider().Length > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsHitNearGround()
    {
        var posX = _playerControl.PlayerT.position.x + _offsetBoxNearGround.x;
        var posY = _playerControl.PlayerT.position.y + _offsetBoxNearGround.y;
        var posZ = _playerControl.PlayerT.position.z + _offsetBoxNearGround.z;

        var hit = Physics.CheckBox(new Vector3(posX, posY, posZ), _sizeBoxNearGround, Quaternion.identity, _targetLayer);

        return hit;
    }






    /// <summary>
    /// Gizmoに範囲を描画する
    /// </summary>
    /// <param name="origin"> 当たり判定の中央を表すTransform </param>
    public void OnDrawGizmos(Transform origin)
    {
        if (_isDrawGizmo)
        {

            Gizmos.color = Color.green;
            var posXNearGround = origin.position.x + _offsetBoxNearGround.x;
            var posYNearGround = origin.position.y + _offsetBoxNearGround.y;
            var posZNearGround = origin.position.z + _offsetBoxNearGround.z;
            Gizmos.DrawWireCube(new Vector3(posXNearGround, posYNearGround, posZNearGround), _sizeBoxNearGround);


            Gizmos.color = Color.red;
            var posX = origin.position.x + _offset.x;
            var posY = origin.position.y + _offset.y;
            var posz = origin.position.z + _offset.z;
            Gizmos.DrawCube(new Vector3(posX, posY, posz), _size);


            //Gizmos.color = Color.yellow;
            //var posXS = origin.position.x + _offsetBoxSwingToGroundLong.x;
            //var posYS = origin.position.y + _offsetBoxSwingToGroundLong.y;
            //var poszS = origin.position.z + _offsetBoxSwingToGroundLong.z;


            //Gizmos.DrawCube(new Vector3(posXS, posYS, poszS), _size);

        }
    }
}

