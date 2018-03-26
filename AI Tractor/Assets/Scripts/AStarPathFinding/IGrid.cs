using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AStarPathFinding {
    public interface IGrid<T> where T : INode<T> {
        void ClearScore();
        T GetNode(int x, int y);
        bool IsInsideGrid(int x, int y);
    }
}
