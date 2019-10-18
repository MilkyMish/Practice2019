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
            _grid.DataSource = data.UpdateLog();
            Controls.Add(_grid);
        }

        private void LogForm_Load(object sender, EventArgs e)
        {
            
           
            refreshLog();
        }
        public void refreshLog()
        {
            var source = new BindingSource(data.UpdateLog(), null);
            _grid.DataSource = source;
            _grid.Refresh();
        }
    }
}
