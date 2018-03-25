using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithm {
    public interface IGrid<T> where T : INode<T> {
        void InitGrid();
        T GetNode(int x, int y);
        bool IsInsideGrid(int x, int y);
    }
}
