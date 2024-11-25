using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Teleport : Skill
{
    [SerializeField] GameObject skillPref;
    public override void Activate()
    {
        if(skillPref == null)
        {
            return;
        }

        //Teleport player and activate skill on the old pos and new pos
        Vector3 oldPos = MockUpPlayer.instance.transform.position;
        GameObject skill1 = Instantiate(skillPref);
        skill1.transform.position = oldPos;
        Lullaby.instance.OutSourceActivate(oldPos);

        Vector3 newPos = MockUpPlayer.instance.GetMousePosition();
        GameObject skill2 = Instantiate(skillPref);
        skill2.transform.position = newPos;
        Lullaby.instance.OutSourceActivate(newPos);

        MockUpPlayer.instance.transform.position = newPos;
    }
}
