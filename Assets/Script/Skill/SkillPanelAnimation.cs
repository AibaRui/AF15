using UnityEngine;
using DG.Tweening;

public class SkillPanelAnimation : MonoBehaviour
{
    [SerializeField]
    private float _animTime = 1;

    private void OnEnable()
    {
        transform.DOScale(Vector3.one, _animTime);
    }
}
