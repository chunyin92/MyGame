using UnityEngine;
using System.Collections;
using System.Linq;

namespace RPG.CharacterSystem
{
    public class CharacterDatabase : ScriptableObjectDatabase<CharacterData>
    {
        public int GetIndex(string name)
        {
            return database.FindIndex(a => a.Name == name);
        }

        public CharacterData Get (string name)
        {
            return database.ElementAt(database.FindIndex (a => a.Name == name));
        }

    }
}
