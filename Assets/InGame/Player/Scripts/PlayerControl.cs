using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerControl : MonoBehaviour, IDamageble
{
    [SerializeField] private CinemachineVirtualCamera _camera;

    [SerializeField] private Animator _animator;

    [SerializeField] private Transform _playerT;

    [SerializeField] private Transform _playerMidlePos;

    [SerializeField] private GameObject _player;

    [SerializeField] private Transform _modelT;

    [SerializeField] private GameObject _model;

    [SerializeField] private LineRenderer _lineRenderer;

    [SerializeField] private Rigidbody _rb;

    //[SerializeField] private CameraControl _cameraControl;

    [SerializeField] private PlayerStateMachine _stateMachine = default;

    [SerializeField] private InputManager _inputManager;

    [Header("ステータス設定")]
    [SerializeField] private PlayerStatus _playerStatus;

    [Header("アニメーション設定")]
    [SerializeField] private PlayerAnimationControl _animControl;



    [Header("設置判定")]
    [SerializeField] private GroundCheck _groundCheck;

    [Header("レイヤー管理")]
    [SerializeField] public LayerControl _layerControl;

    [Header("マテリアル設定")]
    [SerializeField] private MaterialControl _materialControl;

    [Header("速度制限設定")]
    [SerializeField] private VelocityLimit _velocityLimit;

    [Header("移動")]
    [SerializeField] private PlayerMove _playerMove;

    [Header("攻撃")]
    [SerializeField] private Attack _attack;

    [Header("攻撃のアシスト移動")]
    [SerializeField] private AttackAsist _attackAsist;

    [Header("攻撃時のステップ")]
    [SerializeField] private Step _step;

    [Header("シフトブレイク")]
    [SerializeField] private ShiftBreak _shiftBreak;

    [Header("ダメージ")]
    [SerializeField] private Damage _damage;

    [Header("回避")]
    [SerializeField] private Avoid _avoid;

    [Header("回避構え時の動き")]
    [SerializeField] private HoldAvoid _holdAvoidMove;

    [Header("残像")]
    [SerializeField] private AfterImageControl _afterImage;

    [Header("アイテムを拾う")]
    [SerializeField] private PickUp _pickUp;

    [SerializeField] private ColliderSearcher _colliderSearcher;
    [SerializeField] public StepInfo _stepInfo;
    [SerializeField] private AllWeapons _allWeapons;
    [SerializeField] private WeaponSetting _weaponSetting;
    [SerializeField] private EnemyChecker _enemyCheck;
    [SerializeField] private ParticleControl _particleControl;
    [SerializeField] private AttackSkil _attackSkil;
    [SerializeField] private PanelStack _panelStack;
    [SerializeField] private ItemGetUIControl _itemGetUIControl;
    [SerializeField] private EnemyLockOn _lockOmUI;
    [SerializeField] private InGameWeaponSelect _inGameWeaponSelect;
    [SerializeField] private SoundManager _soundManager;

    public SoundManager SoundManager => _soundManager;
    private bool _isTimeLineNow = false;
    public InGameWeaponSelect InGameUIControl => _inGameWeaponSelect;
    public ItemGetUIControl ItemGetUIControl => _itemGetUIControl;
    public PanelStack PanelStack => _panelStack;
    public ColliderSearcher ColliderSearcher => _colliderSearcher;
    public PickUp PickUp => _pickUp;
    public LayerControl LayerControl => _layerControl;
    public Damage Damage => _damage;
    public PlayerStatus PlayerStatus => _playerStatus;
    public HoldAvoid HoldAvoidMove => _holdAvoidMove;
    public Transform PlayerMidlePos => _playerMidlePos;
    public ParticleControl ParticleControl => _particleControl;
    public MaterialControl MaterialControl => _materialControl;
    public EnemyChecker EnemyCheck => _enemyCheck;
    public AfterImageControl AfterImage => _afterImage;
    public GameObject Player => _player;
    public AttackAsist AttackAsist => _attackAsist;
    public PlayerAnimationControl AnimControl => _animControl;
    public InputManager InputManager => _inputManager;
    public PlayerMove Move => _playerMove;
    public CinemachineVirtualCamera Camera => _camera;
    //public CinemachineVirtualCamera CameraGrapple => _cameraGrapple;
    public Transform ModelT => _modelT;
    // public Transform Hads => _hands;
    public Animator Animator => _animator;
    public Rigidbody Rb => _rb;
    public Transform PlayerT => _playerT;
    // public PlayerAnimationControl AnimControl => _animControl;
    public GroundCheck GroundCheck => _groundCheck;
    public LineRenderer LineRenderer => _lineRenderer;
    public VelocityLimit VelocityLimit { get => _velocityLimit; set => _velocityLimit = value; }
    public Attack Attack => _attack;
    public Avoid Avoid => _avoid;
    public AttackSkil AttackSkill => _attackSkil;
    public WeaponSetting WeaponSetting => _weaponSetting;
    public GameObject Model => _model;

    public ShiftBreak ShiftBreak => _shiftBreak;

    public AllWeapons AllWeapons => _allWeapons;

    public Step Step => _step;
    ////// public SetUp SetUp => _setUp;

    float h = 0;
    float v = 0;

    Vector3 velo;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        _animator.keepAnimatorStateOnDisable = true;

        _stateMachine.Init(this);
        _playerMove.Init(this);
        _animControl.Init(this);
        _groundCheck.Init(this);
        _velocityLimit.Init(this);

        _attack.Init(this);
        _avoid.Init(this);

        _attackSkil.Init(this);
        _shiftBreak.Init(this);
        _attackAsist.Init(this);
        _step.Init(this);
        _afterImage.Init(this);
        _materialControl.Init(this);
        _holdAvoidMove.Init(this);
        _playerStatus.Init(this);
        _damage.Init(this);
        _layerControl.Init(this);
        _pickUp.Init(this);
    }

    void Start()
    {

    }

    private void OnDrawGizmosSelected()
    {
        _groundCheck.OnDrawGizmos(PlayerT);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !GameManager.Instance.PauseManager.IsPause)
        {
            GameManager.Instance.PauseManager.PauseStart();
            _panelStack.OpenInventory();
        }

        if (!GameManager.Instance.PauseManager.IsPause)
        {
            _stateMachine.Update();
            _velocityLimit.LimitSpeed();

            _animControl.AnimSet();
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.PauseManager.IsPause)
        {
            _stateMachine.FixedUpdate();
        }
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.PauseManager.IsPause)
        {
            _stateMachine.LateUpdate();
        }
    }


    public GameObject InstantiateObject(GameObject obj)
    {
        return Instantiate(obj);
    }


    /// <summary>どのステートでも共通でまわすもの</summary>
    public void Allways()
    {
        if (!GameManager.Instance.PauseManager.IsPause)
        {
            // _attack.AttackCoolTime();
            // _swing.CountCoolTime();
            _avoid.CountCoolTimeAvoid();

            _afterImage.AfterImageLifeTimeCount();

            _shiftBreak.CountCoolTime();

            _enemyCheck.EnemyTargetting();
            _enemyCheck.EnemyCheck();

            _avoid.CheckHoldAvoid();
        }
    }

    void OnEnable()
    {
        GameManager.Instance.PauseManager.OnPauseResume += PauseResume;
    }

    void OnDisable()
    {
        GameManager.Instance.PauseManager.OnPauseResume -= PauseResume;
    }

    void PauseResume(bool isPause)
    {
        if (isPause)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    public void Pause()
    {
        _rb.isKinematic = true;
        _animator.speed = 0;

    }

    public void Resume()
    {
        _rb.isKinematic = false;
        _animator.speed = 1;
    }


    public Vector3 TargetPosition()
    {
        return Vector3.zero;
        //  return _target.position;
    }

    public GameObject InsGameObject(GameObject gameObject)
    {
        return Instantiate(gameObject);
    }

    public void AddDamage(float damage)
    {
        if (_holdAvoidMove.IsHoldAvoid)
        {
            _holdAvoidMove.IsDamage = true;
        }
        else
        {
            if (!_damage.IsDamageNow && !_shiftBreak.IsShiftBreake && !_step.IsStepNow)
            {
                if (_damage.DownToDamage <= damage)
                {
                    _damage.IsDamage = true;

                    _soundManager.PlaySound(AudioType.Damage);

                    if (_playerStatus.PlayerLife.Damage((int)damage))
                    {

                        _isTimeLineNow = true;
                        GameManager.Instance.PauseManager.PauseStart();
                        _damage.Dead();
                        _inGameWeaponSelect.UIActive(false);
                    }
                }
            }
        }
    }


    /// <summary>タイムラインが終わった事の通知用</summary>
    public void EndDeadTimeLine()
    {
        _particleControl.HealParticle();
        _inGameWeaponSelect.UIActive(true);
        _isTimeLineNow = false;
        GameManager.Instance.PauseManager.PauseEnd();
        _damage.EndTimeLine();
    }

    public void P()
    {
        GameManager.Instance.PauseManager.PauseStart();
    }

    public void EndP()
    {
        GameManager.Instance.PauseManager.PauseEnd();
    }
}
