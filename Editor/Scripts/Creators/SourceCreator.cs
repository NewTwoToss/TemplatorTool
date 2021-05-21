// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 07.05.2021
// =================================================================================================

using Plugins.Templator.Editor.Scripts.ComponentProperties;
using Plugins.Templator.Editor.Scripts.Creators.Contracts;
using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Creators
{
    public class SourceCreator : IProduct, ICreator
    {
        public IPropertiesSource Properties { get; set; }

        public RectTransform Product { get; private set; }

        public void CreateUI()
        {
            Product = Properties.Source;
        }
    }
}