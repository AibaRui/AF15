using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Skill
{

    [Serializable]
    public class Skill2 : ISkillable
    {

        [SerializeField, Tooltip("スキルの開放に必要なスキルポイント")]
        private int _skillPoint;

        [SerializeField]
        private PlayerControl _playerController;

        //スキルを自由に書いてください
        public bool OnSkill()
        {
            if (_playerController.PlayerStatus.SkillPoint.NowSkillPoint >= _skillPoint)
            {
                _playerController.PlayerStatus.SkillPoint.NowSkillPoint -= _skillPoint;
                _playerController._stepInfo.StepLevel2();
                return true;
            }

            Debug.Log("スキル2の開放に失敗しました。");
            return false;

            return true;//仮

        }
    }
}


