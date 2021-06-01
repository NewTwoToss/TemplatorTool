// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 30.05.2021
// =================================================================================================

using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Drawers
{
    public static class PivotSelectorDrawer
    {
        private static GUIStyle _styleButton;
        private static int _indexPivot = 4; // 4 = Index of Middle Center Pivot 
        private static GUIContent[] _contents;

        public static void Load(GUISkin toolSkin)
        {
            _styleButton = toolSkin.GetStyle("button");
            
            _contents = new GUIContent[9];

            var icon = Resources.Load<Texture2D>("Textures/PivotIcons/PivotIconTopLeft");
            _contents[0] = new GUIContent(icon, "Top Left");

            icon = Resources.Load<Texture2D>("Textures/PivotIcons/PivotIconTopCenter");
            _contents[1] = new GUIContent(icon, "Top Center");

            icon = Resources.Load<Texture2D>("Textures/PivotIcons/PivotIconTopRight");
            _contents[2] = new GUIContent(icon, "Top Right");

            icon = Resources.Load<Texture2D>("Textures/PivotIcons/PivotIconMiddleLeft");
            _contents[3] = new GUIContent(icon, "Middle Left");

            icon = Resources.Load<Texture2D>("Textures/PivotIcons/PivotIconMiddleCenter");
            _contents[4] = new GUIContent(icon, "Middle Center");

            icon = Resources.Load<Texture2D>("Textures/PivotIcons/PivotIconMiddleRight");
            _contents[5] = new GUIContent(icon, "Middle Right");

            icon = Resources.Load<Texture2D>("Textures/PivotIcons/PivotIconBottomLeft");
            _contents[6] = new GUIContent(icon, "Bottom Left");

            icon = Resources.Load<Texture2D>("Textures/PivotIcons/PivotIconBottomCenter");
            _contents[7] = new GUIContent(icon, "Bottom Center");

            icon = Resources.Load<Texture2D>("Textures/PivotIcons/PivotIconBottomRight");
            _contents[8] = new GUIContent(icon, "Bottom Right");
        }

        public static int DrawPivot()
        {
            GUILayout.BeginVertical();
            GUILayout.Label("Pivot");
            _indexPivot = GUILayout.SelectionGrid(_indexPivot,
                _contents,
                3,
                _styleButton,
                GUILayout.Width(24 * 3),
                GUILayout.Height(24 * 3));
            GUILayout.EndVertical();

            return _indexPivot;
        }
    }
}