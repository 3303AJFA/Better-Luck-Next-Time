using DS.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace DS.Elements
{
    using Data.Save;
    using Utilities;
    using Game.Player.Inventory;

    public class DSTakeItemChoiceNode : DSSingleChoiceNode
    {
        public override void Initialize(string nodeName, DSGraphView dsGraphView, Vector2 position)
        {
            base.Initialize(nodeName, dsGraphView, position);

            DialogueType = Enumerations.DSDialogueType.TakeItemChoice;
        }

        protected override void AddExtensionContainer()
        {
            VisualElement customDataContainer = new VisualElement();

            customDataContainer.AddToClassList("ds-node__custom-data-container");

            ObjectField ItemField = DSElementUtility.CreateObjectField("Item", typeof(Item), callback =>
            {
                Item = (Item)callback.newValue;
            });
            ItemField.value = Item;

            customDataContainer.Add(ItemField);

            extensionContainer.Add(customDataContainer);

            base.AddExtensionContainer();
        }
    }
}
