using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillTree
{
    public Skill rootSkill = null;

    public SkillTree(Skill _rootSkill)
    {
        rootSkill = _rootSkill;
    }
}
