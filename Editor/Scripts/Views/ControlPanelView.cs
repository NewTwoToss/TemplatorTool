// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 02.05.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.Scripts.Views.Base;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts.Views
{
    public class ControlPanelView : BaseView
    {
        private bool _initialized;
        private Color _colorCreateButton = new Color(72.0f, 208.0f, 72.0f);

        public override void DrawGUI(Rect pRect)
        {
            if (!_initialized)
            {
                _colorCreateButton = new Color(0.1f, 1.0f, 0.1f);
                _initialized = true;
            }
            
            if (Application.isPlaying) return;

            var rect = new Rect(pRect.width / 2 - 200, pRect.height - 80, 400, 70);

            DrawBoxGUI(rect, "Control Panel", TextAnchor.UpperLeft);
            DrawClearButton(rect);
            DrawCreateButton(rect);

            GUI.color = Color.white;
            GUI.enabled = true;
        }

        private void DrawClearButton(Rect rect)
        {
            GUI.enabled = true;
            GUI.color = Color.red;
            if (GUI.Button(new Rect(rect.x + 4, rect.y + 32, 100, 30), "Clear"))
            {
                Data.ResetTool();
            }
        }

        private void DrawCreateButton(Rect rect)
        {
            var sourceNode = Data.SourceNode;
            
            GUI.enabled = sourceNode.IsPossibleCreateProcess();
            GUI.color = _colorCreateButton;
            if (GUI.Button(new Rect(rect.x + rect.width - 104, rect.y + 32, 100, 30), "CREATE"))
            {
                if (sourceNode.IsPossibleCreateProcess())
                {
                    sourceNode.Create();
                }
                else
                {
                    Debug.Log("NULL OR CHILD COUNT = 0");
                }
            }
        }
    }
}