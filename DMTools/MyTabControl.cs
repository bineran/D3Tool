using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTools
{
    public  class MyTabControl : TabControl
    {
        public MyTabControl()
        {
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
