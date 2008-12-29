﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickGraph
{
    [Serializable]
    public class UndirectedEdgeEventArgs<TVertex, TEdge>
        : EdgeEventArgs<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly bool reversed;

        public UndirectedEdgeEventArgs(TEdge edge, bool reversed)
            :base(edge)
        {
            this.reversed = reversed;
        }

        public bool Reversed
        {
            get { return this.reversed; }
        }

        public TVertex Source
        {
            get { return this.reversed ? this.Edge.Target : this.Edge.Source; }
        }

        public TVertex Target
        {
            get { return this.reversed ? this.Edge.Source : this.Edge.Target; }
        }
    }

    public delegate void UndirectedEdgeEventHandler<TVertex, TEdge>(
        Object sender,
        UndirectedEdgeEventArgs<TVertex, TEdge> e)
        where TEdge : IEdge<TVertex>;
}