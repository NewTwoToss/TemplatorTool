// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 11.05.2021
// =================================================================================================

using Plugins.Templator.Editor.Scripts.ComponentProperties;
using Plugins.Templator.Editor.Scripts.Creators.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.Templator.Editor.Scripts.Creators
{
    public class GridLayoutCreator: IParent, ICreator
    {
        public IPropertiesGridLayout Properties { get; set; }

        public RectTransform Parent { get; set; }

        public void CreateUI()
        {
            var go = Parent.gameObject;

            var gl = go.AddComponent<GridLayoutGroup>();
            gl.spacing = Properties.Spacing;
        }
    }
}