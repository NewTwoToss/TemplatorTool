// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 01.05.2021
// =================================================================================================

using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts.Drawers.Base
{
    public abstract class BaseDrawer
    {
        protected const int MAX_NUMBER_VALUE = 9_999;

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

        public abstract void DrawNode(int index, int level);

        public abstract void DrawInspector();

        protected void DrawNodeBackground()
        {
            GUI.color = nodeBackgroundColor;
            GUI.Box(rect, string.Empty, data._skin.GetStyle("NodeBodyBg"));
            GUI.color = Color.white;
        }

        protected void DrawNodeTitle(Texture icon, int index, int level)
        {
            GUI.color = nodeBackgroundColor;

            GUI.DrawTexture(new Rect(rect.x + 4, rect.y + 4, 16, 16), icon);

            GUI.Label(new Rect(new Vector2(rect.x + 24, rect.y),
                    new Vector2(rect.width - 24, 20)),
                //$"{Type} - [{index}] [{level}]",
                Type,
                data._skin.GetStyle("NodeTitle"));

            GUI.color = Color.white;
        }

        public void ShiftUp(int countDeleteNodes)
        {
            var newRectPositionY = rect.y - countDeleteNodes * data._nodeShiftVertical;
            var newRectPosition = new Vector2(rect.x, newRectPositionY);
            var newRect = new Rect(newRectPosition, rect.size);
            rect = newRect;
        }

        public void ShiftDown()
        {
            var newRectPosition = new Vector2(rect.x, rect.y + data._nodeShiftVertical);
            var newRect = new Rect(newRectPosition, rect.size);
            rect = newRect;
        }

        protected void GUISpaceSmall() => GUILayout.Space(4);

        protected void GUISpaceBig() => GUILayout.Space(10);
    }
}