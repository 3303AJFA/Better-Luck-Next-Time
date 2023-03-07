using DS.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS.Elements
{
    using Data.Save;
    using Enumerations;

    public class DSStartBattleChoiceNode : DSNode
    {
        public override void Initialize(string nodeName, DSGraphView dsGraphView, Vector2 position)
        {
            base.Initialize(nodeName, dsGraphView, position);

            DialogueType = DSDialogueType.StartBattleChoice;
        }

        public override void Draw()
        {
            base.Draw();

            RefreshExpandedState();
        }
    }
}
