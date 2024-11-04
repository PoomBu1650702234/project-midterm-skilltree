using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MockUpPlayer : MonoBehaviour
{
    public Skill[] skillSlots = new Skill[4];

    void Update()
    {
        if(Input.GetKeyDown("1"))
        {
            print("Fire1");
            Skill testSkill = SkillTreeManager.skillTreeManagerInstance.GetSkillInTree(SkillTreeManager.skillTreeManagerInstance.skillTreeList[0].rootSkill,"FireBallSpread_MoreSpread");
            if(testSkill != null)
            {
                print($"found {testSkill.SkillName}");
            }
            
            List<Skill> skillListBT = SkillTreeManager.skillTreeManagerInstance.GetTrackBackSkillToParentList(new List<Skill> {testSkill});
            foreach(Skill skill in skillListBT)
            {
                print(skill.SkillName);
            }
            if(skillSlots[0] != null)
            {
                
            }
        }
        if(Input.GetKeyDown("2"))
        {
            print("Fire2");
            if(skillSlots[1] != null)
            {

            }
        }
        if(Input.GetKeyDown("3"))
        {
            print("Fire3");
            if(skillSlots[2] != null)
            {

            }
        }
        if(Input.GetKeyDown("4"))
        {
            print("Fire4");
            if(skillSlots[3] != null)
            {

            }
        }
    }
}
