using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBusinessLayer;
using Entities;

namespace Tanks
{
    public partial class LogForm : Form
    {
        DataBL data;
        DataGridView _grid = new DataGridView();
      
        public LogForm(DataBL _data)
        {
            InitializeComponent();
            data = _data;
            _grid.Dock = DockStyle.Fill;
            // _grid
            //_grid.DataSource = data.UpdateLog();

            _grid.Columns.Add("Name", "Name");
            _grid.Columns.Add("X", "X");
            _grid.Columns.Add("Y", "Y");

            Controls.Add(_grid);
        }

        private void LogForm_Load(object sender, EventArgs e)
        {
            
           
            refreshLog();
        }
        public void refreshLog()
        {
            //var source = new BindingSource(data.UpdateLog(), null);
            //_grid.DataSource = source;
            _grid.Rows.Clear();
            BindingList<LogView> entities = data.UpdateLog();
            for (int i = 0; i < entities.Count - 1; i++)
            {
                    _grid.Rows.Add();
                    _grid.Rows[i].Cells[0].Value = entities[i].Name;
                    _grid.Rows[i].Cells[1].Value = entities[i].X;
                    _grid.Rows[i].Cells[2].Value = entities[i].Y;
            }
            // _grid.Rows.Add(entities);
            _grid.Refresh();
        }
    }
}
