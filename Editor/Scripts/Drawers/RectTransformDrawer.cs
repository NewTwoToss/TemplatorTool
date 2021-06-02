// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 02.05.2021
// =================================================================================================

using System;
using Plugins.Templator.Editor.Scripts.ComponentProperties;
using Plugins.Templator.Editor.Scripts.Core;
using Plugins.Templator.Editor.Scripts.Drawers.Base;
using Plugins.Templator.Editor.Scripts.Drawers.Selectors;
using UnityEditor;
using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Drawers
{
    [Serializable]
    public class RectTransformDrawer : BaseDrawer, IPropertiesRectTransform
    {
        public override string Type => "Rect Transform";

#region [INSPECTOR]

        public string Name { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }
        
        public int IndexAnchor { get; private set; }

        public int IndexPivot { get; private set; }

#endregion

        public RectTransformDrawer(Rect rect, DTemplatorCore core) : base(rect, core)
        {
            Name = "RTName";
            Width = core.DefaultValues.RectTransform.Width;
            Height = core.DefaultValues.RectTransform.Height;
            nodeBackgroundColor = core.DefaultValues.RectTransform.NodeColor;
        }
        
        public RectTransformDrawer(Rect rect, DTemplatorCore core, IPropertiesRectTransform drawer) 
            : base(rect, core)
        {
            Name = drawer.Name;
            Width = drawer.Width;
            Height = drawer.Height;
            IndexAnchor = drawer.IndexAnchor;
            IndexPivot = drawer.IndexPivot;
            nodeBackgroundColor = core.DefaultValues.RectTransform.NodeColor;
        }

        public override void DrawNode()
        {
            DrawNodeBackground();

            var icon = new GUIContent(EditorGUIUtility.IconContent("d_RectTransform Icon")).image;
            DrawNodeTitle(icon);

            var labelText = $"{Name} [{Width}x{Height}]";
            DrawBody(labelText);
        }

        public override void DrawInspector()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Name");
            Name = GUILayout.TextField(Name, MAX_TEXT_FIELD_LENGTH, GUILayout.Width(200));
            GUILayout.EndHorizontal();

            GUISpaceBig();

            Width = Mathf.Clamp(EditorGUILayout.IntField("Width", Width), 2, MAX_NUMBER_VALUE);
            Height = Mathf.Clamp(EditorGUILayout.IntField("Height", Height), 2, MAX_NUMBER_VALUE);
            
            GUISeparator();
            
            GUILayout.BeginHorizontal();
            IndexAnchor = AnchorsSelectorDrawer.Draw();
            IndexPivot = PivotSelectorDrawer.Draw();
            GUILayout.EndHorizontal();
        }
    }
}