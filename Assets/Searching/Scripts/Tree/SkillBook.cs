using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tree
{

    public class SkillBook : MonoBehaviour
    {
        public SkillTreeOld attackSkillTree;

        SkillOld attack;
        SkillOld fireStorm;
        SkillOld fireBall;
        SkillOld fireBlast;
        SkillOld fireWave;
        SkillOld fireExplosion;

        public void Start()
        {
            #region Depicting the skill tree
            // build skill tree
            // └── Attack
            //     └── FireStorm
            //         ├── FireBlast
            //         └── FireBall
            //             └── FireWave
            //                 └── FireExplosion
            #endregion

            attack = new SkillOld("Attack");
            attack.isAvailable = true;

            fireStorm = new SkillOld("FireStorm");
            fireBall = new SkillOld("FireBall");
            fireBlast = new SkillOld("FireBlast");
            fireWave = new SkillOld("FireWave");
            fireExplosion = new SkillOld("FireExplosion");

            attack.nextSkills.Add(fireStorm);
            fireStorm.nextSkills.Add(fireBlast);
            fireStorm.nextSkills.Add(fireBall);
            fireBall.nextSkills.Add(fireWave);
            fireWave.nextSkills.Add(fireExplosion);


            this.attackSkillTree = new SkillTreeOld(attack);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                attackSkillTree.rootSkill.PrintSkillTreeHierarchy("");
                // attackSkillTree.rootSkill.PrintSkillTree();
                Debug.Log("====================================");
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                attack.Unlock();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                fireStorm.Unlock();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                fireBall.Unlock();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                fireBlast.Unlock();
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                fireWave.Unlock();
            }
        }
    }

}