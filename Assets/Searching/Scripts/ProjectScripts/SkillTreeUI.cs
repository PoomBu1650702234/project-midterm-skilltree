using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeUI : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void ButtonClick()
    {
        if(isActiveAndEnabled)
        {
            Close();
        }else
        {
            Open();
        }
        
    }


}
