// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 17.05.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.Scripts.Views.Base;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts.Views
{
    public class DrawTopSection : BaseView
    {
        private bool _initialized;
        private Texture _whiteTexture;
        
        public override void DrawGUI(Rect pRect)
        {
            if (!_initialized)
            {
                _whiteTexture = Resources.Load<Texture>("Textures/white_rect_64");
                _initialized = true;
            }
            
            var topSectionRect = new Rect(0, 0, pRect.width, 24);
            GUI.color = new Color(0.1f, 0.1f, 0.1f, 0.8f);
            GUI.DrawTexture(topSectionRect, _whiteTexture);
            GUI.color = Color.white;
            
            if (GUI.Button(new Rect(0, 1, 100, 22), "File"))
            {
            }
            
            if (GUI.Button(new Rect(102, 1, 120, 22), "Preferences"))
            {
            }
        }
    }
}