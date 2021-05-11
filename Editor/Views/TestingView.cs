// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 10.05.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.Views.Base;
using UnityEditor;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Views
{
    public class TestingView : BaseView
    {
        public Vector2 scrollPosition = Vector2.zero;
        
        public override void DrawGUI(Rect pRect)
        {
            scrollPosition = GUI.BeginScrollView(new Rect(0, 0, pRect.width, pRect.height), 
                scrollPosition, 
                new Rect(0, 0, 400, 300));

            // Make four buttons - one in each corner. The coordinate system is defined
            // by the last parameter to BeginScrollView.
            GUI.Button(new Rect(30, 30, 100, 20), "Top-left");
            GUI.Button(new Rect(120, 0, 100, 20), "Top-right");
            GUI.Button(new Rect(0, 280, 100, 20), "Bottom-left");
            GUI.Button(new Rect(120, 180, 100, 20), "Bottom-right");

            // End the scroll view that we began above.
            GUI.EndScrollView();
        }
    }
}