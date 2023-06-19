using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[System.Serializable]
public class AfterImageControl
{
    [SerializeField] private GameObject _afterImagePrefab;

    [SerializeField] private float _lifeTime = 2;

    private float _currentTime = 0;

    private bool _isAfterImage = false;

    /// <summary>残像用のオブジェクト</summary>
    private GameObject _afterImageObject;

    /// <summary>残像のアニメーター</summary>
    private Animator _animator;

    private PlayerControl _playerControl;

    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }

    /// <summary>残像の消失時間の計算</summary>
    public void AfterImageLifeTimeCount()
    {
        if (_isAfterImage)
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= _lifeTime)
            {
                _isAfterImage = false;
                _currentTime = 0;

                ActiveFalseAfrterImage();
            }
        }
    }

    /// <summary>残像を非表示にする</summary>
    public void ActiveFalseAfrterImage()
    {
        _afterImageObject.SetActive(false);
    }


    /// <summary>残像を使用する</summary>
    public void PlayAfterImage(string animName,Vector3 startPos,Quaternion rotation)
    {
        _isAfterImage = true;

        //残像を生成
        SetAfterImageObject(startPos,rotation);

        //アニメーションの設定
        SetAnimation(animName);
    }

    /// <summary>残像を生成</summary>
    public void SetAfterImageObject(Vector3 startPos,Quaternion rotation)
    {
        if (_afterImageObject == null)
        {
            _afterImageObject = _playerControl.InstantiateObject(_afterImagePrefab);
            _animator = _afterImageObject.transform.GetChild(0).GetComponent<Animator>();
        }
        else
        {
            _afterImageObject.SetActive(true);
        }

        _afterImageObject.transform.position = startPos;
        _afterImageObject.transform.rotation = rotation;
    }

    /// <summary>残像のアニメーションの設定</summary>
    public void SetAnimation(string animName)
    {
        AnimatorStateInfo animationState = _playerControl.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorClipInfo[] myAnimatorClip = _playerControl.Animator.GetCurrentAnimatorClipInfo(0);
        float currentTime = myAnimatorClip[0].clip.length * animationState.normalizedTime;
        _animator.speed = 0.3f;
        _animator.Play(animName, 0, currentTime);
    }

}
