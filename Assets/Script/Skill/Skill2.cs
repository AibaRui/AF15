using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Skill
{

    [Serializable]
    public class Skill2 : ISkillable
    {

        [SerializeField, Tooltip("�X�L���̊J���ɕK�v�ȃX�L���|�C���g")]
        private int _skillPoint;

        [SerializeField]
        private PlayerControl _playerController;

        //�X�L�������R�ɏ����Ă�������
        public bool OnSkill()
        {
            if (_playerController.PlayerStatus.SkillPoint.NowSkillPoint >= _skillPoint)
            {
                _playerController.PlayerStatus.SkillPoint.NowSkillPoint -= _skillPoint;
                _playerController._stepInfo.StepLevel2();
                return true;
            }

            Debug.Log("�X�L��2�̊J���Ɏ��s���܂����B");
            return false;

            return true;//��

        }
    }
}


