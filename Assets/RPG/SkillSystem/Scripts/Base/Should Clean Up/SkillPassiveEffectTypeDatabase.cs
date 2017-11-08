namespace RPG.SkillSystem
{
    public class SkillPassiveEffectTypeDatabase : ScriptableObjectDatabase<SkillPassiveEffectType>
    {
        public int GetIndex(string name)
        {
            return database.FindIndex(a => a.Name == name);
        }
    }
}