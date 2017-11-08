using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem
{
    public interface IQuality
    {
        string Name { get; set; }
        Sprite Icon { get; set; }
    }
}
