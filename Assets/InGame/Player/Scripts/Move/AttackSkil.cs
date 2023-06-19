using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[System.Serializable]
public class AttackSkil : IPlayerAction
{
    [SerializeField] private PlayableDirector _timeLine;


    public void UseSkill()
    {
        _playerControl.Model.SetActive(false);
        _timeLine.gameObject.transform.position = _playerControl.PlayerT.position;
        _timeLine.Play();
    }
}
