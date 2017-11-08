namespace RPG.SkillSystem
{
    public class SkillTypeDatabase : ScriptableObjectDatabase<SkillType>
    {
        public int GetIndex(string name)
        {
            return database.FindIndex(a => a.Name == name);
        }
    }
}
