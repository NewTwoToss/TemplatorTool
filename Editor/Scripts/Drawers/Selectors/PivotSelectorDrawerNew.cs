// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 05.06.2021
// =================================================================================================

using Plugins.Templator.Editor.Scripts.Core;
using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Drawers.Selectors
{
    public class PivotSelectorDrawerNew
    {
        private readonly GUIStyle _styleButton;
        private readonly GUIContent[] _contents;

        public PivotSelectorDrawerNew(DTemplatorCore core)
        {
            var icons = core.Icons.PivotSelector;
            _styleButton = core.Skin.GetStyle("button");
            _contents = new GUIContent[9];

            _contents[0] = new GUIContent(icons[0], "Top Left");
            _contents[1] = new GUIContent(icons[1], "Top Center");
            _contents[2] = new GUIContent(icons[2], "Top Right");
            _contents[3] = new GUIContent(icons[3], "Middle Left");
            _contents[4] = new GUIContent(icons[4], "Middle Center");
            _contents[5] = new GUIContent(icons[5], "Middle Right");
            _contents[6] = new GUIContent(icons[6], "Bottom Left");
            _contents[7] = new GUIContent(icons[7], "Bottom Center");
            _contents[8] = new GUIContent(icons[8], "Bottom Right");
        }

        public int Draw(int indexSelectedButton)
        {
            GUILayout.BeginVertical();
            GUILayout.Label("Anchors 123");
            indexSelectedButton = GUILayout.SelectionGrid(indexSelectedButton,
                _contents,
                3,
                _styleButton,
                GUILayout.Width(60),
                GUILayout.Height(60));
            GUILayout.EndVertical();

            return indexSelectedButton;
        }
    }
}