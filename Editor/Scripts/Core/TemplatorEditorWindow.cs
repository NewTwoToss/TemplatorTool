// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 26.04.2021
// =================================================================================================

using System.Collections.Generic;
using Plugins.Templator.Editor.Scripts.Views;
using Plugins.Templator.Editor.Scripts.Views.Base;
using UnityEditor;
using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Core
{
    public class TemplatorEditorWindow : EditorWindow
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
            var mainWindow = (TemplatorEditorWindow) GetWindow(typeof(TemplatorEditorWindow));
            mainWindow.titleContent.text = "Templator";
            mainWindow.titleContent.tooltip = "Game UI Builder for Unity projects";
            mainWindow.Show();
        }

        private static TemplatorCore _core;

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

            _core = (TemplatorCore) AssetDatabase.LoadAssetAtPath(
                pathValidatorSettings,
                typeof(TemplatorCore));
            _core.Initialize();
        }

        private void OnGUI()
        {
            if (_views is null || _core is null)
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
            CreateView<TopSectionView>();
            CreateView<ControlPanelView>();
            //CreateView<TestingView>();
        }

        private void CreateView<T>() where T : BaseView, new()
        {
            _views.Add(new T());
        }
    }
}