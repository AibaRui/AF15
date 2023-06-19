using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Skill
{
    [Serializable]
    public class Skill : ISkillable
    {
        [SerializeField]
        private int _skillPoint;

        [SerializeField]
        private PlayerControl _playerController;

        //�X�L�������R�ɏ����Ă�������
        public bool OnSkill()
        {
            Debug.Log("FFF");
            if (_playerController.PlayerStatus.SkillPoint.NowSkillPoint >= _skillPoint)
            {
                _playerController.PlayerStatus.SkillPoint.NowSkillPoint -= _skillPoint;
                _playerController._stepInfo.StepLevel1();
                Debug.Log("�X�L���J���ɐ������܂����B");
                return true;
            }

            Debug.Log("�X�L���J���Ɏ��s���܂����B");
            return false;

            return true;//��
            
        }
    }
}


