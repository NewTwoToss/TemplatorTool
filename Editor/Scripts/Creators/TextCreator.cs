// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 11.05.2021
// =================================================================================================

using Plugins.Templator.Editor.Scripts.ComponentProperties;
using Plugins.Templator.Editor.Scripts.Creators.Contracts;
using TMPro;
using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Creators
{
    public class TextCreator : IParentable, IProductable, ICreateable
    {
        public IPropertiesText Properties { get; set; }

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

            var txt = go.AddComponent<TextMeshProUGUI>();
            txt.text = Properties.Text;

            Product = rt;
        }
    }
}