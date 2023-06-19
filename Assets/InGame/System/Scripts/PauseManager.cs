using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[System.Serializable]
public class PauseManager
{
    /// <summary> �|�[�Y���߂����s���ꂽ�񐔂��J�E���g����l </summary>
    private int _pauseCount = 0;

    public int PauseCounter { get => _pauseCount; set => _pauseCount = value; }

    private bool _isPause = false;

    public bool IsPause => _isPause;

    /// <summary>true �̎��͈ꎞ��~�Ƃ���</summary>
    bool _pauseFlg = false;
    /// <summary>�ꎞ��~�E�ĊJ�𐧌䂷��֐��̌^�i�f���Q�[�g�j���`����</summary>
    public delegate void Pause(bool isPause);
    /// <summary>�f���Q�[�g�����Ă����ϐ�</summary>
    Pause _onPauseResume = default;

    /// <summary>�ꎞ��~�E�ĊJ������f���Q�[�g�v���p�e�B</summary>
    public Pause OnPauseResume
    {
        get { return _onPauseResume; }
        set { _onPauseResume = value; }
    }

    /// <summary>�ꎞ��~�E�ĊJ��؂�ւ���</summary>
    void PauseResume(bool isPause)
    {
        _pauseFlg = isPause;
        _isPause = isPause;

        if (_onPauseResume != null)
        {
            _onPauseResume(_pauseFlg);  // ����ŕϐ��ɑ�������֐����i�S�āj�Ăяo����
        }

    }

    /// <summary>
    /// Pause���I��
    /// </summary>
    public void PauseEnd()
    {
        PauseResume(false);
        _pauseCount = 0;
    }

    /// <summary>
    /// Pause���J�n
    /// </summary>
    public void PauseStart()
    {
        if (_pauseCount == 0)
        {
            PauseResume(true);
        }

        _pauseCount++;
    }

    void Update()
    {

    }
}
