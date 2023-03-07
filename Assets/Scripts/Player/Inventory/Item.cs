using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Game.Player.Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "Game/Inventory/Item/new Item", order = 0)]
    public class Item : ScriptableObject
    {
        [ShowAssetPreview] public Sprite Icon;
    }
}
