// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 01.06.2021
// =================================================================================================

using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Drawers.Selectors
{
    public static class AnchorsSelectorDrawer
    {
        private static GUIStyle _styleButton;
        private static int _indexAnchor = 4; // 4 = Index of Middle Center Anchor 
        private static GUIContent[] _contents;

        public static void Load(GUISkin toolSkin)
        {
            _styleButton = toolSkin.GetStyle("button");

            _contents = new GUIContent[9];

            var icon = Resources.Load<Texture2D>("Textures/AnchorIcons/AnchorIconTopLeft");
            _contents[0] = new GUIContent(icon, "Top Left");

            icon = Resources.Load<Texture2D>("Textures/AnchorIcons/AnchorIconTopCenter");
            _contents[1] = new GUIContent(icon, "Top Center");

            icon = Resources.Load<Texture2D>("Textures/AnchorIcons/AnchorIconTopRight");
            _contents[2] = new GUIContent(icon, "Top Right");

            icon = Resources.Load<Texture2D>("Textures/AnchorIcons/AnchorIconMiddleLeft");
            _contents[3] = new GUIContent(icon, "Middle Left");

            icon = Resources.Load<Texture2D>("Textures/AnchorIcons/AnchorIconMiddleCenter");
            _contents[4] = new GUIContent(icon, "Middle Center");

            icon = Resources.Load<Texture2D>("Textures/AnchorIcons/AnchorIconMiddleRight");
            _contents[5] = new GUIContent(icon, "Middle Right");

            icon = Resources.Load<Texture2D>("Textures/AnchorIcons/AnchorIconBottomLeft");
            _contents[6] = new GUIContent(icon, "Bottom Left");

            icon = Resources.Load<Texture2D>("Textures/AnchorIcons/AnchorIconBottomCenter");
            _contents[7] = new GUIContent(icon, "Bottom Center");

            icon = Resources.Load<Texture2D>("Textures/AnchorIcons/AnchorIconBottomRight");
            _contents[8] = new GUIContent(icon, "Bottom Right");
        }

        public static int Draw()
        {
            GUILayout.BeginVertical();
            GUILayout.Label("Anchors");
            _indexAnchor = GUILayout.SelectionGrid(_indexAnchor,
                _contents,
                3,
                _styleButton,
                GUILayout.Width(60),
                GUILayout.Height(60));
            GUILayout.EndVertical();

            return _indexAnchor;
        }
    }
}