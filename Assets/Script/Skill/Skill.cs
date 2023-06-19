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

        //スキルを自由に書いてください
        public bool OnSkill()
        {
            Debug.Log("FFF");
            if (_playerController.PlayerStatus.SkillPoint.NowSkillPoint >= _skillPoint)
            {
                _playerController.PlayerStatus.SkillPoint.NowSkillPoint -= _skillPoint;
                _playerController._stepInfo.StepLevel1();
                Debug.Log("スキル開放に成功しました。");
                return true;
            }

            Debug.Log("スキル開放に失敗しました。");
            return false;

            return true;//仮
            
        }
    }
}


