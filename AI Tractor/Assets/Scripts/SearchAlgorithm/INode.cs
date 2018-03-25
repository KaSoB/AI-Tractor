using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithm {
    public interface INode<T> : IComparable<T>, IEquatable<T> {
        int X { get; }
        int Y { get; }
        T Parent { get; set; }
        int G_Score { get; set; }
        int H_Score { get; set; }
        int F_Score { get; }
        bool Walkable { get; }
        bool IsCorrectPath { get; set; }
        void ClearScore();
    }
}
