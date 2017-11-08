namespace RPG.ItemSystem
{
    public class QualityDatabase : ScriptableObjectDatabase<Quality>
    {
        public int GetIndex(string name)
        {
            return database.FindIndex(a => a.Name == name);
        }
    }
}
