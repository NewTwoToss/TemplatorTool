// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 26.04.2021
// =================================================================================================

using System.Collections.Generic;
using Plugins.Templator.Editor.Scripts.Nodes;
using Plugins.Templator.Editor.Scripts.Views;
using Plugins.Templator.Editor.Scripts.Views.Base;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Plugins.Templator.Editor.Scripts.Core
{
    public class TemplatorEditorWindow : EditorWindow
    {
        private const string CORE_FILE_NAME = "TemplatorCore01";

        private static DTemplatorCore _core;
        private List<BaseView> _views;

        private void OnEnable()
        {
            Debug.Log(_core is null
                ? "[Templator] OnEnable() :: _core = NULL"
                : $"[Templator] OnEnable() :: {_core.name}");

            EditorSceneManager.sceneClosing += EditorSceneManagerOnsceneClosing;

            InitializeTool();
        }

        private void EditorSceneManagerOnsceneClosing(Scene scene, bool removingscene)
        {
            _core.SourceNode.SetParentReferenceToNull();
        }

        private void OnDisable()
        {
            Debug.Log("[Templator] OnDisable()");
            _core!.DisableEditor();
        }

        [InitializeOnLoadMethod]
        public static void OnProjectLoadedInEditor()
        {
            Debug.Log(_core is null
                ? "[Templator] OnProjectLoadedInEditor() :: _core = NULL"
                : $"[Templator] OnProjectLoadedInEditor() :: {_core.name}");

            InitializeTool();
        }

        [MenuItem("Tools/Templator")]
        private static void UnityMenuGameUIBuilder()
        {
            InitializeTool();
            //_core.Initialize();
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

            Debug.Log("[Templator] InitializeTool() :: _core = NULL!");
            InitializeCore();
        }

        private static void InitializeCore()
        {
            var guidValidatorSettings = AssetDatabase.FindAssets(CORE_FILE_NAME);

            if (guidValidatorSettings.Length == 0) return;

            var pathValidatorSettings = AssetDatabase.GUIDToAssetPath(guidValidatorSettings[0]);

            _core = (DTemplatorCore) AssetDatabase.LoadAssetAtPath(pathValidatorSettings,
                typeof(DTemplatorCore));

            if (!(_core is null))
            {
                Debug.Log($"[Templator] InitializeCore() :: {_core.name}");
            }
        }

        private void OnGUI()
        {
            if (_core is null) return;

            if (_views is null)
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
            CreateView<MainView>();
            CreateView<InspectorView>();
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