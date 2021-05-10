// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 07.05.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.ComponentProperties;
using Plugins.GameUIBuilder.Editor.Creators.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.GameUIBuilder.Editor.Creators
{
    public class VerticalLayoutCreator : IParentable, ICreateable
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