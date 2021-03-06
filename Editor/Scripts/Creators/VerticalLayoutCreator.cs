// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 07.05.2021
// =================================================================================================

using Plugins.Templator.Editor.Scripts.ComponentProperties;
using Plugins.Templator.Editor.Scripts.Creators.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.Templator.Editor.Scripts.Creators
{
    public class VerticalLayoutCreator : IParent, ICreator
    {
        public IPropertiesVerticalLayout Properties { get; set; }

        public RectTransform Parent { get; set; }

        public void CreateUI()
        {
            var go = Parent.gameObject;

            var vl = go.AddComponent<VerticalLayoutGroup>();
            vl.spacing = Properties.Spacing;
        }
    }
}