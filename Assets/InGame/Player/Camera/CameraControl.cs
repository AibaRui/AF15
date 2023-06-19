using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public enum InputChoice
    {
        KeyboardAndMouse, Controller,
    }

    [Header("Swing�A�󒆎��̏�����Ԃ�Y�̊p�x")]
    [SerializeField] private float _firstYvalue = 0;



    [Header("Swing�A�󒆎��̉������̂�Y�̊p�x")]
    [SerializeField] private float _downvalueY = 10;



    [Header("Swing�A�󒆎��̏�����Ԃ�OffSet")]
    [SerializeField] private float _firstOffSet = 1.2f;

    [Header("Swing���̍ő�̏������OffSet")]
    [SerializeField] private float _maxUpOffSet = 1.2f;

    [Header("Swing���̍ő�̉�������OffSet")]
    [SerializeField] private float _maxDownOffSet = 4f;

    [Header("Swing���̃J�����̍ŏ���Distance")]
    [SerializeField] private float _firstSwingCameraDistance = 7f;

    [Header("Swing���̃J�����̍ő��Distance")]
    [SerializeField] private float _maxSwingCameraDistance = 11f;

    [Header("�J�������_�̕ύX������܂ł̃N�[���^�C��")]
    [SerializeField] private float _cameraAngleChangeTime = 1f;

    [SerializeField] private PlayerControl _playerControl;

    [SerializeField] Transform follow;

    [SerializeField] Transform lookAt;

    public InputChoice inputChoice;

    public bool allowRuntimeCameraSettingsChanges;

    [SerializeField] private float _firstZ;

    [SerializeField] private CinemachineVirtualCamera keyboardAndMouseCamera;

    [Header("�ʏ펞�̃J����")]
    [SerializeField] private CinemachineVirtualCamera controllerCamera;

    [Header("Swing�A�󒆈ړ����̃J����")]
    [SerializeField] private CinemachineVirtualCamera _swingControllerCamera;

    [Header("�\�����̃J����")]
    [SerializeField] private CinemachineVirtualCamera _setUpControllerCamera;

    [Header("WallRun�p�̃J����")]
    [SerializeField] private CinemachineVirtualCamera _wallRunControllerCamera;

    private CinemachinePOV _swingCinemachinePOV;

    private CinemachineFramingTransposer _swingCameraFraming;

    public CinemachineVirtualCamera WallRunCameraController => _wallRunControllerCamera;


    private bool _isUpEnd = false;

    private float _countTime = 0;
    private bool _isDontCameraMove = false;

    public bool IsDontCameraMove => _isDontCameraMove;

    private float _countCameraMoveSwingingX = 0;

    private float _countCameraMoveAirX = 0;

    private float _countCameraMoveY = 0;

    private bool _isEndAutoFollow = false;

    private float _autoFloowCount = 0;

    private bool _isSwingEndCameraDistanceToLong = false;

    private float _swingEndPlayerRotateY;
    public PlayerControl PlayerControl => _playerControl;
    public float SwingEndPlayerRotateY { get => _swingEndPlayerRotateY; set => _swingEndPlayerRotateY = value; }




    public bool IsEndAutpFollow => _isEndAutoFollow;
    void Awake()
    {
        UpdateCameraSettings();
        _swingCinemachinePOV = _swingControllerCamera.GetCinemachineComponent<CinemachinePOV>();
        _swingCameraFraming = _swingControllerCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    void Update()
    {
        if (allowRuntimeCameraSettingsChanges)
        {
            UpdateCameraSettings();
        }
        CountDontCameraMoveTime();
    }

    void UpdateCameraSettings()
    {
        keyboardAndMouseCamera.Follow = follow;
        keyboardAndMouseCamera.LookAt = lookAt;
        // keyboardAndMouseCamera.m_XAxis.m_InvertInput = keyboardAndMouseInvertSettings.invertX;
        // keyboardAndMouseCamera.m_YAxis.m_InvertInput = keyboardAndMouseInvertSettings.invertY;

        //  controllerCamera.m_XAxis.m_InvertInput = controllerInvertSettings.invertX;
        //  controllerCamera.m_YAxis.m_InvertInput = controllerInvertSettings.invertY;
        controllerCamera.Follow = follow;
        controllerCamera.LookAt = lookAt;

        keyboardAndMouseCamera.Priority = inputChoice == InputChoice.KeyboardAndMouse ? 1 : 0;
        controllerCamera.Priority = inputChoice == InputChoice.Controller ? 1 : 0;
    }




    private void CountDontCameraMoveTime()
    {
        if (_playerControl.InputManager.IsControlCameraValueChange != Vector2.zero)
        {
            _isEndAutoFollow = false;
            _isDontCameraMove = false;
            _countTime = 0;
        }

        if (!_isDontCameraMove)
        {
            _countTime += Time.deltaTime;
            if (_countTime > _cameraAngleChangeTime)
            {
                _isDontCameraMove = true;
                _countTime = 0;
            }
        }
    }

    public void CountTime()
    {
        //�ړ����͂��󂯎��
        float h = _playerControl.InputManager.HorizontalInput;

        if (h > -0.5f && h < 0.5f)
        {
            //if (_countCameraMoveSwingingX > 0)
            //{
            //    _countCameraMoveSwingingX -= 0.01f;
            //}

            //if (_countCameraMoveAirX > 0)
            //{
            //    _countCameraMoveAirX -= 0.01f;
            //}

            _countCameraMoveSwingingX = 0;
            _countCameraMoveAirX = 0;
        }
        else
        {
            //if (_playerControl.Swing.IsSwingNow)
            //{
            //    if (_countCameraMoveSwingingX < 0.5)
            //    {
            //        _countCameraMoveSwingingX += 0.002f;
            //    }
            //}
            //else
            //{
            //    if (_countCameraMoveAirX < 0.4f)
            //    {
            //        _countCameraMoveAirX += 0.002f;
            //    }
            //}

            // if (!_playerControl.VelocityLimit.IsSpeedUp)
            //  {

            // }

            if (_isDontCameraMove)
            {
                //if (_countCameraMoveY != 0.2f)
                //{
                //    _countCameraMoveY += 0.0005f;
                //    if (_countCameraMoveY > 0.2f)
                //    {
                //        _countCameraMoveY = 0.2f;
                //    }

                //}
            }
        }

    }

    /// <summary>Swing��Ƀv���C���[�̃J�����̉�]��␳����</summary>
    public void SwingEndCameraAutoFollow()
    {
        //var h = _playerControl.InputManager.HorizontalInput;
        //var v = _playerControl.InputManager.VerticalInput;
        //if (h != 0 || v != 0)
        //{
        //    //�Ǐ]���I��
        //    _isEndAutoFollow = false;
        //}

        //if (_isEndAutoFollow && _isDontCameraMove)
        //{
        //    if (_autoFloowCount < 0.8f)
        //    {
        //        _autoFloowCount += 0.01f;
        //    }

        //    float y = 0;
        //    if (_playerControl.PlayerT.eulerAngles.y > 180)
        //    {
        //        y = _playerControl.PlayerT.eulerAngles.y - 360;
        //    }
        //    else
        //    {
        //        y = _playerControl.PlayerT.eulerAngles.y;
        //    }

        //    float angleDiff = Mathf.DeltaAngle(y, _swingCinemachinePOV.m_HorizontalAxis.Value); // �p�x����-180�x����180�x�͈̔͂Ɏ��߂�

        //    if (Mathf.Abs(angleDiff) > 90f)
        //    {
        //        angleDiff -= Mathf.Sign(angleDiff) * 180f;
        //    }// �p�x����90�x���傫���ꍇ�́A�t�����ɉ�]����

        //    if (angleDiff > 0f)
        //    {
        //        _swingCinemachinePOV.m_HorizontalAxis.Value -= Mathf.Min(angleDiff, _autoFloowCount);
        //    }// �v���C���[�̉�]�p�x�ɋ߂Â��悤��Value�̒l�����炷
        //    else if (angleDiff < 0f)
        //    {
        //        _swingCinemachinePOV.m_HorizontalAxis.Value += Mathf.Min(-angleDiff, _autoFloowCount);
        //    }// �v���C���[�̉�]�p�x�ɋ߂Â��悤��Value�̒l�𑝂₷

        //    float dis = Mathf.Abs(_swingEndPlayerRotateY - _swingCinemachinePOV.m_HorizontalAxis.Value);

        //    if (dis < 1f)
        //    {
        //        //�Ǐ]���I��
        //        _isEndAutoFollow = false;

        //        //���݂̃J�����̉�]���x���󂯌p��
        //        _countCameraMoveAirX = _autoFloowCount;

        //        return;
        //    }
        //}
    }


    public void SwingCameraValueX(bool isSwing)
    {
        if (isSwing)
        {
            //�ړ����͂��󂯎��
            float h = _playerControl.InputManager.HorizontalInput;

            ////////////X���̒���
            if (h > 0.8f)
            {
                _swingCinemachinePOV.m_HorizontalAxis.Value += _countCameraMoveSwingingX;
            }
            else if (h < -0.8f)
            {
                _swingCinemachinePOV.m_HorizontalAxis.Value -= _countCameraMoveSwingingX;
            }
        }
        else
        {
            //if (_playerControl.VelocityLimit.IsSpeedUp ||_isEndAutoFollow)
            //{
            //    _countCameraMoveAirX = 0;
            //    return;
            //}
            //

            //�ړ����͂��󂯎��
            float h = _playerControl.InputManager.HorizontalInput;

            Debug.Log(_countCameraMoveAirX);

            ////////////X���̒���
            if (h > 0.8f)
            {
                _swingCinemachinePOV.m_HorizontalAxis.Value += _countCameraMoveAirX;
            }
            else if (h < -0.8f)
            {
                _swingCinemachinePOV.m_HorizontalAxis.Value -= _countCameraMoveAirX;
            }
        }
    }

    /// <summary>Swing���ɃX�N���[����ł̃v���C���[�̈ʒu��ύX����</summary>
    /// <param name="velocityY"></param>
    public void SwingCameraYValues(float velocityY, float down, float up, float changeSpeed)
    {
        //�X�N���[����ł̃v���C���[�̈ʒu�̕ύX
        if (velocityY > 0)
        {
            Vector3 v = new Vector3(0, velocityY, 0);
            if (_swingCameraFraming.m_CameraDistance > _firstSwingCameraDistance + 0.5f)
                _swingCameraFraming.m_CameraDistance -= 0.0007f * v.magnitude;

            if (Mathf.Abs(_swingCameraFraming.m_CameraDistance - (_firstSwingCameraDistance + 0.5f)) < 0.1f)
            {
                _swingCameraFraming.m_CameraDistance = _firstSwingCameraDistance + 0.5f;
            }


            if (_swingCameraFraming.m_TrackedObjectOffset.y > _maxUpOffSet)
            {
                Debug.Log($"�ő�:{_maxUpOffSet} ����{_swingCameraFraming.m_TrackedObjectOffset.y}");
                _swingCameraFraming.m_TrackedObjectOffset.y -= Time.deltaTime * 1.8f;
            }
        }   //�ʒu�����̕��ɉ�����
        else if (velocityY < 0)
        {
            //�J�����̋����𗣂�
            Vector3 v = new Vector3(0, velocityY, 0);
            if (_swingCameraFraming.m_CameraDistance < _maxSwingCameraDistance)
                _swingCameraFraming.m_CameraDistance += 0.01f * v.magnitude;

            if (Mathf.Abs(_swingCameraFraming.m_CameraDistance - _maxSwingCameraDistance) < 0.1f)
            {
                _swingCameraFraming.m_CameraDistance = _maxSwingCameraDistance;
            }


            if (_swingCameraFraming.m_TrackedObjectOffset.y < _maxDownOffSet)
            {
                if (_swingCameraFraming.m_TrackedObjectOffset.y < _firstOffSet)
                {
                    _swingCameraFraming.m_TrackedObjectOffset.y += Time.deltaTime*2;
                }
                else
                {
                    _swingCameraFraming.m_TrackedObjectOffset.y += Time.deltaTime;
                }

            } //�ʒu����̕��ɂ���
        }

        if (_isDontCameraMove)
        {
            ////////// //Y���̒���
            if (velocityY > 0)
            {
                if (_swingCinemachinePOV.m_VerticalAxis.Value > -40)
                {
                    float add = new Vector3(0, velocityY, 0).magnitude;

                    float limit = new Vector3(0, 30, 0).magnitude;

                    if (add > limit)
                    {
                        add = limit;
                    }


                    _swingCinemachinePOV.m_VerticalAxis.Value -= 0.006f * add;
                }
            }
            else if (velocityY < 0)
            {
                if (_swingCinemachinePOV.m_VerticalAxis.Value <= 30)
                {
                    float add = new Vector3(0, velocityY, 0).magnitude;

                    float limit = new Vector3(0, 20, 0).magnitude;

                    if (add > limit)
                    {
                        add = limit;
                    }
                    _swingCinemachinePOV.m_VerticalAxis.Value += 0.005f * add;
                }
            }
        }
    }

    /// <summary>Y���̊p�x�𒼂�</summary>
    public void AirCameraYValue(float velocityY)
    {
        //Swing�I���ɁA�J�����𗣂����ǂ���
        if (_isSwingEndCameraDistanceToLong)
        {
            if (velocityY < 5) _isSwingEndCameraDistanceToLong = false;

            //�J�����̋����𗣂�
            Vector3 v = new Vector3(0, velocityY, 0);
            if (_swingCameraFraming.m_CameraDistance < _maxSwingCameraDistance + 1)
                _swingCameraFraming.m_CameraDistance += 0.01f * v.magnitude;

            if (Mathf.Abs(_swingCameraFraming.m_CameraDistance - _maxSwingCameraDistance + 1) < 0.1f)
            {
                _swingCameraFraming.m_CameraDistance = _maxSwingCameraDistance + 1;
            }
            Debug.Log("YES");
        }


        if (_isUpEnd)//������ɔ�яオ������
        {
            //���j�^�[��ł̃v���C���[�̈ʒu��ς���B��̕���
            if (_swingCameraFraming.m_TrackedObjectOffset.y > -1.6f)
            {
                Vector3 v = new Vector3(0, velocityY, 0);
                _swingCameraFraming.m_TrackedObjectOffset.y -= 0.01f * v.magnitude;


                //if (_swingCameraFraming.m_CameraDistance <= _maxSwingCameraDistance)
                //    _swingCameraFraming.m_CameraDistance += Time.deltaTime;
            }

            if (velocityY < 0 || _swingCameraFraming.m_TrackedObjectOffset.y <= -1.6f)
            {
                _isUpEnd = false;
            }   //Y�̑��x��0��菬�����Ȃ�����I��
        }
        else
        {
            //���j�^�[��ł̃v���C���[�̈ʒu��ς���B������Ԃ�
            if (velocityY < 0)
            {

                if (velocityY > -5)
                {
                    if (_swingCameraFraming.m_TrackedObjectOffset.y < _firstOffSet)
                        _swingCameraFraming.m_TrackedObjectOffset.y += Time.deltaTime * 1f;

                    if (_swingCameraFraming.m_TrackedObjectOffset.y > _firstOffSet)
                        _swingCameraFraming.m_TrackedObjectOffset.y -= Time.deltaTime * 0.5f;

                    if (Mathf.Abs(_swingCameraFraming.m_TrackedObjectOffset.y - _firstOffSet) < 0.02f)
                        _swingCameraFraming.m_TrackedObjectOffset.y = _firstOffSet;
                }
                else
                {
                    float speed = 0;

                    if (velocityY > -13)
                    {
                        speed = 0.1f;
                    }
                    else if (velocityY > -16)
                    {
                        speed = 0.4f;
                    }
                    else
                    {
                        speed = 0.6f;
                    }

                    if (_swingCameraFraming.m_TrackedObjectOffset.y < _maxDownOffSet - 1)
                    {
                        _swingCameraFraming.m_TrackedObjectOffset.y += Time.deltaTime * speed;
                    } //�ʒu����̕��ɂ���
                }

                if (_swingCameraFraming.m_CameraDistance > _firstSwingCameraDistance)
                {
                    Vector3 v = new Vector3(0, _playerControl.Rb.velocity.y, 0);
                    _swingCameraFraming.m_CameraDistance -= 0.0009f * v.magnitude;

                    if (_swingCameraFraming.m_CameraDistance < _firstSwingCameraDistance)
                    {
                        _swingCameraFraming.m_CameraDistance = _firstSwingCameraDistance;
                    }
                }
            }
        }

        if (_isDontCameraMove)
        {
            //�J�����̊p�x�����ɖ߂�
            if (_playerControl.InputManager.IsControlCameraValueChange == Vector2.zero)
            {
                if (_playerControl.Rb.velocity.y < -15)
                {
                    //if (_swingCinemachinePOV.m_VerticalAxis.Value <= 50)
                    //{
                    //    float add = new Vector3(0, velocityY, 0).magnitude;

                    //    float limit = new Vector3(0, 20, 0).magnitude;

                    //    if (add > limit)
                    //    {
                    //        add = limit;
                    //    }
                    //    _swingCinemachinePOV.m_VerticalAxis.Value += 0.005f * add;
                    //}
                }
                else
                {

                    if (_swingCinemachinePOV.m_VerticalAxis.Value > _firstYvalue)
                    {
                        _swingCinemachinePOV.m_VerticalAxis.Value -= Time.deltaTime * 15;

                        if (_swingCinemachinePOV.m_VerticalAxis.Value < _firstYvalue)
                        {
                            _swingCinemachinePOV.m_VerticalAxis.Value = _firstYvalue;
                        }
                    }
                    else if (_swingCinemachinePOV.m_VerticalAxis.Value < _firstYvalue)
                    {
                        _swingCinemachinePOV.m_VerticalAxis.Value += Time.deltaTime * 15;

                        if (_swingCinemachinePOV.m_VerticalAxis.Value > _firstYvalue)
                        {
                            _swingCinemachinePOV.m_VerticalAxis.Value = _firstYvalue;
                        }
                    }
                }
            }
        }
    }



    public void ZipCamera()
    {
        if (_swingCameraFraming.m_TrackedObjectOffset.y < _firstOffSet)
            _swingCameraFraming.m_TrackedObjectOffset.y += Time.deltaTime * 3f;

        if (_swingCameraFraming.m_TrackedObjectOffset.y > _firstOffSet)
            _swingCameraFraming.m_TrackedObjectOffset.y -= Time.deltaTime * 1f;

        if (Mathf.Abs(_swingCameraFraming.m_TrackedObjectOffset.y - _firstOffSet) < 0.02f)
            _swingCameraFraming.m_TrackedObjectOffset.y = _firstOffSet;

        if (_swingCinemachinePOV.m_VerticalAxis.Value > _firstYvalue)
        {
            _swingCinemachinePOV.m_VerticalAxis.Value -= Time.deltaTime * 30;

            if (_swingCinemachinePOV.m_VerticalAxis.Value < _firstYvalue)
            {
                _swingCinemachinePOV.m_VerticalAxis.Value = _firstYvalue;
            }
        }
        else if (_swingCinemachinePOV.m_VerticalAxis.Value < _firstYvalue)
        {
            _swingCinemachinePOV.m_VerticalAxis.Value += Time.deltaTime * 30;

            if (_swingCinemachinePOV.m_VerticalAxis.Value > _firstYvalue)
            {
                _swingCinemachinePOV.m_VerticalAxis.Value = _firstYvalue;
            }
        }

    }


    /// <summary>Zip�������Ƃ��̃J����</summary>
    public void ZipMoveCamera(float setDistance)
    {
        if (setDistance < _swingCameraFraming.m_CameraDistance)
        {
            return;
        }

        _swingCameraFraming.m_CameraDistance = setDistance;
    }


    public void UseWallRunCamera()
    {
        controllerCamera.Priority = 0;
        _setUpControllerCamera.Priority = 0;

        _swingControllerCamera.Priority = 0;

        _wallRunControllerCamera.Priority = 50;


        //Swing���̃J������Offset��߂�
        _swingCameraFraming.m_TrackedObjectOffset.y = _firstOffSet;
    }

    public void UseSwingCamera()
    {
        controllerCamera.Priority = 0;
        _setUpControllerCamera.Priority = 0;

        _swingControllerCamera.Priority = 50;
    }

    public void SetUpCamera()
    {
        controllerCamera.Priority = 0;
        _swingControllerCamera.Priority = 0;
        _wallRunControllerCamera.Priority = 0;

        _setUpControllerCamera.Priority = 50;

        //Swing���̃J������Offset��߂�
        _swingCameraFraming.m_TrackedObjectOffset.y = _firstOffSet;
    }

    public void RsetCamera()
    {
        //controllerCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = 

        _swingControllerCamera.Priority = 0;
        _setUpControllerCamera.Priority = 0;
        _wallRunControllerCamera.Priority = 0;

        controllerCamera.Priority = 40;
    }


    public void SwingEndSetCamera()
    {
        _countCameraMoveY = 0;
        // _countCameraMoveAirX = _countCameraMoveSwingingX;
        _countCameraMoveSwingingX = 0;
        _autoFloowCount = 0;

        //_swingCameraFraming.m_TrackedObjectOffset.y = _firstOffSet;
    }

    public void EndFollow()
    {
        _isEndAutoFollow = true;
        _isSwingEndCameraDistanceToLong = true;
    }


}

