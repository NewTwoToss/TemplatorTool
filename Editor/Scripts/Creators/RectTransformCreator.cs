// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 07.05.2021
// =================================================================================================

using Plugins.Templator.Editor.Scripts.ComponentProperties;
using Plugins.Templator.Editor.Scripts.Creators.Contracts;
using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Creators
{
    public class RectTransformCreator : IParentable, IProductable, ICreateable 
    {
        public IPropertiesRectTransform Properties { get; set; }
        
        public RectTransform Parent { get; set; }
        
        public RectTransform Product { get; private set; }
        
        public void CreateUI()
        {
            var go = new GameObject(Properties.Name);
            go.transform.SetParent(Parent);

            var rt = go.AddComponent<RectTransform>();
            rt.localPosition = Vector3.zero;
            rt.localScale = Vector3.one;
            rt.sizeDelta = new Vector2(Properties.Width, Properties.Height);

            Product = rt;
        }
    }
}