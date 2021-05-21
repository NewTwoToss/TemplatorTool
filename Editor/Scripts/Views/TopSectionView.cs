// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 17.05.2021
// =================================================================================================

using Plugins.Templator.Editor.Scripts.Views.Base;
using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Views
{
    public class TopSectionView : BaseView
    {
        private bool _initialized;
        private Texture _whiteTexture;
        private GUIStyle _infoLabelStyle;

        public override void DrawGUI(Rect pRect)
        {
            if (!_initialized)
            {
                _whiteTexture = Texture2D.whiteTexture;
                _infoLabelStyle = new GUIStyle
                {
                    normal = {textColor = Color.gray},
                    alignment = TextAnchor.MiddleRight
                };
                _initialized = true;
            }

            var topSectionRect = new Rect(0, 0, pRect.width, 24);
            GUI.color = new Color(0.1f, 0.1f, 0.1f, 0.8f);
            GUI.DrawTexture(topSectionRect, _whiteTexture);
            GUI.color = Color.white;

            GUI.enabled = Core.UndoRedo.IsUndoStack;
            if (GUI.Button(new Rect(10, 1, 60, 22), "Undo"))
            {
                Core.UndoRedo.Undo();
            }

            GUI.enabled = Core.UndoRedo.IsRedoStack;
            if (GUI.Button(new Rect(76, 1, 60, 22), "Redo"))
            {
                Core.UndoRedo.Redo();
            }

            GUI.enabled = true;


            GUI.Label(new Rect(pRect.width - 230, 0, 220, 24),
                $"Templator v{Core.Version}",
                _infoLabelStyle);
        }
    }
}