using UnityEditor;
using System.Collections.Generic;

namespace RPG.SkillSystem
{
    public class SkillDatabase : ScriptableObjectDatabase<SkillData>
    {
        public int GetIndex(string name)
        {
            return database.FindIndex(a => a.Name == name);
        }

        // testing, try to get all skills???
        public List<SkillData> GetSkillList()
        {
            List<SkillData> baseSkillList = new List<SkillData> ();

            for (int i = 0; i < database.Count; i++)
                baseSkillList.Add (Get (i));

            return baseSkillList;
        }
    }
}