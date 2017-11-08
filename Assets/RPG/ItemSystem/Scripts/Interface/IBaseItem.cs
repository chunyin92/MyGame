using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem
{
    public interface IBaseItem
    {
        string Name { get; set; }
        Sprite Icon { get; set; }
        string Description { get; set; }
        Quality Quality { get; set; }
    }
}
