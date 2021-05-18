// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 11.05.2021
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
    public class TextNode : BaseNodeComponent
    {
        private TextDrawer _drawer;
        private readonly TextCreator _creator;

        public override BaseDrawer Drawer => _drawer;

        public TextNode(Rect rect, DTossCreator data) : base(data)
        {
            _drawer = new TextDrawer(rect, data);
            _creator = new TextCreator();
        }

        public override void SetParent(RectTransform parent)
        {
            _creator.Parent = parent;
        }

        public override void Create()
        {
            _creator.Properties = _drawer;
            _creator.CreateUI();
            var getProduct = _creator.Product;

            CreateDecorators(getProduct);
            CreateGameUINodes(getProduct);
        }
        
        private void DrawerForClone(TextDrawer drawer) => _drawer = drawer;
        
        public override void MyClone(BaseNodeComponent cloneParent)
        {
            var cloneNode = new TextNode(GetCloneRect(), data);
            cloneNode.DrawerForClone(_drawer);

            cloneParent.nodes.Add(cloneNode);

            if (nodes.Count == 0) return;

            foreach (var node in nodes)
            {
                node.MyClone(cloneNode);
            }
        }
    }
}