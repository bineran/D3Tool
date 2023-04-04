using System.Diagnostics;
using System.Resources;
using System.Windows.Forms;
using static MapTest.Form1;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace MapTest
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }


        int defaultPointXY = -99999;
        int[,] mapPoint;
        int RowCount = 10;
        int ColumnCount = 10;
        private int random()
        {
            return 0;
            Task.Delay(2).Wait();
            var seed = Guid.NewGuid().GetHashCode();
            Random r = new Random(seed);
            return r.Next(0, 10) > 5 ? 0 : 1;
        }
        public void InitPoint()
        {

            //mapPoint = new int[RowCount, ColumnCount];
            //for (int i = 0; i < ColumnCount; i++)
            //{

            //    long tick = DateTime.Now.Ticks;

            //    Task.Delay(2).Wait();
            //    mapPoint[0, i] = random();
            //}
            //for (int i = 1; i < RowCount; i++)
            //{
            //    bool isbreak = false;
            //    while (!isbreak)
            //    {
            //        for (int j = 0; j < ColumnCount; j++)
            //        {
            //            mapPoint[i, j] = random();
            //        }

            //        for (int j = 0; j < ColumnCount; j++)
            //        {
            //            if (mapPoint[i - 1, j] == 0 && mapPoint[i, j] == 0)
            //            {
            //                isbreak = true;
            //                break;
            //            }

            //        }
            //    }
            //}

            mapPoint = new int[,] {
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

        }

        public void SetPoint(int x, int y, Button btn)
        {
            if (endPoint.x != defaultPointXY && beginPoint.x != defaultPointXY)
            {
                RestButtonColor();
                beginPoint.btn.BackColor = Color.White;
                endPoint.btn.BackColor = Color.White;
                beginPoint = new Vertex();
                endPoint = new Vertex();
            }
            if (beginPoint.x == defaultPointXY)
            {
                beginPoint = new Vertex();
                beginPoint.x = x;
                beginPoint.y = y;


                btn.BackColor = Color.Green;
                beginPoint.btn = btn;
            }
            else if (endPoint.x == defaultPointXY && !beginPoint.btn.Equals(btn))
            {
                endPoint.x = x;
                endPoint.y = y;

                btn.BackColor = Color.Red;
                endPoint.btn = btn;
                Start();
            }
        }



        public void DrawMap()
        {
            this.tableLayoutPanel1.ColumnCount = ColumnCount;
            this.tableLayoutPanel1.RowCount = RowCount;
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();
            Parallel.For(0, RowCount, i =>
            {
                this.Invoke(() =>
                {
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                });

            });
            Parallel.For(0, ColumnCount, i =>
            {
                this.Invoke(() =>
                {
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                });
            });

            Parallel.For(0, tableLayoutPanel1.RowCount, i =>
            {
                int r = i;
                Parallel.For(0, tableLayoutPanel1.ColumnCount, j =>
                {
                    int c = j;
                    var btn = new Button();



                    if (this.mapPoint[r, c] == 0)
                    {
                        btn.Text = $"{r},{c}";
                        btn.BackColor = Color.White;
                        btn.Click += (sender, e) =>
                        {
                            var btn = sender as Button;
                            if (btn != null)
                            {
                                var strs = btn.Text.Split(",");
                                int x = Convert.ToInt32(strs[0]);
                                int y = Convert.ToInt32(strs[1]);
                                SetPoint(x, y, btn);
                            }

                        };
                    }
                    else
                    {
                        btn.BackColor = Color.Black;
                    }

                    btn.Dock = DockStyle.Fill;

                    this.Invoke(new Action(() =>
                    {
                        btn.Name = $"btn{r}{c}";
                        tableLayoutPanel1.Controls.Add(btn, c, r);
                    }));


                });
            });

        }
        Vertex beginPoint = new Vertex();
        Vertex endPoint = new Vertex();
        private void Btn_Click(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.InitPoint();
            this.DrawMap();
           // LoadPic();
           
            

        }
        Vertex p1 = new Vertex();
        Vertex p2 = new Vertex();
        Ostu ostu = new Ostu(MapTest.Resource.map);
        public void LoadPic()
        {
            PictureBox pic = new PictureBox();
            pic.Image = MapTest.Resource.map;
            pic.Size=new Size(pic.Image.Width, pic.Image.Height);
            this.tableLayoutPanel1.Controls.Add(pic);


           
            ostu.RetrunPoint();

            
            PictureBox pic2 = new PictureBox();
            pic2.Image = MapTest.Resource.map;
            pic2.Size = new Size(pic2.Image.Width, pic2.Image.Height);
            this.tableLayoutPanel1.Controls.Add(pic2);
            pic2.MouseClick += Pic2_MouseClick;
     

        }

        private void Pic2_MouseClick(object? sender, MouseEventArgs e)
        {
            var x = e.Location.X;
            var y = e.Location.Y;
            if (p1.x != defaultPointXY && p2.x != defaultPointXY)
            {

                p1 = new Vertex();
                p2 = new Vertex();
            }
            if (p1.x == defaultPointXY)
            {
                p1 = new Vertex();
                p1.x = x;
                p1.y = y;


            }
            else if (p2.x == defaultPointXY && !(p1.x == x && p1.y == y))
            {
                p2.x = x;
                p2.y = y;

                AstarHelper a = new AstarHelper();
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                var vertices = a.AStar(p1, p2, ostu.point);
                stopwatch.Stop();
                this.Text = stopwatch.ElapsedMilliseconds + "毫秒";

            }
        }



        public void Start()
        {
            if (beginPoint.x >= 0 && endPoint.x >= 0)
            {
                AstarHelper a = new AstarHelper();
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                var vertices = a.AStar(beginPoint, endPoint, mapPoint);
                stopwatch.Stop();
                this.Text = stopwatch.ElapsedMilliseconds + "毫秒";
                SetVertexButton(vertices);
            }
        }

        public void RestButtonColor()
        {
            foreach (var sender in tableLayoutPanel1.Controls)
            {
                var btn = sender as Button;
                if (btn != null && btn.Text.Length > 0)
                {
                    btn.BackColor = Color.White;

                }
            }

        }
        public void SetVertexButton(List<Vertex> vertices)
        {

            RestButtonColor();
            foreach (var sender in tableLayoutPanel1.Controls)
            {
                var btn = sender as Button;
                if (btn != null && btn.Text.Length > 0)
                {
                    if (vertices.Exists(r => r.x + "," + r.y == btn.Text))
                    {
                        btn.BackColor = Color.Blue;
                    }


                }
            }
            beginPoint.btn.BackColor = Color.Green;
            endPoint.btn.BackColor = Color.Red;
        }


        public class Vertex
        {
            public int x { get; set; } = -99999;
            public int y { get; set; } = -99999;//顶点的X,Y坐标
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
            public Button btn;
        }
        public class AstarHelper
        {
            //只有四方向就只要四个.
           readonly  int[,] direction = new int[8, 2] {
                    { 0, 1 },
                    { 1, 0 },
                    { 0, -1 },
                    { -1, 0 },

                    { 1,1},
                    { -1, -1 },
                    {1,-1 },
                    { -1,1}
                };
            public int heuristicsCostEstimate(Vertex reachableV, Vertex goalV)
            {
                int disX = reachableV.x - goalV.x;
                int disY = reachableV.y - goalV.y;
                return Math.Abs(disX) + Math.Abs(disY);
            }



            public List<Vertex> AStar(Vertex startVertex, Vertex endVertex, int[,] mapData)
            {
                List<Vertex> openList = new List<Vertex>();
                List<Vertex> closeList = new List<Vertex>();

                //List<Vertex> vexs = new List<Vertex>();
                //for (int i = 0; i < 2000; ++i)
                //{
                //    Vertex vex = new Vertex();
                //    vexs.Add(vex);
                //}



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

                    if (minVertex.x == endVertex.x && minVertex.y == endVertex.y)
                    {
                        endVertex.parent = minVertex.parent;
                        break;
                    }

                    //将F值最小的节点移入close表中，并将其作为当前要处理的节点
                    openList.Remove(minVertex);
                    closeList.Add(minVertex);

                    //对当前要处理节点的可到达节点进行检查,

                    for (int i = 0; i < directionCount; ++i)
                    {
                        int nextX = minVertex.x + direction[i, 0];
                        int nextY = minVertex.y + direction[i, 1];

                        //下一个顶点没有越界
                        if (nextX >= 0 && nextX < aLength && nextY >= 0 && nextY < bLength)
                        {
                            if (mapData[nextX, nextY] == 0 && !closeList.Any(r => r.x == nextX && r.y == nextY))
                            {
                                //判断是否在openList中
                                if (!openList.Any(r => r.x == nextX && r.y == nextY))
                                {//不在openList中，则加入，并将当前处理节点作为它的父节点，并计算其F,G,H
                                    Vertex vex = new Vertex();
                                    vex.x = nextX;
                                    vex.y = nextY;
                                    vex.parent = minVertex;
                                    vex.G = vex.parent.G + 1;
                                    vex.H = heuristicsCostEstimate(vex, endVertex);
                                    vex.F = vex.G + vex.H;
                                    openList.Add(vex);
                                }
                                else
                                {
                                    //从openList中获取该节点
                                    Vertex vex = openList.First(r => r.x == nextX && r.y == nextY);
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




    }
}