// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 02.05.2021
// =================================================================================================

using System.Text;
using Plugins.Templator.Editor.Scripts.Views.Base;
using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Views
{
    public class ControlPanelView : BaseView
    {
        private const int PANEL_WIDTH = 500;

        private bool _initialized;
        private StringBuilder _sb;
        private Color _colorClearButton;
        private Color _colorCreateButton;

        public override void DrawGUI(Rect pRect)
        {
            if (!_initialized)
            {
                _colorClearButton = new Color(1.0f, 0.2f, 0.2f);
                _colorCreateButton = new Color(0.0f, 1.0f, 0.0f);
                _sb = new StringBuilder();
                _initialized = true;
            }

            if (Application.isPlaying) return;

            var rectPosition = new Vector2(pRect.width / 2 - PANEL_WIDTH / 2.0f, pRect.height - 80);
            var rectSize = new Vector2(PANEL_WIDTH, 70);
            var rect = new Rect(rectPosition, rectSize);

            DrawBoxGUI(rect, "Control Panel", TextAnchor.UpperLeft);
            DrawClearButton(rect);
            DrawInfoLabels(rect);
            DrawCreateButton(rect);

            GUI.color = Color.white;
            GUI.enabled = true;
        }

        private void DrawClearButton(Rect pRect)
        {
            GUI.enabled = !Core.SourceNode.IsChildCountZero;
            GUI.color = _colorClearButton;
            if (GUI.Button(new Rect(pRect.x + 4, pRect.y + 32, 100, 30), "Clear"))
            {
                Core.ClearHierarchy();
            }
        }

        private void DrawInfoLabels(Rect pRect)
        {
            var sourceNode = Core.SourceNode;
            var isFirstError = false;
            _sb.Clear();

            if (sourceNode.IsSourceNull)
            {
                _sb.Append("Source Node Parent = Null !");
                isFirstError = true;
            }

            if (sourceNode.IsChildCountZero)
            {
                if (isFirstError)
                {
                    _sb.Append("\n");
                }

                _sb.Append("Source Node Child Count = 0 !");
            }

            GUI.enabled = true;
            GUI.color = Color.white;
            var rect = new Rect(pRect.x + 104, pRect.y + 32, PANEL_WIDTH - 208, 30);
            GUI.Label(rect, _sb.ToString(), Core.Skin.GetStyle("ControlPanelLabel"));
        }

        private void DrawCreateButton(Rect pRect)
        {
            var sourceNode = Core.SourceNode;

            GUI.enabled = sourceNode.IsPossibleCreateProcess;
            GUI.color = _colorCreateButton;
            if (GUI.Button(new Rect(pRect.x + pRect.width - 104, pRect.y + 32, 100, 30), "CREATE"))
            {
                sourceNode.Create();
            }
        }
    }
}