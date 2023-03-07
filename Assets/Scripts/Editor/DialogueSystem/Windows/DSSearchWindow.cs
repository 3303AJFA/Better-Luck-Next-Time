using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DS.Windows
{
    using Elements;
    using Enumerations;

    public class DSSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        private DSGraphView graphView;
        private Texture2D indentationIcon;

        public void Initialize(DSGraphView dsGraphView)
        {
            graphView = dsGraphView;

            indentationIcon = new Texture2D(1, 1);
            indentationIcon.SetPixel(0, 0, Color.clear);
            indentationIcon.Apply();
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> searchTreeEntries = new List<SearchTreeEntry>()
            {
                new SearchTreeGroupEntry(new GUIContent("Create Elements")),
                new SearchTreeGroupEntry(new GUIContent("Dialogue Nodes"), 1),
                new SearchTreeEntry(new GUIContent("Single Choice", indentationIcon))
                {
                    userData = DSDialogueType.SingleChoice,
                    level = 2
                },
                new SearchTreeEntry(new GUIContent("Multiple Choice", indentationIcon))
                {
                    userData = DSDialogueType.MultipleChoice,
                    level = 2
                },
                new SearchTreeEntry(new GUIContent("Take Item Choice", indentationIcon))
                {
                    userData = DSDialogueType.TakeItemChoice,
                    level = 2
                },
                new SearchTreeEntry(new GUIContent("Start Battle Choice", indentationIcon))
                {
                    userData = DSDialogueType.StartBattleChoice,
                    level = 2
                },
                new SearchTreeGroupEntry(new GUIContent("Dialogue Groups"), 1),
                new SearchTreeEntry(new GUIContent("Single Group", indentationIcon))
                {
                    userData = new Group(),
                    level = 2
                }
            };

            return searchTreeEntries;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            Vector2 localMousePosition = graphView.GetLocalMousePosition(context.screenMousePosition, true);
            string defaultName = "DialogueName";

            switch (SearchTreeEntry.userData)
            {
                case DSDialogueType.SingleChoice:
                {
                    DSSingleChoiceNode singleChoiceNode = (DSSingleChoiceNode) graphView.CreateNode(defaultName, DSDialogueType.SingleChoice, localMousePosition);

                    graphView.AddElement(singleChoiceNode);

                    return true;
                }

                case DSDialogueType.MultipleChoice:
                {
                    DSMultipleChoiceNode multipleChoiceNode = (DSMultipleChoiceNode) graphView.CreateNode(defaultName, DSDialogueType.MultipleChoice, localMousePosition);

                    graphView.AddElement(multipleChoiceNode);

                    return true;
                }

                case DSDialogueType.TakeItemChoice:
                    {
                        DSTakeItemChoiceNode takeItemChoiceNode = (DSTakeItemChoiceNode)graphView.CreateNode(defaultName, DSDialogueType.TakeItemChoice, localMousePosition);

                        graphView.AddElement(takeItemChoiceNode);

                        return true;
                    }

                case DSDialogueType.StartBattleChoice:
                    {
                        DSStartBattleChoiceNode startBattleChoiceNode = (DSStartBattleChoiceNode)graphView.CreateNode(defaultName, DSDialogueType.StartBattleChoice, localMousePosition);

                        graphView.AddElement(startBattleChoiceNode);

                        return true;
                    }

                case Group _:
                {
                    graphView.CreateGroup("DialogueGroup", localMousePosition);

                    return true;
                }

                default:
                {
                    return false;
                }
            }
        }
    }
}