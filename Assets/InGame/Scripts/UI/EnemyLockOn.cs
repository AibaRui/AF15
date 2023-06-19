using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLockOn : MonoBehaviour
{
    [SerializeField] private EnemyChecker _enemyChecker;

    [SerializeField] private GameObject _targetUI;

    // オブジェクトを映すカメラ
    [SerializeField] private Camera _targetCamera;

    // オブジェクト位置のオフセット
    [SerializeField] private Vector3 _worldOffset;

    [Header("-----敵情報-----")]
    [Header("敵情報のパネル")]
    [SerializeField] private GameObject _enemyInfo;

    [Header("敵の名前のText")]
    [SerializeField] private Text _enemyInfoName;

    [Header("敵のHpのスライダー")]
    [SerializeField] private Slider _enemyInfoHp;


    [Header("-----アイコン-----")]

    [Header("大本。アイコン")]
    [SerializeField] private GameObject _enemyInfoWeak;

    [Header("剣のアイコン")]
    [SerializeField] private GameObject _swordIcon;

    [Header("大剣のアイコン")]
    [SerializeField] private GameObject _largeSwordIcon;

    [Header("双剣のアイコン")]
    [SerializeField] private GameObject _twinSwordIcon;

    [Header("槍のアイコン")]
    [SerializeField] private GameObject _spearIcon;

    [Header("銃のアイコン")]
    [SerializeField] private GameObject _gunIcon;

    [Header("炎のアイコン")]
    [SerializeField] private GameObject _fireIcon;

    [Header("氷のアイコン")]
    [SerializeField] private GameObject _iceIcon;

    [Header("雷のアイコン")]
    [SerializeField] private GameObject _thenderIcon;


    [SerializeField] private PlayerControl _playerControl;

    private RectTransform _parentUI;
    private void Awake()
    {
        // カメラが指定されていなければメインカメラにする
        if (_targetCamera == null)
            _targetCamera = Camera.main;

        // 親UIのRectTransformを保持
        _parentUI = _targetUI.transform.parent.GetComponent<RectTransform>();
    }

    //カメラの更新と合わせるため、Fixedで呼ぶことにする
    private void FixedUpdate()
    {
        if (_enemyChecker.NowEnemy != null)
        {
            if (!_targetUI.activeSelf)
            {
                _targetUI.SetActive(true);
            }

            if (!_enemyInfo.activeSelf)
            {
                _enemyInfo.SetActive(true);
            }
            UpdateLockOnUIPosition();
            EnemyInfoUIUpdata();

        }
        else
        {
            //ロックオンする敵が居なかったらマーカーを非表示
            if (_targetUI.activeSelf)
            {
                _targetUI.SetActive(false);
            }

            if (_enemyInfo.activeSelf)
            {
                _enemyInfo.SetActive(false);
            }

        }
    }

    // UIの位置を更新する
    private void UpdateLockOnUIPosition()
    {
        var cameraTransform = Camera.main.transform;

        // カメラの向きベクトル
        var cameraDir = cameraTransform.forward;

        // オブジェクトの位置
        var targetWorldPos = _enemyChecker.NowEnemy.transform.position + _worldOffset;

        // カメラからターゲットへのベクトル
        var targetDir = targetWorldPos - cameraTransform.position;

        // 内積を使ってカメラ前方かどうかを判定
        var isFront = Vector3.Dot(cameraDir, targetDir) > 0;

        // カメラ前方ならUI表示、後方なら非表示
        _targetUI.gameObject.SetActive(isFront);
        if (!isFront) return;

        // オブジェクトのワールド座標→スクリーン座標変換
        var targetScreenPos = _targetCamera.WorldToScreenPoint(targetWorldPos);

        // スクリーン座標変換→UIローカル座標変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _parentUI,
            targetScreenPos,
            null,
            out var uiLocalPos
        );

        // RectTransformのローカル座標を更新
        _targetUI.transform.localPosition = uiLocalPos;
    }

    private void EnemyInfoUIUpdata()
    {
        //取得したコライダーの一番上の親オブジェクトを取得
        var enemyParent = _enemyChecker.NowEnemy.transform.root.gameObject;

        bool isGet = enemyParent.TryGetComponent<EnemyControl>(out EnemyControl tryGetEnemyControl);

        if (!isGet)
        {
            return;
        }

        //敵の情報のパネルの設定
        _enemyInfoName.text = tryGetEnemyControl.EnemyData.EnemyName;
        _enemyInfoHp.maxValue = tryGetEnemyControl.EnemyData.MaxHp;
        _enemyInfoHp.value = tryGetEnemyControl.EnemyLife.NowHp;

        if (_enemyChecker.IsTargetting && _enemyChecker.NowEnemy != null)
        {
            _enemyInfoWeak.SetActive(true);
            IconSet(tryGetEnemyControl);
        }
        else
        {
            _enemyInfoWeak.SetActive(false);
        }


        var cameraTransform = Camera.main.transform;

        // カメラの向きベクトル
        var cameraDir = cameraTransform.forward;

        // オブジェクトの位置
        var targetWorldPos = _enemyChecker.NowEnemy.transform.position + tryGetEnemyControl.EnemyData.UIOffSet;

        // カメラからターゲットへのベクトル
        var targetDir = targetWorldPos - cameraTransform.position;

        // 内積を使ってカメラ前方かどうかを判定
        var isFront = Vector3.Dot(cameraDir, targetDir) > 0;

        // カメラ前方ならUI表示、後方なら非表示
        _enemyInfo.gameObject.SetActive(isFront);
        if (!isFront) return;

        // オブジェクトのワールド座標→スクリーン座標変換
        var targetScreenPos = _targetCamera.WorldToScreenPoint(targetWorldPos);

        // スクリーン座標変換→UIローカル座標変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _parentUI,
            targetScreenPos,
            null,
            out var uiLocalPos
        );

        // RectTransformのローカル座標を更新
        _enemyInfo.transform.localPosition = uiLocalPos;
    }

    public void IconSet(EnemyControl enemyControl)
    {
        _swordIcon.SetActive(false);
        _largeSwordIcon.SetActive(false);
        _twinSwordIcon.SetActive(false);
        _spearIcon.SetActive(false);
        _gunIcon.SetActive(false);
        _fireIcon.SetActive(false);
        _iceIcon.SetActive(false);
        _thenderIcon.SetActive(false);

        foreach (var a in enemyControl.EnemyData.WeakWeapons)
        {
            if (a == AllWeapons.WeaponType.Sword)
            {
                _spearIcon.SetActive(true);
            }
            else if (a == AllWeapons.WeaponType.LargeSword)
            {
                _largeSwordIcon.SetActive(true);
            }
            else if (a == AllWeapons.WeaponType.twinSword)
            {
                _twinSwordIcon.SetActive(true);
            }
            else if (a == AllWeapons.WeaponType.Spear)
            {
                _spearIcon.SetActive(true);
            }
            else if (a == AllWeapons.WeaponType.Gun)
            {
                _gunIcon.SetActive(true);
            }
        }

        foreach (var a in enemyControl.EnemyData.WeakAttributes)
        {
            if (a == Attribute.Fire)
            {
                _fireIcon.SetActive(true);
            }
            else if (a == Attribute.Ice)
            {
                _iceIcon.SetActive(true);
            }
            else if (a == Attribute.Thunder)
            {
                _thenderIcon.SetActive(true);
            }
        }

    }
}
