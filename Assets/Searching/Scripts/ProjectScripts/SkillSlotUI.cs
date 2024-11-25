using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
    [SerializeField] private int slot;
    public Skill skill;
    SkillTreeManager skillTreeManagerInstance;
    
    
    void Start ()
    {
        skillTreeManagerInstance =  SkillTreeManager.skillTreeManagerInstance;
        skillTreeManagerInstance.OnSkillSlotsChange += UpdateUI;

        UpdateUI();
    }

    void UpdateUI()
    {
        ClearUI();
        skill = MockUpPlayer.instance.skillSlots[slot];
        if(skill == null)
        {
            return;
        }
        GameObject textObj = Instantiate(skillTreeManagerInstance.textUIPref);
        textObj.transform.SetParent(transform , false);
        TextMeshProUGUI textUI = textObj.GetComponent<TextMeshProUGUI>();
        textUI.text = $"{skill.SkillName}";

        GameObject imageObj = Instantiate(skillTreeManagerInstance.imageUIPref);
        imageObj.transform.SetParent(transform , false);
        Image skillimage = imageObj.GetComponent<Image>();

        if(skill.skillIcon != null)
        {
            skillimage.sprite = skill.skillIcon;
        }
    }
    void ClearUI()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
