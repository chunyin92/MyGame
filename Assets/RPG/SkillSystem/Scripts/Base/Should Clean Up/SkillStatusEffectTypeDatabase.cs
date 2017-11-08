namespace RPG.SkillSystem
{
    public class SkillStatusEffectTypeDatabase : ScriptableObjectDatabase<SkillStatusEffectType>
    {
        public int GetIndex(string name)
        {
            return database.FindIndex(a => a.Name == name);
        }
    }
}
