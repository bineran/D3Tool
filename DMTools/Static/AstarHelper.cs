using System;
using System.Collections.Generic;
using System.Text;

namespace Static
{
    /// <summary>
    /// A星算法
    /// </summary>
    public static class AstarHelper
    {

        public class Vertex
        {
            public int X { get; set; }
            public int Y { get; set; } //顶点的X,Y坐标
            public Vertex Parent { get; set; }//父节点（前驱节点）,默认无父节点

            public float F, G, H; //F = G +H

            public Vertex(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
            public Vertex()
            { }
        }

        //只有四方向就只要四个.
        //第一个是X坐标,第二个是Y坐标加,第三个值是G的变量,直线走算10,斜的算15
        //理论了上也可以跳着走,只要改变X和Y的增量就可以.
        static readonly int[,] direction = new int[8, 3] {
                    //直线走
                    { 0, 1,10 },
                    { 1, 0,10 },
                    { 0, -1,10 },
                    { -1, 0 ,10},
                    //斜着走
                    { 1,1,15},
                    { -1, -1,15 },
                    {1,-1 ,15},
                    { -1,1,15}
                };
        /// <summary>
        /// DEMO 测试方法 从0,0-->9,9
        /// </summary>
        /// <returns></returns>
        public static string Test()
        {
            var mapPoint = new int[,] {
                    { 0,1,1,1,1,1,1,1,1,0},
                    { 0,0,1,0,0,0,0,1,0,0},
                    { 0,0,1,0,1,1,0,1,0,0},
                    { 0,0,1,0,1,0,0,1,0,0},
                    { 0,0,0,0,1,0,1,1,0,0},
                    { 0,0,1,1,1,0,0,0,0,0},
                    { 0,0,0,0,1,0,1,1,0,0},
                    { 0,0,1,0,0,0,1,1,0,0},
                    { 0,0,1,0,1,0,1,1,0,0},
                    { 0,1,1,1,1,1,1,1,1,0}
                };
            var al = AStar(0, 0, 9, 9, mapPoint);
            //下面是打印
            string str = "原始的\r\n";
            for (int i = 0; i < mapPoint.GetLength(0); i++)
            {
                for (int j = 0; j < mapPoint.GetLength(1); j++)
                {
                    if (mapPoint[i, j] == 1)
                    {
                        str += "1";
                    }
                    else
                    {
                        str += "0";
                    }
                }
                str += "\r\n";
            }
            str += "计算后\r\n";
            for (int i = 0; i < mapPoint.GetLength(0); i++)
            {
                for (int j = 0; j < mapPoint.GetLength(1); j++)
                {
                    if (mapPoint[i, j] == 1)
                    {
                        str += "1";
                    }
                    else
                    {
                        if (al.Any(r => r.X == i && r.Y == j))
                        {
                            str += "*";
                        }
                        else
                        {
                            str += "0";
                        }
                    }
                }
                str += "\r\n";
            }
            return str;
        }

        public static List<Vertex> AStar(Vertex startVertex, Vertex endVertex, int[,] mapData)
        {
            List<Vertex> openList = new List<Vertex>();
            List<Vertex> closeList = new List<Vertex>();
            //初始化closeList
            //closeList.Add(startVertex);

            //初始化openList,将起点添加进去
            openList.Add(startVertex);
            var aLength = mapData.GetLength(0);
            var bLength = mapData.GetLength(1);
            //方向数
            int directionCount = direction.GetLength(0);
            while (openList.Count > 0)
            {//当openList不为空时
             //遍历openList,查找F值最小的顶点
                Vertex minVertex = openList.OrderBy(r => r.F).First();

                if (minVertex.X == endVertex.X && minVertex.Y == endVertex.Y)
                {
                    endVertex.Parent = minVertex.Parent;
                    break;
                }

                //将F值最小的节点移入close表中，并将其作为当前要处理的节点
                openList.Remove(minVertex);
                closeList.Add(minVertex);

                //对当前要处理节点的可到达节点进行检查,

                for (int i = 0; i < directionCount; ++i)
                {
                    int nextX = minVertex.X + direction[i, 0];
                    int nextY = minVertex.Y + direction[i, 1];

                    //下一个顶点没有越界
                    if (nextX >= 0 && nextX < aLength && nextY >= 0 && nextY < bLength)
                    {
                        if (mapData[nextX, nextY] == 0 && !closeList.Any(r => r.X == nextX && r.Y == nextY))
                        {
                            //判断是否在openList中
                            if (!openList.Any(r => r.X == nextX && r.Y == nextY))
                            {//不在openList中，则加入，并将当前处理节点作为它的父节点，并计算其F,G,H
                                Vertex vex = new Vertex();
                                vex.X = nextX;
                                vex.Y = nextY;
                                vex.Parent = minVertex;
                                vex.G = vex.Parent.G + direction[i, 2];
                                vex.H = Math.Abs(vex.X - endVertex.X) + Math.Abs(vex.Y - endVertex.Y);
                                vex.F = vex.G + vex.H;
                                openList.Add(vex);
                            }
                            else
                            {
                                //从openList中获取该节点
                                Vertex vex = openList.First(r => r.X == nextX && r.Y == nextY);
                                if (minVertex.G + direction[i, 2] < vex.G)
                                {//如果从当前处理顶点到该顶点使得G更小
                                    vex.Parent = minVertex;
                                    vex.G = minVertex.G + direction[i, 2];
                                    vex.F = vex.G + vex.H;
                                }
                            }
                        }
                    }
                }
            }
            List<Vertex> theWay = new List<Vertex>();
            Vertex v = endVertex;
            while (v.Parent != null)
            {
                theWay.Add(v);
                v = v.Parent;
            }
            return theWay;
        }
        public static List<Vertex> AStar((int x, int y) startVertex, (int x, int y) endVertex, int[,] mapData)
        {
            return AStar(new Vertex(startVertex.x, startVertex.y), new Vertex(endVertex.x, endVertex.y), mapData);
        }
        public static List<Vertex> AStar(int x1, int y1, int x2, int y2, int[,] mapData)
        {
            return AStar(new Vertex(x1, y1), new Vertex(x2, y2), mapData);
        }
    }

}
