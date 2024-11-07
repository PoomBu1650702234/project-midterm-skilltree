using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeUI : MonoBehaviour
{
    SkillTreeManager skillTreeManagerInstance;
    void Start()
    {
        skillTreeManagerInstance = SkillTreeManager.skillTreeManagerInstance;

        // Assuming OnSkillTreeChange is an event in SkillTreeManager, subscribe to it.
        skillTreeManagerInstance.OnSkillTreeChange += UiUpdate;
    }

    void UiUpdate()
    {
        print("Gott update signal");
    }
}
