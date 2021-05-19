// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 30.04.2021
// =================================================================================================

using System;
using System.Collections.Generic;
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

#region [GETTERS]

        public override BaseDrawer Drawer => _drawer;

        public bool IsSourceNull => _drawer.Source is null;

        public bool IsChildCountZero => nodes.Count == 0;

        public bool IsPossibleCreateProcess => !IsSourceNull && !IsChildCountZero;

#endregion

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

        public override bool CanBeDeleted()
        {
            return false;
        }

        public SourceNode GetClone()
        {
            var clone = new SourceNode(_drawer.Rect, data)
            {
                nodes = new List<BaseNodeComponent>(),
                decorators = new List<BaseNodeComponent>()
            };

            foreach (var node in nodes)
            {
                node.MyClone(clone);
            }
            
            foreach (var decorator in decorators)
            {
                decorator.MyClone(clone);
            }

            return clone;
        }
    }
}