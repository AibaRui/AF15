using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skill
{
    public interface ISkillable
    {
        /// <summary>
        /// �X�L���̒��g�Ȃ̂Ŏ��R�ɏ����Ă�������
        /// </summary>
        /// <returns>�X�L�����J���ł��邩�ł��Ȃ���</returns>
        public bool OnSkill();
    }
}

