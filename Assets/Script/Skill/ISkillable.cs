using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skill
{
    public interface ISkillable
    {
        /// <summary>
        /// スキルの中身なので自由に書いてください
        /// </summary>
        /// <returns>スキルを開放できるかできないか</returns>
        public bool OnSkill();
    }
}

