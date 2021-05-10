// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 07.05.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.ComponentProperties;
using Plugins.GameUIBuilder.Editor.Creators.Contracts;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Creators
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