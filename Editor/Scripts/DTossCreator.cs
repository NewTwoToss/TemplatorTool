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
        [SerializeField]
        private GUISkin _skin;

        public Vector2 _sourceNodePosition;

        [Range(100, 600)]
        public int _nodeWidth = 200;

        [Range(40, 600)]
        public int _nodeHeight = 60;

        [Range(40, 100)]
        public int _nodeShiftVertical = 80;

        [Range(40, 100)]
        public int _nodeShiftHorizontal = 40;

        [Range(10, 100)]
        public int _decoratorShiftHorizontal = 10;

        [Space]
        [SerializeField]
        private DrawValues _drawValues;

        [Space]
        [SerializeField]
        private DefaultValues _defaultValues;

#region [GETTERS / SETTERS]

        public GUISkin Skin => _skin;

        public int NodeIndex => _nodeIndex++;

        public SourceNode SourceNode { get; private set; }

        public BaseNodeComponent SelectedNode { get; set; }

        public BaseNodeComponent CurrentNode { get; set; }

        public bool IsSelection { get; set; }

        public bool IsRepaint { get; set; }

        public DrawValues DrawValues => _drawValues;

        public DefaultValues DefaultValues => _defaultValues;

#endregion

#region [PRIVATE]

        private int _nodeIndex;

#endregion

        public void Init()
        {
            Debug.Log("DATA :: CORE :: Init()");

            _nodeIndex = 0;
            SelectedNode = null;
            CurrentNode = null;
            IsSelection = false;
            IsRepaint = false;

            CreateNodes();
        }

        private void CreateNodes()
        {
            SourceNode = new SourceNode(_drawValues.SourceNodeRect, this);

            var rect = new Rect(_sourceNodePosition.x + _nodeShiftHorizontal,
                _sourceNodePosition.y + _nodeShiftVertical, _nodeWidth, _nodeHeight);
            var nodeLevel1 = new RectTransformNode(rect, this);
            SourceNode.AddNode(nodeLevel1);

            rect = new Rect(_sourceNodePosition.x + 2 * _nodeShiftHorizontal,
                _sourceNodePosition.y + 2 * _nodeShiftVertical, _nodeWidth, _nodeHeight);
            BaseNodeComponent nodeLevel2 = new TextNode(rect, this);
            nodeLevel1.AddNode(nodeLevel2);

            rect = new Rect(_sourceNodePosition.x + 2 * _nodeShiftHorizontal,
                _sourceNodePosition.y + 3 * _nodeShiftVertical, _nodeWidth, _nodeHeight);
            nodeLevel2 = new ButtonNode(rect, this);
            nodeLevel1.AddNode(nodeLevel2);

            for (var i = 0; i < 3; i++)
            {
                rect = new Rect(_sourceNodePosition.x + _nodeShiftHorizontal,
                    _sourceNodePosition.y + (4 + i) * _nodeShiftVertical, _nodeWidth, _nodeHeight);
                nodeLevel1 = new RectTransformNode(rect, this);
                SourceNode.AddNode(nodeLevel1);
            }
        }

        public void ResetTool()
        {
            SourceNode.Clear();
            Init();
        }

        public void ResetSelection()
        {
            IsSelection = false;
            SelectedNode = null;
        }
    }
}