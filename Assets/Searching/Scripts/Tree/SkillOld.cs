using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tree
{

    public class SkillOld
    {
        public string name;
        public bool isUnlocked;
        public bool isAvailable;
        public List<SkillOld> nextSkills;

        public SkillOld(string name)
        {
            this.name = name;
            isUnlocked = false;
            nextSkills = new List<SkillOld>();
        }

        public void Unlock()
        {
            if (!isAvailable)
            {
                throw new System.Exception("Skill is not available to unlock.");
            }

            if (isUnlocked)
            {
                Debug.Log($"Skill {name} is already unlocked.");
                return;
            }

            isUnlocked = true;
            for (int i = 0; i < nextSkills.Count; i++)
            {
                nextSkills[i].isAvailable = true;
            }
        }

        public void PrintSkillTree()
        {
            Debug.Log($"Skill: {name} available: {isAvailable} unlocked: {isUnlocked}");
            for (int i = 0; i < nextSkills.Count; i++)
            {
                nextSkills[i].PrintSkillTree();
            }
        }

        public void PrintSkillTreeHierarchy(string indent)
        {
            Debug.Log($"{indent}- Skill: {name} available: {isAvailable} unlocked: {isUnlocked}");
            foreach (var skill in this.nextSkills)
            {
                skill.PrintSkillTreeHierarchy(indent + "    "); // Increase indentation for the next level
            }
        }

    }

    public class SkillTreeOld
    {
        public SkillOld rootSkill;

        public SkillTreeOld(SkillOld rootSkill)
        {
            this.rootSkill = rootSkill;
        }
    }
}