// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 30.04.2021
// =================================================================================================

using System;
using Plugins.GameUIBuilder.Editor.Creators;
using Plugins.GameUIBuilder.Editor.Drawers;
using Plugins.GameUIBuilder.Editor.Drawers.Base;
using Plugins.GameUIBuilder.Editor.Nodes.Base;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Nodes
{
    [Serializable]
    public class SourceNode : BaseNodeComponent
    {
        private readonly SourceDrawer _drawer;
        private readonly SourceCreator _creator;

        public override BaseDrawer Drawer => _drawer;

        public SourceNode(Rect rect, DTestScriptable data) : base(data)
        {
            _drawer = new SourceDrawer(rect, data);
            _creator = new SourceCreator();
        }

        public override void Create()
        {
            _creator.Properties = _drawer;
            _creator.CreateUI();
            var getProduct = _creator.Product;

            CreateDecorators(getProduct);
            CreateGameUINodes(getProduct);
        }

        public void Clear() => nodes.Clear();

        public bool IsPossibleCreateProcess()
        {
            var isSourceNull = _drawer.Source is null;
            var isChildCountZero = nodes.Count == 0;

            return !isSourceNull && !isChildCountZero;
        }

        public override bool CanBeDeleted()
        {
            return false;
        }
    }
}