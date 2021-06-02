// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 07.05.2021
// =================================================================================================

using Plugins.Templator.Editor.Scripts.ComponentProperties;
using Plugins.Templator.Editor.Scripts.Creators.Contracts;
using Plugins.Templator.Editor.Scripts.Utilities;
using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Creators
{
    public class RectTransformCreator : IParent, IProduct, ICreator 
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
            
            var indexAnchor = Properties.IndexAnchor;
            rt.anchorMin = CreatorUtilities.GetAnchorMinByIndex(indexAnchor);
            rt.anchorMax = CreatorUtilities.GetAnchorMaxByIndex(indexAnchor);
            rt.pivot = CreatorUtilities.GetPivotByIndex(Properties.IndexPivot);

            Product = rt;
        }
    }
}