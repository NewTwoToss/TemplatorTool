// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 01.05.2021
// =================================================================================================

using System;
using Plugins.Templator.Editor.Scripts.Core;
using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Drawers.Base
{
    [Serializable]
    public abstract class BaseDrawer
    {
        protected const int MAX_NUMBER_VALUE = 9_999;
        protected const int MAX_TEXT_FIELD_LENGTH = 24;
        protected const int MAX_TEXT_AREA_LENGTH = 100;

        protected Rect rect;
        protected readonly DTemplatorCore core;
        protected Color nodeBackgroundColor = new Color(1.0f, 1.0f, 1.0f);
        private GUIStyle _styleSeparator;
        
        public abstract string Type { get; }

        public Rect Rect
        {
            get => rect;
            set => rect = value;
        }

        protected BaseDrawer(Rect rect, DTemplatorCore core)
        {
            this.rect = rect;
            this.core = core;
            _styleSeparator = core.Skin.GetStyle("InspectorSeparator");
        }

        public abstract void DrawNode();

        public abstract void DrawInspector();

        protected void DrawNodeBackground()
        {
            GUI.color = nodeBackgroundColor;
            GUI.Box(rect, string.Empty, core.Skin.GetStyle("NodeBodyBg"));
            GUI.color = Color.white;
        }

        protected void DrawNodeTitle(Texture icon)
        {
            GUI.color = nodeBackgroundColor;

            GUI.DrawTexture(new Rect(rect.x + 4, rect.y + 4, 16, 16), icon);

            GUI.Label(new Rect(new Vector2(rect.x + 24, rect.y),
                    new Vector2(rect.width - 24, 20)),
                Type,
                core.Skin.GetStyle("NodeTitle"));

            GUI.color = Color.white;
        }

        protected virtual void DrawBody(string labelText)
        {
            var rectPosition = new Vector2(rect.x + 10, rect.y + 20);
            var rectSize = new Vector2(rect.width - 20, 40);
            GUI.Label(new Rect(rectPosition, rectSize),
                labelText,
                core.Skin.GetStyle("NodeText"));
        }

        public void ShiftUp(int countDeleteNodes)
        {
            var newRectPositionY = rect.y - countDeleteNodes * core.DrawValues.NodeShiftVertical;
            var newRectPosition = new Vector2(rect.x, newRectPositionY);
            var newRect = new Rect(newRectPosition, rect.size);
            rect = newRect;
        }

        public void ShiftDown(int countNodes = 1)
        {
            var newRectPositionY = rect.y + countNodes * core.DrawValues.NodeShiftVertical;
            var newRectPosition = new Vector2(rect.x, newRectPositionY);
            var newRect = new Rect(newRectPosition, rect.size);
            rect = newRect;
        }

        protected void GUISpaceSmall() => GUILayout.Space(4);

        protected void GUISpaceBig() => GUILayout.Space(10);

        protected void GUISeparator()
        {
            GUILayout.Label("------------------------------------",
                _styleSeparator, 
                GUILayout.Height(32));
        }
    }
}