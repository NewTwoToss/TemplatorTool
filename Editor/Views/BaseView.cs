// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 27.04.2021
// =================================================================================================

using Dash;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Views
{
    public abstract class BaseView
    {
        public DTestScriptable Data { get; set; }

        public abstract void DrawGUI(Rect pRect);

        public virtual void ProcessEvent(Event pEvent, Rect pRect)
        {
        }
        
        protected void DrawBoxGUI(Rect rect, string title, TextAnchor titleAlignment)
        {
            var style = DashEditorCore.Skin.GetStyle("ViewBase");
            style.alignment = titleAlignment;

            style.contentOffset = titleAlignment switch
            {
                TextAnchor.UpperLeft => new Vector2(10, 0),
                TextAnchor.UpperRight => new Vector2(-10, 0),
                _ => Vector2.zero
            };

            GUI.Box(rect, "", style);
            GUI.Box(new Rect(rect.x, rect.y, rect.width, 32), title, style);
        }
    }
}