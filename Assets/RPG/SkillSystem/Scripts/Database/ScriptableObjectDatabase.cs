using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

namespace RPG
{
    public class ScriptableObjectDatabase<T> : ScriptableObject where T : class
    {
        [SerializeField] protected List<T> database = new List<T>();

        public List<T> GetDatabaseList
        {
            get { return database; }
        }

        public void Add(T itemToAdd)
        {
            database.Add(itemToAdd);
            EditorUtility.SetDirty(this);
        }

        public void Insert(int index, T itemToInsert)
        {
            database.Insert(index, itemToInsert);
            EditorUtility.SetDirty(this);
        }

        public void Remove(T itemToRemove)
        {
            database.Remove(itemToRemove);
            EditorUtility.SetDirty(this);
        }

        public void Remove(int index)
        {
            database.RemoveAt(index);
            EditorUtility.SetDirty(this);
        }

        public void MoveUp (int index)
        {
            if (index <= 0 || index >= database.Count)
            {
                Debug.Log ("Cannot move up");
                return;
            }

            database.Insert (index - 1, database.ElementAt (index));
            database.RemoveAt (index + 1);
            EditorUtility.SetDirty (this);
        }

        public void MoveDown (int index)
        {
            if (index >= database.Count - 1 || index < 0)
            {
                Debug.Log ("Cannot move down");
                return;
            }

            database.Insert (index + 2, database.ElementAt (index));
            database.RemoveAt (index);
            EditorUtility.SetDirty (this);
        }

        public int Count
        {
            get { return database.Count; }
        }

        public T Get(int index)
        {
            return database.ElementAt(index);
        }

        public void Replace(int index, T itemToReplaceWith)
        {
            database[index] = itemToReplaceWith;
            EditorUtility.SetDirty(this);
        }

        public static U GetDatabase<U>(string dbPath, string dbName) where U : ScriptableObject
        {
            string dbFullPath = @"Assets/" + dbPath + "/" + dbName;
            U db = AssetDatabase.LoadAssetAtPath(dbFullPath, typeof(U)) as U;

            if (db == null)
            {
                //check to see if the folder exists
                if (!AssetDatabase.IsValidFolder(@"Assets/" + dbPath))
                    AssetDatabase.CreateFolder(@"Assets", dbPath);

                //create the database and refresh the AssetDatabase
                db = CreateInstance<U>() as U;
                AssetDatabase.CreateAsset(db, dbFullPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            return db;
        }
    }
}
