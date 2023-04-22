using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Point = System.Drawing.Point;

namespace DMTools
{
    public  class MyTabControl : TabControl
    {
        public MyTabControl()
        {
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
            SetStyle(
                    ControlStyles.ResizeRedraw |
      ControlStyles.AllPaintingInWmPaint |  //全部在窗口绘制消息中绘图
      ControlStyles.OptimizedDoubleBuffer, true //使用双缓冲
   );
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            TabPage tabPage = GetTabPageByTab(new Point(e.X, e.Y));
            if (tabPage != null)
            {
                this.DoDragDrop(tabPage, DragDropEffects.All);
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
           // e.DrawBackground();
            try
            {
                var  backgroundColor = Brushes.GreenYellow;
                var fontColor = Brushes.Black;
                if (this.SelectedIndex == e.Index)
                {
                    backgroundColor = Brushes.BlueViolet;
                }
                var uf = this.TabPages[e.Index].Controls[0] as UserFun;
                if (uf != null)
                {
                    if (!uf.d3ConfigItem.EnabledFlag)
                    {
                        backgroundColor = Brushes.White;
                        if (this.SelectedIndex == e.Index)
                        {
                            backgroundColor = Brushes.BlueViolet;
                        }
                        fontColor = Brushes.DarkGray;
                    }
                }
              
               
                    e.Graphics.FillRectangle(backgroundColor, e.Bounds);
                    SizeF sz = e.Graphics.MeasureString(this.TabPages[e.Index].Text, e.Font);
                    e.Graphics.DrawString(this.TabPages[e.Index].Text, e.Font,
                        fontColor,
                        e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2,
                        e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);
                    //Rectangle rect = e.Bounds;
                    //rect.Offset(0, -1);
                    //rect.Inflate(0, -1);
                    //e.Graphics.DrawRectangle(Pens.DarkGray, rect);
                    //e.DrawFocusRectangle();
                
            }
            catch
            { }
        }
        private TabPage GetTabPageByTab(Point point)
        {
            for (int i = 0; i < this.TabPages.Count; i++)
            {
                if (GetTabRect(i).Contains(point))
                {
                    return this.TabPages[i];
                }
            }
            return null;
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);
            TabPage source = (TabPage)e.Data.GetData(typeof(TabPage));
            if (source != null)
            {
                TabPage target = GetTabPageByTab(PointToClient(new Point(e.X, e.Y)));
                if (target != null)
                {
                    e.Effect = DragDropEffects.Move;
                    if (this.TabPages[this.TabPages.Count - 1] == target || this.TabPages[this.TabPages.Count - 1] == source)
                    {
                        return;
                    }
                    MoveTabPage(source, target);
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void MoveTabPage(TabPage source, TabPage target)
        {
            if (source == target)
                return;

            int targetIndex = -1;
            List<TabPage> lstPages = new List<TabPage>();
            for (int i = 0; i < this.TabPages.Count; i++)
            {
                if (this.TabPages[i] == target)
                {
                    targetIndex = i;
                }
                if (this.TabPages[i] != source)
                {
                    lstPages.Add(this.TabPages[i]);
                }
            }
            this.TabPages.Clear();
            this.TabPages.AddRange(lstPages.ToArray());
            this.TabPages.Insert(targetIndex, source);
            this.SelectedTab = source;
        }
    }
}
