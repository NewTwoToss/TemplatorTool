// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 30.04.2021
// =================================================================================================

using System;
using Plugins.GameUIBuilder.Editor.Scripts.Creators;
using Plugins.GameUIBuilder.Editor.Scripts.Drawers;
using Plugins.GameUIBuilder.Editor.Scripts.Drawers.Base;
using Plugins.GameUIBuilder.Editor.Scripts.Nodes.Base;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts.Nodes
{
    [Serializable]
    public class SourceNode : BaseNodeComponent
    {
        private readonly SourceDrawer _drawer;
        private readonly SourceCreator _creator;

        public override BaseDrawer Drawer => _drawer;

        public SourceNode(Rect rect, DTossCreator data) : base(data)
        {
            Level = 0;
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