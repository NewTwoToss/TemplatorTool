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
                _shortcutsIcon = new GUIContent(EditorGUIUtility
                        .IconContent("d__Help")).image;
                _infoLabelStyle = new GUIStyle
                {
                    normal = {textColor = Color.gray},
                    alignment = TextAnchor.MiddleLeft
                };

                _infoShortcuts = new StringBuilder();
                _infoShortcuts.Append("[Ctrl + Keypad0] Add RectTransform");
                _infoShortcuts.Append("\n\n---------------------------------\n\n");
                _infoShortcuts.Append("[Ctrl + Keypad1] Add Image ");
                _infoShortcuts.Append("\n\n---------------------------------\n\n");
                _infoShortcuts.Append("[Ctrl + Keypad2] Add Text");
                _infoShortcuts.Append("\n\n---------------------------------\n\n");
                _infoShortcuts.Append("[Ctrl + Keypad3] Add Button");
                _infoShortcuts.Append("\n\n---------------------------------\n\n");
                _infoShortcuts.Append("[Ctrl + Del] Delete Node");

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
            
            if (GUI.Button(new Rect(200, 1, 90, 22), "Template 01"))
            {
                Core.CreateTemplate01();
            }
            
            if (GUI.Button(new Rect(296, 1, 90, 22), "Template 02"))
            {
                Core.CreateTemplate02();
            }

            GUI.Label(new Rect(pRect.width - 144, 2, 20, 20),
                new GUIContent(_shortcutsIcon, _infoShortcuts.ToString()));

            GUI.Label(new Rect(pRect.width - 120, 0, 110, 24),
                $"Templator v{Core.Version}",
                _infoLabelStyle);
        }
    }
}