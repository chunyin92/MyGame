namespace RPG.SkillSystem
{
    public class SkillTargetTypeDatabase : ScriptableObjectDatabase<SkillTargetType>
    {
        public int GetIndex(string name)
        {
            return database.FindIndex(a => a.Name == name);
        }
    }
}
