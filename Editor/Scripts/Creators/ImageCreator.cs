// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 10.05.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.Scripts.ComponentProperties;
using Plugins.GameUIBuilder.Editor.Scripts.Creators.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.GameUIBuilder.Editor.Scripts.Creators
{
    public class ImageCreator : IParentable, IProductable, ICreateable
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

            var img = go.AddComponent<Image>();
            img.sprite = Properties.SourceImage;
            img.color = Properties.Color;

            if (Properties.SetNativeSize)
                img.SetNativeSize();

            Product = rt;
        }
    }
}