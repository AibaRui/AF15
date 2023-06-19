using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("攻撃の音")]
    [SerializeField] private List<AudioClip> _attackAudio = new List<AudioClip>();

    [Header("発砲音")]
    [SerializeField] private AudioClip _fire;

    [Header("着地の音")]
    [SerializeField] private AudioClip _land;

    [Header("ジャンプ")]
    [SerializeField] private AudioClip _jump;

    [Header("アイテムゲット")]
    [SerializeField] private AudioClip _itemGet;

    [Header("武器選択")]
    [SerializeField] private AudioClip _selsectWeapon;

    [Header("武器選択")]
    [SerializeField] private AudioClip _damage;

    [Header("シフトブレイク始め")]
    [SerializeField] private AudioClip _shiftStart;

    [Header("シフトブレイク始め")]
    [SerializeField] private AudioClip _shiftEnd;


    [Header("ステップ")]
    [SerializeField] private AudioClip _step;

    [Header("アシスト")]
    [SerializeField] private AudioClip _assistMove;


    [SerializeField] private AudioSource _audioSource;


    public void PlaySound(AudioType type)
    {
        if(type == AudioType.Attack)
        {
            var r = Random.Range(0, _attackAudio.Count);

            _audioSource.PlayOneShot(_attackAudio[r]);
        }
        else if (type == AudioType.Fire)
        {
            _audioSource.PlayOneShot(_fire);
        }
        else if (type == AudioType.Damage)
        {
            _audioSource.PlayOneShot(_damage);
        }
        else if (type == AudioType.ItemGet)
        {
            _audioSource.PlayOneShot(_itemGet);
        }
        else if (type == AudioType.Jump)
        {
            _audioSource.PlayOneShot(_jump);
        }
        else if (type == AudioType.Selsect)
        {
            _audioSource.PlayOneShot(_selsectWeapon);
        }
        else if (type == AudioType.Shift)
        {
            _audioSource.PlayOneShot(_shiftStart);
        }
        else if (type == AudioType.Land)
        {
            _audioSource.PlayOneShot(_land);
        }
        else if (type == AudioType.ShiftEnd)
        {
            _audioSource.PlayOneShot(_shiftEnd);
        }
        else if (type == AudioType.Step)
        {
            _audioSource.PlayOneShot(_step);
        }

    }

}

public enum AudioType
{
    Attack,
    Fire,
    Jump,
    ItemGet,
    Selsect,
    Damage,
    Shift,
    Land,
    Step,
    ShiftEnd,
}