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
        private const string CORE_FILE_NAME = "TemplatorCore01";
        
        private static DTemplatorCore _core;
        private List<BaseView> _views;

        private void OnEnable()
        {
            Debug.Log("[TemplatorCore] OnEnable()");
            InitializeTool();
        }

        [InitializeOnLoadMethod]
        public static void OnProjectLoadedInEditor()
        {
            Debug.Log("[TemplatorCore] OnProjectLoadedInEditor()");
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
            mainWindow.titleContent.text = "Templator Editor";
            mainWindow.titleContent.tooltip = "Simple UI Builder for Unity projects";
            mainWindow.Show();
        }

        private static void InitializeTool()
        {
            if (!(_core is null)) return;

            Debug.Log("[TOOL] InitializeTool()");
            Initialize();
        }

        private static void Initialize()
        {
            var guidValidatorSettings = AssetDatabase.FindAssets(CORE_FILE_NAME);

            if (guidValidatorSettings.Length == 0) return;

            var pathValidatorSettings = AssetDatabase.GUIDToAssetPath(guidValidatorSettings[0]);

            _core = (DTemplatorCore) AssetDatabase.LoadAssetAtPath(pathValidatorSettings,
                typeof(DTemplatorCore));
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
        }

        private void InitializeViews()
        {
            foreach (var view in _views)
            {
                view.Core = _core;
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