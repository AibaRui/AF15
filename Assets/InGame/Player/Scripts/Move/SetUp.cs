using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SetUp 
{
    [Header("�J������Priority")]
    [SerializeField] private int _cameraPriority = 30;

    [SerializeField]
    private float _count = 0.5f;

    private float _countTime = 0;

    private bool _isEndCameraTransition;

    public bool IsEndCameraTransition  =>_isEndCameraTransition;


    private PlayerControl _playerControl;

    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }



    public void SetUpCamera()
    {
        Time.timeScale = 0.3f;
       // _playerControl.CameraBrain.m_IgnoreTimeScale = true;
    }

    public void SetUping()
    {
        Time.timeScale = 0.3f;
        //_playerControl.PlayerT.transform.forward = _playerControl.CameraGrapple.transform.forward;

        if (_countTime > _count)
        {
            //�J�����̐��ڂ�����
            _isEndCameraTransition = true;
        }
        else
        {
            _countTime += Time.unscaledDeltaTime;
        }

    }

    public void SetEnd()
    {
            //�^�C���X�P�[����߂�
            Time.timeScale = 1f;

            _playerControl.LineRenderer.positionCount = 0;

            Quaternion r = _playerControl.PlayerT.rotation;
            r.x = 0;
            r.z = 0;

            _playerControl.PlayerT.rotation = r;

            //�������Ԍv���p�̃^�C�}�[�����Z�b�g
            _countTime = 0;

        _isEndCameraTransition = false;
    }


    
}
