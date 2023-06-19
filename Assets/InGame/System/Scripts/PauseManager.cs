using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[System.Serializable]
public class PauseManager
{
    /// <summary> ポーズ命令が発行された回数をカウントする値 </summary>
    private int _pauseCount = 0;

    public int PauseCounter { get => _pauseCount; set => _pauseCount = value; }

    private bool _isPause = false;

    public bool IsPause => _isPause;

    /// <summary>true の時は一時停止とする</summary>
    bool _pauseFlg = false;
    /// <summary>一時停止・再開を制御する関数の型（デリゲート）を定義する</summary>
    public delegate void Pause(bool isPause);
    /// <summary>デリゲートを入れておく変数</summary>
    Pause _onPauseResume = default;

    /// <summary>一時停止・再開を入れるデリゲートプロパティ</summary>
    public Pause OnPauseResume
    {
        get { return _onPauseResume; }
        set { _onPauseResume = value; }
    }

    /// <summary>一時停止・再開を切り替える</summary>
    void PauseResume(bool isPause)
    {
        _pauseFlg = isPause;
        _isPause = isPause;

        if (_onPauseResume != null)
        {
            _onPauseResume(_pauseFlg);  // これで変数に代入した関数を（全て）呼び出せる
        }

    }

    /// <summary>
    /// Pauseを終了
    /// </summary>
    public void PauseEnd()
    {
        PauseResume(false);
        _pauseCount = 0;
    }

    /// <summary>
    /// Pauseを開始
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
