// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 28.04.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.Scripts.Core;
using Plugins.GameUIBuilder.Editor.Scripts.Nodes;
using Plugins.GameUIBuilder.Editor.Scripts.Nodes.Base;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts
{
    [CreateAssetMenu(fileName = "TossCreatorTool",
        menuName = "TossTool/Create Tool",
        order = 0)]
    public class DTossCreator : ScriptableObject
    {
#region [INSPECTOR]

        [SerializeField]
        private string _version = "0.0.0";
        
        [SerializeField]
        private GUISkin _skin;

        [Space, Header("[DRAW VALUES]"), SerializeField]
        private DrawValues _drawValues;

        [Space, Header("[DEFAULT VALUES]"), SerializeField]
        private DefaultValues _defaultValues;

#endregion

#region [GETTERS / SETTERS]

        public string Version => _version;

        public GUISkin Skin => _skin;

        public int NodeIndex => _nodeIndex++;

        public SourceNode SourceNode { get; set; }

        public BaseNodeComponent SelectedNode { get; set; }

        public BaseNodeComponent CurrentNode { get; set; }

        public bool IsSelection { get; set; }

        public bool IsRepaint { get; set; }

        public DrawValues DrawValues => _drawValues;

        public DefaultValues DefaultValues => _defaultValues;
        
        public UndoRedoController UndoRedo { get; private set; }

#endregion

#region [PRIVATE]

        private int _nodeIndex;

#endregion

        public void Initialize()
        {
            Debug.Log("DATA :: CORE :: Init()");

            _nodeIndex = 0;
            SelectedNode = null;
            CurrentNode = null;
            IsSelection = false;
            IsRepaint = false;

            UndoRedo = new UndoRedoController(this);

            CreateNodes();
        }

        private void CreateNodes()
        {
            SourceNode = new SourceNode(_drawValues.SourceNodeRect, this);

            var sourceNodePosition = _drawValues.SourceNodePosition;
            var nodeWidth = _drawValues.NodeSize.x;
            var nodeHeight = _drawValues.NodeSize.y;
            var nodeShiftHorizontal = _drawValues.NodeShiftHorizontal;
            var nodeShiftVertical = _drawValues.NodeShiftVertical;

            var rect = new Rect(sourceNodePosition.x + nodeShiftHorizontal,
                sourceNodePosition.y + nodeShiftVertical, nodeWidth, nodeHeight);
            var nodeLevel1 = new RectTransformNode(rect, this);
            SourceNode.AddNode(nodeLevel1);

            rect = new Rect(sourceNodePosition.x + 2 * nodeShiftHorizontal,
                sourceNodePosition.y + 2 * nodeShiftVertical, nodeWidth, nodeHeight);
            BaseNodeComponent nodeLevel2 = new TextNode(rect, this);
            nodeLevel1.AddNode(nodeLevel2);

            rect = new Rect(sourceNodePosition.x + 2 * nodeShiftHorizontal,
                sourceNodePosition.y + 3 * nodeShiftVertical, nodeWidth, nodeHeight);
            nodeLevel2 = new ButtonNode(rect, this);
            nodeLevel1.AddNode(nodeLevel2);

            for (var i = 0; i < 10; i++)
            {
                rect = new Rect(sourceNodePosition.x + nodeShiftHorizontal,
                    sourceNodePosition.y + (4 + i) * nodeShiftVertical, nodeWidth, nodeHeight);
                nodeLevel1 = new RectTransformNode(rect, this);
                SourceNode.AddNode(nodeLevel1);
            }
        }

        public void ResetTool()
        {
            UndoRedo.ResetMechanics();
            SourceNode.Clear();
            Initialize();
        }

        public void ResetSelection()
        {
            IsSelection = false;
            SelectedNode = null;
        }
    }
}