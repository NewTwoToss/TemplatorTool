// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 10.05.2021
// =================================================================================================

using UnityEditor;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Views
{
    public class TestingView : BaseView
    {
        public override void DrawGUI(Rect pRect)
        {
            var icon = new GUIContent(EditorGUIUtility.IconContent("d_Canvas Icon").image);
            GUI.DrawTexture(new Rect(400, 10, 16, 16), icon.image);
        }
    }
}