using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerEffectControl
{
    [Header("�W����")]
    [SerializeField] private Image _concentrationLineeffectImage;

    [Header("�W�����̍ő哧���x")]
    [SerializeField] private float _concentrationLineeffectMaxColorAlpha;

    [Header("�W������L���ɂ��鑬�x")]
    [SerializeField] private float _useConcentrationLineeffectVelocity = -20;

    [Header("Zip")]
    [SerializeField] private GameObject _zipImage;

    private PlayerControl _playerControl;

    private Color _concentrationLineeffectColor;

    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }


    /// <summary>�W�����̊Ǘ�</summary>
    public void ConcentrationLineEffect()
    {
        Vector3 speed = _playerControl.Rb.velocity;
        speed.y = 0;

        if ((_playerControl.Rb.velocity.y <= _useConcentrationLineeffectVelocity))
        {
            if (_concentrationLineeffectImage.color.a >= _concentrationLineeffectMaxColorAlpha)
            {
                return;
            }

            var setColor = _concentrationLineeffectImage.color;
            setColor.a += Time.deltaTime;
            if (_concentrationLineeffectMaxColorAlpha - setColor.a < 0.1f)
            {
                setColor.a = _concentrationLineeffectMaxColorAlpha;
                _concentrationLineeffectImage.color = setColor;
            }
            else
            {
                _concentrationLineeffectImage.color = setColor;
            }
        }
        else
        {
            if (_concentrationLineeffectImage.color.a <= 0)
            {
                return;
            }

            var setColor = _concentrationLineeffectImage.color;
            setColor.a -= Time.deltaTime;

            if (setColor.a < 0f)
            {
                setColor.a = 0;
                _concentrationLineeffectImage.color = setColor;
            }
            else
            {
                _concentrationLineeffectImage.color = setColor;
            }
        }
    }

    public void ZipSet(bool isOn)
    {
       //s _zipImage.SetActive(isOn);
    }
}
