// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 17.05.2021
// =================================================================================================

using System;
using System.Collections.Generic;
using Plugins.Templator.Editor.Scripts.Nodes;

namespace Plugins.Templator.Editor.Scripts.Core
{
    [Serializable]
    public class UndoRedoController
    {
        private readonly TemplatorCore _data;
        private readonly Stack<SourceNode> _undoStack;
        private readonly Stack<SourceNode> _redoStack;

        public bool IsUndoStack => _undoStack.Count != 0;
        
        public bool IsRedoStack => _redoStack.Count != 0;

        public UndoRedoController(TemplatorCore data)
        {
            _data = data;
            _undoStack = new Stack<SourceNode>();
            _redoStack = new Stack<SourceNode>();
        }

        public void RegisterSnapshot()
        {
            _redoStack.Clear();
            var clone = _data.SourceNode.GetClone();
            _undoStack.Push(clone);
        }

        public void Undo()
        {
            if (_undoStack.Count == 0) return;
            
            var clone = _data.SourceNode.GetClone();
            _redoStack.Push(clone);
            _data.SourceNode = _undoStack.Pop();
        }

        public void Redo()
        {
            if (_redoStack.Count == 0) return;

            var clone = _data.SourceNode.GetClone();
            _undoStack.Push(clone);
            _data.SourceNode = _redoStack.Pop();
        }

        public void ResetMechanics()
        {
            _undoStack.Clear();
            _redoStack.Clear();
        }
    }
}