using UnityEngine;
using System.Collections;
using UnityEditor;

namespace RPG.ItemSystem
{
    [System.Serializable]
    public class Weapon : BaseItem, IWeapon
    {
        [SerializeField] int _attack;

        public int Attack
        {
            get { return _attack; }
            set { _attack = value; }
        }

        public Weapon()
        {

        }

        public override void OnGUI()
        {
            base.OnGUI();
            _attack = EditorGUILayout.IntField("Attack", _attack);
        }
    }
}
