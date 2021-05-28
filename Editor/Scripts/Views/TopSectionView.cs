// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 17.05.2021
// =================================================================================================

using System.Text;
using Plugins.Templator.Editor.Scripts.Views.Base;
using UnityEditor;
using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Views
{
    public class TopSectionView : BaseView
    {
        private bool _initialized;
        private Texture _backgroundTexture;
        private Texture _shortcutsIcon;
        private Color _backgroundColor;
        private GUIStyle _infoLabelStyle;
        private StringBuilder _infoShortcuts;

        public override void DrawGUI(Rect pRect)
        {
            if (!_initialized)
            {
                _backgroundTexture = Texture2D.whiteTexture;
                _backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.8f);
                _shortcutsIcon = new GUIContent(EditorGUIUtility.IconContent("console.infoicon.sml")).image;
                _infoLabelStyle = new GUIStyle
                {
                    normal = {textColor = Color.gray},
                    alignment = TextAnchor.MiddleRight
                };

                _infoShortcuts = new StringBuilder();
                _infoShortcuts.Append("[Ctrl + R] Add RectTransform");
                _infoShortcuts.Append("\n---------------------------------\n");
                _infoShortcuts.Append("[Ctrl + I] Add Image ");
                _infoShortcuts.Append("\n---------------------------------\n");
                _infoShortcuts.Append("[Ctrl + B] Add Button");
                _infoShortcuts.Append("\n---------------------------------\n");
                _infoShortcuts.Append("[Ctrl + T] Add Text");

                _initialized = true;
            }

            var topSectionRect = new Rect(0, 0, pRect.width, 24);
            GUI.color = _backgroundColor;
            GUI.DrawTexture(topSectionRect, _backgroundTexture);
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

            GUI.Label(new Rect(pRect.width - 150, 2, 20, 20),
                new GUIContent(_shortcutsIcon, _infoShortcuts.ToString()));

            GUI.Label(new Rect(pRect.width - 130, 0, 120, 24),
                $"Templator v{Core.Version}",
                _infoLabelStyle);
        }
    }
}