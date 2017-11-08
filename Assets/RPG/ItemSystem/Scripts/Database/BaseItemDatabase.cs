namespace RPG.ItemSystem
{
    public class BaseItemDatabase : ScriptableObjectDatabase<BaseItem>
    {
        public int GetIndex(string name)
        {
            return database.FindIndex(a => a.Name == name);
        }
    }
}