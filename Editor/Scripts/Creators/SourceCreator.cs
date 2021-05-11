// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 07.05.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.Scripts.ComponentProperties;
using Plugins.GameUIBuilder.Editor.Scripts.Creators.Contracts;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts.Creators
{
    public class SourceCreator : IProductable, ICreateable
    {
        public IPropertiesSource Properties { get; set; }

        public RectTransform Product { get; private set; }

        public void CreateUI()
        {
            Product = Properties.Source;
        }
    }
}