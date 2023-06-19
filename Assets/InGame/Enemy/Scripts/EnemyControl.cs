using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class EnemyControl : MonoBehaviour, IEnemyDamageble
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _animator;

    [SerializeField] private Transform _enemy;

    [Header("戦闘範囲")]
    [SerializeField] private BattleArea _battleArea;

    [Header("敵の情報")]
    [SerializeField] private EnemyData _enemyData;


    [Header("移動処理")]
    [SerializeField] private EnemyMove _enemyMove;

    [Header("体力処理")]
    [SerializeField] private EnemyLife _enemyLife;

    [Header("攻撃処理")]
    [SerializeField] private EnemyAttack _enemyAttack;

    [Header("アニメーション")]
    [SerializeField] private EnemyAnim _enemyAnim;

    private bool _isDead = false;

    public EnemyAnim AnimControl => _enemyAnim;
    public Animator Animator => _animator;
    public Rigidbody Rb => _rb;
    public Transform EnemyT => _enemy;
    public BattleArea BattleArea => _battleArea;
    public EnemyData EnemyData => _enemyData;

    public EnemyLife EnemyLife => _enemyLife;
    public EnemyAttack EnemyAttack => _enemyAttack;

    private void Awake()
    {
        _enemyLife.Init(this);
        _enemyMove.Init(this);
        _enemyAnim.Init(this);
        _enemyAttack.Init(this);


    }

    private void Start()
    {
        GameManager.Instance.PauseManager.OnPauseResume += PauseResume;
    }

    void Update()
    {
        //アニメーションパラメータ
        _enemyAnim.AnimParametaSet();
        _enemyAttack.CountCoolTime();
    }

    private void FixedUpdate()
    {
        if (_battleArea.IsBattle)
        {
            _enemyMove.SetDir();
            if (!_enemyAttack.IsAttack && !_enemyAttack.IsAttackCoolTIme)
            {
                //移動処理
                _enemyMove.Move();
            }

            _enemyMove.Rotate();
        }
        _enemyMove.Gravity();
    }

    public void DeadDestoroy()
    {
        Destroy(gameObject);
    }

    public void Dead()
    {
        _isDead = true;
        _enemyAnim.DeathAnim();
    }


    void OnEnable()
    {

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

    public void AddDamageEnemy(float damage, AllWeapons.WeaponType type, Attribute attribute)
    {
        if (!_isDead)
        {
            if (_enemyLife.Damage(damage,attribute,type))
            {
                Dead();
            }

        }
    }

}
