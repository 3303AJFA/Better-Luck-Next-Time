using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.BattleSystem.Cards
{
    [CreateAssetMenu(fileName = "Cards list", menuName = "Game/Card/new Cards list", order = 0)]
    public class CardsListSO : ScriptableObject
    {
        public List<CardSO> AllAttackCards = new List<CardSO>();
    }
}
