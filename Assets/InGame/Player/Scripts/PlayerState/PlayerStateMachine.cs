using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerStateMachine : StateMachine
{

    #region State
    [SerializeField]
    private IdleState _stateIdle = default;
    [SerializeField]
    private WalkState _stateWalk = default;
    [SerializeField]
    private RunState _stateRun = default;
    [SerializeField]
    private JumpState _stateJump = default;
    [SerializeField]
    private UpAirState _stateUpAir = default;
    [SerializeField]
    private DownAirState _stateDownAir = default;

    [SerializeField]
    private AttackState _stateAttack = default;
    [SerializeField]
    private AvoidState _stateAvoid = default;
    [SerializeField]
    private AttackSkilState _stateAttackSkill = default;
    [SerializeField]
    private WeaponSelectState _stateWeaponSelect = default;
    [SerializeField]
    private ShiftBreakState _stateShiftBreak;
    [SerializeField] private StepState _stateStep;

    [SerializeField]
    private HoldAvoidldleState _stateHoldAvoidIdle;
    [SerializeField]
    private HoldAvoidWalkState _stateHoldAvoidWalk;
    [SerializeField]
    private HoldAvoidAirState _stateHoldAvoidAir;
    [SerializeField]
    private DamageState _stateDamage;
    [SerializeField]
    private PickUpItemState _statePickUpItem;

    public PickUpItemState StatePickUpItem => _statePickUpItem;
    public HoldAvoidldleState StateholdAvoidldle => _stateHoldAvoidIdle;
    public HoldAvoidWalkState StateholdAvoidWalk => _stateHoldAvoidWalk;
    public HoldAvoidAirState StateholdAvoidAir => _stateHoldAvoidAir;

    private PlayerControl _playerController = null;

    public IdleState StateIdle => _stateIdle;
    public WalkState StateWalk => _stateWalk;
    public JumpState StateJump => _stateJump;
    public UpAirState StateUpAir => _stateUpAir;
    public DamageState StateDamage => _stateDamage;


    public DownAirState StateDownAir => _stateDownAir;
    public RunState StateRun => _stateRun;

    public PlayerControl PlayerController => _playerController;

    public AttackState AttackState => _stateAttack;
    public AvoidState AvoidState => _stateAvoid;

    public WeaponSelectState WeaponSelectState => _stateWeaponSelect;

    public AttackSkilState AttackSkillState => _stateAttackSkill;

    public ShiftBreakState ShiftBreakState => _stateShiftBreak;

    public StepState StepState => _stateStep;

    #endregion
    //[SerializeField]
    //private GroundCheck _groundCheck;
    //public GroundCheck GroundCheck => _groundCheck;

    public void Init(PlayerControl playerController)
    {
        _playerController = playerController;
        Initialize(_stateIdle);
    }

    protected override void StateInit()
    {
        _stateIdle.Init(this);
        _stateWalk.Init(this);
        _stateJump.Init(this);
        _stateUpAir.Init(this);
        _stateDownAir.Init(this);

        _stateRun.Init(this);

        _stateAttack.Init(this);
        _stateAvoid.Init(this);
        _stateAttackSkill.Init(this);
        _stateWeaponSelect.Init(this);
        _stateShiftBreak.Init(this);
        _stateStep.Init(this);

        _stateHoldAvoidIdle.Init(this);
        _stateHoldAvoidWalk.Init(this);
        _stateHoldAvoidAir.Init(this);
        _stateDamage.Init(this);
        _statePickUpItem.Init(this);
    }

}
