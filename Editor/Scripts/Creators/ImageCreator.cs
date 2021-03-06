// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 10.05.2021
// =================================================================================================

using Plugins.Templator.Editor.Scripts.ComponentProperties;
using Plugins.Templator.Editor.Scripts.Creators.Contracts;
using Plugins.Templator.Editor.Scripts.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.Templator.Editor.Scripts.Creators
{
    public class ImageCreator : IParent, IProduct, ICreator
    {
        public IPropertiesImage Properties { get; set; }

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

            var img = go.AddComponent<Image>();
            img.sprite = Properties.SourceImage;
            img.color = Properties.Color;
            img.raycastTarget = Properties.RaycastTarget;
            img.maskable = Properties.Maskable;

            if (Properties.SetNativeSize)
            {
                img.SetNativeSize();
            }

            Product = rt;
        }
    }
}