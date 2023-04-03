using POE.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE.Common
{



        class Vertex
        {
            public int x, y;//顶点的X,Y坐标
            public Vertex parent = null;//父节点（前驱节点）,默认无父节点
            public int F, G, H; //F = G +H
            public Vertex(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public Vertex()
            {

            }
        }

        class AstarTestClass
        {
            public int heuristicsCostEstimate(Vertex reachableV, Vertex goalV)
            {
                int disX = reachableV.x - goalV.x;
                int disY = reachableV.y - goalV.y;
                return Math.Abs(disX) + Math.Abs(disY);
            }

            public int getG(Vertex v)//当前点与父节点为上下左右的关系，距离为1（不考虑上左，上右，下左，下右）
            {
                return v.parent != null ? v.parent.G + 1 : 0;
            }
            //从openList中查找F值最小的顶点
            public Vertex getVexOfMinFFromOpenList(List<Vertex> openList)
            {
                int min = openList[0].F;
                Vertex vexOfMinF = openList[0];
                for (int i = 1; i < openList.Count(); ++i)
                {
                    if (openList[i].F < min)
                    {
                        min = openList[i].F;
                        vexOfMinF = openList[i];
                    }
                }
                return vexOfMinF;
            }

            public bool isInList(int x, int y, List<Vertex> list)
            {
                foreach (Vertex v in list)
                {
                    if (v.x == x && v.y == y)
                    {
                        return true;
                    }
                }
                return false;
            }


            public Vertex getVertexFromList(int x, int y, List<Vertex> list)
            {
                foreach (Vertex v in list)
                {
                    if (v.x == x && v.y == y)
                    {
                        return v;
                    }
                }
                return new Vertex(0, 0);//上面一定有返回值
            }

            public List<Vertex> AStar(Vertex startVertex, Vertex endVertex, int[,] mapData)
            {
                List<Vertex> openList = new List<Vertex>();
                List<Vertex> closeList = new List<Vertex>();

                List<Vertex> vexs = new List<Vertex>();
                for (int i = 0; i < 1000; ++i)
                {
                    Vertex vex = new Vertex();
                    vexs.Add(vex);
                }

                int vexIndex = 0;
                int[,] direction = new int[4, 2] { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };

                //初始化closeList
                //closeList.Add(startVertex);

                //初始化openList,将起点添加进去
                openList.Add(startVertex);

                while (openList.Count() > 0)
                {//当openList不为空时


                    //遍历openList,查找F值最小的顶点
                    Vertex minVertex = getVexOfMinFFromOpenList(openList);

                    if (minVertex.x == endVertex.x && minVertex.y == endVertex.y)
                    {
                        endVertex.parent = minVertex.parent;
                        break;
                    }

                    //将F值最小的节点移入close表中，并将其作为当前要处理的节点
                    openList.Remove(minVertex);
                    closeList.Add(minVertex);

                    //对当前要处理节点的可到达节点进行检查,
                    for (int i = 0; i < 4; ++i)
                    {
                        int nextX = minVertex.x + direction[i, 0];
                        int nextY = minVertex.y + direction[i, 1];

                        //下一个顶点没有越界
                        if (nextX >= 0 && nextX < mapData.GetLength(0) && nextY >= 0 && nextY < mapData.GetLength(1))
                        {
                            if (mapData[nextX, nextY] == 0 && !isInList(nextX, nextY, closeList))
                            {//不是障碍物并且不在close列表中
                             //判断是否在openList中
                                if (!isInList(nextX, nextY, openList))
                                {//不在openList中，则加入，并将当前处理节点作为它的父节点，并计算其F,G,H
                                    Vertex vex = vexs[vexIndex];
                                    vex.x = nextX;
                                    vex.y = nextY;
                                    vex.parent = minVertex;
                                    vex.G = getG(vex);
                                    vex.H = heuristicsCostEstimate(vex, endVertex);
                                    vex.F = vex.G + vex.H;
                                    openList.Add(vex);
                                    vexIndex++;
                                }
                                else
                                {
                                    //从openList中获取该节点
                                    Vertex vex = getVertexFromList(nextX, nextY, openList);
                                    if (minVertex.G + 1 < vex.G)
                                    {//如果从当前处理顶点到该顶点使得G更小
                                        vex.parent = minVertex;
                                        vex.G = minVertex.G + 1;
                                        vex.F = vex.G + vex.H;
                                    }
                                }
                            }
                        }
                    }
                }

                List<Vertex> theWay = new List<Vertex>();
                Vertex v = endVertex;
                while (v.parent != null)
                {
                    theWay.Add(v);
                    v = v.parent;
                }
                return theWay;
            }
            
            

        }

        class Program
        {

            static void Main222(string[] args)
            {

            // mapData[2][3] = 0表示顶点（2, 3）不是障碍物
            int[,] mapData = {
                { 0,0,0,0,0 },
                { 0,1,0,0,0 },
                { 0,1,1,0,1 },
                { 0,0,0,0,0 },
                { 0,0,0,0,0 },
            };

                Console.WriteLine("{0} {1 }", mapData.Rank, mapData.GetLength(0));

                Vertex start = new Vertex(0, 0);
                Vertex end = new Vertex(4, 4);
                start.F = start.G = start.H = 0;

                AstarTestClass aStarTest = new AstarTestClass();

                List<Vertex> theWay = aStarTest.AStar(start, end, mapData);

                for (int i = theWay.Count() - 1; i >= 0; --i)
                {
                    Console.Write("{0},{1} ->", theWay[i].x, theWay[i].y);
                }
            }
        }
    
}
