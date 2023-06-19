using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    /// <summary>キーによる方向</summary>
    private Vector3 inputVector;
    public Vector3 InputVector => inputVector;

    [Tooltip("スペースを押す")]
    private bool _isJumping;
    public bool IsJumping { get => _isJumping; set => _isJumping = value; }

    [Tooltip("左クリック_押す")]
    private bool _isLeftMouseClickDown = false;
    public bool IsLeftMouseClickDown { get => _isLeftMouseClickDown; }

    [Tooltip("左クリック_離す")]
    private bool _isLeftMouseClickUp = false;
    public bool IsLeftMouseClickUp { get => _isLeftMouseClickUp; }

    [Tooltip("右クリック_押す")]
    private bool _isRightMouseClickDown = false;
    public bool IsRightMouseClickDown { get => _isRightMouseClickDown; }

    [Tooltip("右クリック_話す")]
    private bool _isRightMouseClickUp = false;
    public bool IsRightMouseClickUp { get => _isRightMouseClickUp; }


    [Tooltip("左Ctrl_押す")]
    private bool _isCtrlDown;
    public bool IsCtrlDown { get => _isCtrlDown; }

    [Tooltip("左Ctrl_離す")]
    private bool _isCtrlUp;
    public bool IsCtrlUp { get => _isCtrlUp; }

    [Tooltip("攻撃")]
    private bool _isAttack;
    public bool IsAttack { get => _isAttack; }

    [Tooltip("回避")]
    private bool _isInputDownAvoidButttun;
    public bool IsInputDownAvoidButttun { get => _isInputDownAvoidButttun; }

    private bool _isInputUpAvoidButttun;
    public bool IsInputUpAvoidButttun { get => _isInputUpAvoidButttun; }

    private bool _isInputAvoidButttun;
    public bool IsInputAvoidButttun { get => _isInputAvoidButttun; }

    [Tooltip("構え")]
    private float _isSetUp;
    public float IsSetUp { get => _isSetUp; }

    [Tooltip("カメラの移動")]
    private Vector2 _isControlCameraValueChange;

    private bool _weapon1;

    private bool _weapon2;

    private bool _weapon3;

    private bool _weapon4;

    public bool Weapon1 => _weapon1;
    public bool Weapon2 => _weapon2;
    public bool Weapon3 => _weapon3;
    public bool Weapon4 => _weapon4;

    private bool _isShiftBreak;

    public bool IsShiftBreak => _isShiftBreak;


    private float _isMouseScrol = 0;

    private float _isSwing;

    public float IsSwing => _isSwing;


    [Tooltip("Tab_押す")]
    private bool _isTabDown;
    public bool IsTabDown => _isTabDown;

    [Tooltip("左Shift_押す")]
    private bool _isLeftShiftDown = false;
    public bool IsLeftShiftDown => _isLeftShiftDown;

    private bool _isLeftShift = false;
    public bool IsLeftShift => _isLeftShift;

    [Tooltip("左Shift_離す")]
    private bool _isLeftShiftUp = false;
    public bool IsLeftShiftUp => _isLeftShiftUp;

    private float _horizontalInput;
    public float HorizontalInput { get => _horizontalInput; }

    private float _verticalInput;

    public float VerticalInput { get => _verticalInput; }

    private float _cameraHorizontalInput;

    private float _cameraVerticalInput;

    public float CameraHorizontalInput => _cameraHorizontalInput;

    public float CameraVerticalInput => _cameraVerticalInput;
    public Vector2 IsControlCameraValueChange { get => _isControlCameraValueChange; }

    public float IsMouseScrol => _isMouseScrol;

    private bool _isDownESC;
    public bool IsDownEscape => _isDownESC;

    private bool _isInventoryRight;

    private bool _isInventoryleft;

    public bool IsInventoryRight => _isInventoryRight;
    public bool IsInventoryLeft => _isInventoryleft;

    private bool _isGetItem;
    public bool IsGetItem => _isGetItem;

    public void HandleInput()
    {
        _isGetItem = Input.GetKeyDown(KeyCode.F);

        _isInventoryRight = Input.GetKeyDown(KeyCode.X);
        _isInventoryleft = Input.GetKeyDown(KeyCode.Z);

        _isDownESC = Input.GetKeyDown(KeyCode.Escape);

        _horizontalInput = 0;
        _verticalInput = 0;

        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        //Debug.Log(_horizontalInput);

        //マウスの左クリック
        _isLeftMouseClickDown = Input.GetMouseButtonDown(0);
        _isLeftMouseClickUp = Input.GetMouseButtonUp(0);

        //マウス右クリック
        _isRightMouseClickDown = Input.GetMouseButtonDown(1);
        _isRightMouseClickUp = Input.GetMouseButtonUp(1);


        //攻撃
        //_isAttack = Input.GetButtonDown("Fire3");
        _isAttack = Input.GetMouseButtonDown(0);

        //_isAvoid = Input.GetButtonDown("Avoid");


        _isInputDownAvoidButttun = Input.GetKeyDown(KeyCode.C);
        _isInputUpAvoidButttun = Input.GetKeyUp(KeyCode.C);
        _isInputAvoidButttun = Input.GetKey(KeyCode.C);

        //_isSetUp = Input.GetAxisRaw("Fire2");

        // float _horizontalInputCamera = Input.GetAxisRaw("CameraX");
        // float _verticalInputCamera = Input.GetAxisRaw("CameraY");

        //   _isControlCameraValueChange = new Vector2(_horizontalInputCamera, _verticalInputCamera);

        //Ctrlを押したか
        _isCtrlDown = Input.GetKeyDown(KeyCode.LeftControl);
        //Ctrlを離したか
        _isCtrlUp = Input.GetKeyUp(KeyCode.LeftControl);

        //Space
        _isJumping = Input.GetButtonDown("Jump");

        //Tab
        _isTabDown = Input.GetKeyDown(KeyCode.Tab);

        // Shift
        _isLeftShiftDown = Input.GetKeyDown(KeyCode.LeftShift);
        _isLeftShiftUp = Input.GetKeyUp(KeyCode.LeftShift);
        _isLeftShift = Input.GetKey(KeyCode.LeftShift);


        _isMouseScrol = Input.GetAxis("Mouse ScrollWheel");


        _weapon1 = Input.GetKeyDown(KeyCode.F1);
        _weapon2 = Input.GetKeyDown(KeyCode.F2);
        _weapon3 = Input.GetKeyDown(KeyCode.F3);
        _weapon4 = Input.GetKeyDown(KeyCode.F4);


        _isShiftBreak = Input.GetKeyDown(KeyCode.Q);
    }



    private void Update()
    {
        HandleInput();
    }
}
