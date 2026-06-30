using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    internal class AlgorythmDancingLinks
    {
        private class Node
        {
            public Node Up { get; set; }
            public Node Down { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Row { get; set; }
            public ColumnRestrictionHeader? ColumnHeader { get; set; }

            public Node()
            {
                Up = Down = Left = Right = this;
            }

            public Node(ColumnRestrictionHeader colRstrHdr, int row) : this()
            {
                ColumnHeader = colRstrHdr;
                Row = row;
            }

            public virtual void PrintDebugInfo()
            {
                Console.WriteLine($"[Node] Строка матрицы: {Row}");
            }
        }

        private class ColumnRestrictionHeader : Node
        {
            public int Size { get; set; }
            public int ConstraintIndex { get; set; }

            public ColumnRestrictionHeader(int constrInd) : base()
            {
                Size = 0;
                ConstraintIndex = constrInd;
                ColumnHeader = this;
            }

            public override void PrintDebugInfo()
            {
                Console.WriteLine($"[Заголовок Колонки] Индекс ограничения: {ConstraintIndex}, Количество кандидатов (Size): {Size}");
            }
        }

        private ColumnRestrictionHeader columnHeaderSlider;
        private List<Node> solutionList = new List<Node>();
        public List<int[]> Solutions { get; private set; } = new List<int[]>();
        public int GetCountSolutions() => Solutions.Count;

        public AlgorythmDancingLinks(int[,] matrix, int constraintCount, int[]? knownNumberRestr = null)
        {
            columnHeaderSlider = new ColumnRestrictionHeader(-1);
            ColumnRestrictionHeader[] zipper = new ColumnRestrictionHeader[constraintCount];
            Node prevNode = columnHeaderSlider;

            for (int i = 0; i < constraintCount; i++)
            {
                ColumnRestrictionHeader zipperTooth = new ColumnRestrictionHeader(i);
                zipper[i] = zipperTooth;
                prevNode.Right = zipperTooth;
                zipperTooth.Left = prevNode;
                prevNode = zipperTooth;
            }
            prevNode.Right = columnHeaderSlider;
            columnHeaderSlider.Left = prevNode;

            for (int rowInd = 0; rowInd < matrix.GetLength(0); rowInd++)
            {
                Node? rowGrip = null;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[rowInd, j] == 1)
                    {
                        ColumnRestrictionHeader colTooth = zipper[j];
                        Node currentTooth = new Node(colTooth, rowInd);

                        currentTooth.Up = colTooth.Up;
                        currentTooth.Down = colTooth;
                        colTooth.Up.Down = currentTooth;
                        colTooth.Up = currentTooth;

                        colTooth.Size = colTooth.Size + 1;

                        if (rowGrip == null)
                        {
                            rowGrip = currentTooth;
                            currentTooth.Right = currentTooth.Left = currentTooth;
                        }
                        else
                        {
                            currentTooth.Left = rowGrip.Left;
                            currentTooth.Right = rowGrip;
                            rowGrip.Left.Right = currentTooth;
                            rowGrip.Left = currentTooth;
                        }
                    }
                }
            }

            if (knownNumberRestr != null && knownNumberRestr.Length > 0)
            {
                for (int i = 0; i < knownNumberRestr.Length; i++)
                {
                    ExcludeKnownRestrictions(zipper[i]);
                }
            }
        }

        public void SearchSolution(int maxSolCount = 2, int k = 0)
        {
            if (columnHeaderSlider.Left == columnHeaderSlider)
            {
                Solutions.Add(GetSolutionIndicies());
                return;
            }

            if (Solutions.Count >= maxSolCount)
                return;

            ColumnRestrictionHeader tooth = ChoooseColumn();
            Cover(tooth);

            for (Node colNode = tooth.Down; colNode != tooth && Solutions.Count < maxSolCount; colNode = colNode.Down)
            {
                solutionList.Add(colNode);
                for (Node rowNode = colNode.Right; rowNode.Right != colNode; rowNode = rowNode.Right)
                {
                    if (rowNode.ColumnHeader != null)
                    {
                        Cover(rowNode.ColumnHeader);
                    }
                }

                SearchSolution(maxSolCount, k + 1);

                colNode = solutionList[solutionList.Count - 1];
                solutionList.RemoveAt(solutionList.Count - 1);
                for (Node n = colNode.Left; n != colNode; n = n.Left)
                {
                    if (n.ColumnHeader != null)
                    {
                        Uncover(n.ColumnHeader);
                    }
                }
            }
            Uncover(tooth);
        }

        private int[] GetSolutionIndicies()
        {
            return solutionList.Select(node => node.Row).ToArray();
        }

        private ColumnRestrictionHeader ChoooseColumn()
        {
            ColumnRestrictionHeader col = (ColumnRestrictionHeader)columnHeaderSlider.Right;
            int minCount = col.Size;
            for (ColumnRestrictionHeader r = col; r != columnHeaderSlider; r = (ColumnRestrictionHeader)r.Right)
            {
                if (minCount > r.Size)
                {
                    minCount = r.Size;
                    col = r;
                }
            }
            return col;
        }

        private void Cover(ColumnRestrictionHeader currentTooth)
        {
            currentTooth.Right.Left = currentTooth.Left;
            currentTooth.Left.Right = currentTooth.Right;

            for (Node tCol = currentTooth.Down; tCol != currentTooth; tCol = tCol.Down)
            {
                for (Node tRow = tCol.Right; tRow != tCol; tRow = tRow.Right)
                {
                    tRow.Down.Up = tRow.Up;
                    tRow.Up.Down = tRow.Down;
                    if (tRow.ColumnHeader != null)
                    {
                        tRow.ColumnHeader.Size = tRow.ColumnHeader.Size - 1;
                    }
                }
            }
        }

        private void ExcludeKnownRestrictions(ColumnRestrictionHeader tooth)
        {
            Cover(tooth);

            for (Node colNode = tooth.Down; colNode != tooth; colNode = colNode.Down)
            {
                for (Node rowNode = colNode.Right; rowNode.Right != colNode; rowNode = rowNode.Right)
                {
                    if (rowNode.ColumnHeader != null)
                    {
                        Cover(rowNode.ColumnHeader);
                    }
                }
            }
        }

        private void Uncover(ColumnRestrictionHeader currentTooth)
        {
            for (Node i = currentTooth.Up; i != currentTooth; i = i.Up)
            {
                for (Node j = i.Left; j != i; j = j.Left)
                {
                    if (j.ColumnHeader != null)
                    {
                        j.ColumnHeader.Size = j.ColumnHeader.Size + 1;
                    }
                    j.Down.Up = j;
                    j.Up.Down = j;
                }
            }
            currentTooth.Right.Left = currentTooth;
            currentTooth.Left.Right = currentTooth;
        }
    }
}