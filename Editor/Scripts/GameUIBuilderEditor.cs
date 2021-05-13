// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 26.04.2021
// =================================================================================================

using System.Collections.Generic;
using Plugins.GameUIBuilder.Editor.Scripts.Views;
using Plugins.GameUIBuilder.Editor.Scripts.Views.Base;
using UnityEditor;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts
{
    public class GameUIBuilderEditor : EditorWindow
    {
        private List<BaseView> _views;

        private void OnEnable()
        {
            Debug.Log("[TOOL] OnEnable()");
            InitializeTool();
        }

        [InitializeOnLoadMethod]
        public static void OnProjectLoadedInEditor()
        {
            Debug.Log("[TOOL] OnProjectLoadedInEditor()1");
            InitializeTool();
        }

        [MenuItem("Toss Tools/Game UI Builder")]
        private static void UnityMenuGameUIBuilder()
        {
            InitializeTool();
            DrawMainWindow();
        }

        private static void DrawMainWindow()
        {
            var mainWindow = (GameUIBuilderEditor) GetWindow(typeof(GameUIBuilderEditor));
            mainWindow.titleContent.text = "Game UI Builder";
            mainWindow.titleContent.tooltip = "Game UI Builder for Unity projects";
            mainWindow.Show();
        }

        private static DTossCreator _core;

        private static void InitializeTool()
        {
            if (!(_core is null)) return;

            Debug.Log("[TOOL] InitializeTool()");
            Initialize();
        }

        private static void Initialize()
        {
            var guidValidatorSettings = AssetDatabase.FindAssets("ValidatorSettings");

            if (guidValidatorSettings.Length == 0) return;

            var pathValidatorSettings = AssetDatabase.GUIDToAssetPath(guidValidatorSettings[0]);

            _core = (DTossCreator) AssetDatabase.LoadAssetAtPath(
                pathValidatorSettings,
                typeof(DTossCreator));
            _core.Initialize();
        }

        private void OnGUI()
        {
            if (_views == null || _core == null)
            {
                CreateViews();
                InitializeViews();
                return;
            }

            var rectEditor = new Rect(0, 0, position.width, position.height);
            _views.ForEach(v => v.DrawGUI(rectEditor));
            _views.ForEach(v => v.ProcessEvent(Event.current, rectEditor));

            if (!_core.IsRepaint) return;

            Repaint();
            // TODO: Doriesit _rules.IsRepaint = false;
        }

        private void InitializeViews()
        {
            foreach (var view in _views)
            {
                view.Data = _core;
            }
        }

        private void CreateViews()
        {
            _views = new List<BaseView>();
            CreateView<ToolView>();
            CreateView<ToolInspectorView>();
            CreateView<ControlPanelView>();
            //CreateView<TestingView>();
        }

        private void CreateView<T>() where T : BaseView, new()
        {
            _views.Add(new T());
        }
    }
}