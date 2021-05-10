// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 01.05.2021
// =================================================================================================

using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Drawers
{
    public abstract class BaseDrawer
    {
        protected Rect rect;
        protected readonly DTestScriptable data;
        protected Color nodeBackgroundColor = new Color(1.0f, 1.0f, 1.0f);

        public abstract string Type { get; }

        public Rect Rect => rect;

        protected BaseDrawer(Rect rect, DTestScriptable data)
        {
            this.rect = rect;
            this.data = data;
        }

        public abstract void DrawNode();

        protected virtual void DrawNodeTitle(Texture icon)
        {
            GUI.color = nodeBackgroundColor;

            GUI.DrawTexture(new Rect(rect.x + 4, rect.y + 4, 16, 16), icon);

            GUI.Label(new Rect(new Vector2(rect.x + 24, rect.y),
                    new Vector2(rect.width - 24, 20)),
                Type,
                data._skin.GetStyle("NodeTitle"));

            GUI.color = Color.white;
        }

        public abstract void DrawInspector();

        public void ShiftY()
        {
            var newRectPosition = new Vector2(rect.x, rect.y + data._nodeShiftVertical);
            var newRect = new Rect(newRectPosition, rect.size);
            rect = newRect;
        }
    }
}