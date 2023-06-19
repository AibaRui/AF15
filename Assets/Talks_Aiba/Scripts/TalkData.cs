using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "TalkData")]

public class TalkData : ScriptableObject
{
    [Header("話し相手の名前")]
    [SerializeField] private string _humanName;

    [Header("話す内容")]
    [SerializeField] private List<string> _talkData = new List<string>();

    public string HumanName => _humanName;

    public List<string> DataTalk => _talkData;

}
