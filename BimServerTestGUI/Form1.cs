using BusinessLogic;
using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BimServerTestGUI
{
    public partial class Form1 : Form
    {
        private BimServerController ctrl;
        private string token;
        public Form1()
        {
            InitializeComponent();
            ctrl = new BimServerController();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            token = ctrl.GetLoginToken("hansen.ronni@gmail.com", "rONNI4865");
            var projects = ctrl.GetAllProjects(token);

            foreach (var project in projects)
            {
                comboBox1.Items.Add(project);
            }

        }

        private void SelectedProject(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();

            Project project = comboBox1.SelectedItem as Project;
            var revisions = ctrl.GetAllRevisionsByProject(project.Id, token);
            foreach (var item in revisions)
            {
                comboBox2.Items.Add(item);
            }
        }

        private void SelectedRevision(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            Revision selectedRevision = (Revision)comboBox2.SelectedItem;

            //Get Rooms
            var spaces = ctrl.GetSpace(selectedRevision.Id, token);
            foreach (var item in spaces)
            {
                comboBox3.Items.Add(item);
            }
        }

        private void SelectedSpace(object sender, EventArgs e)
        {
            Revision selectedRevision = (Revision)comboBox2.SelectedItem;
            Space selectedSpace = (Space)comboBox3.SelectedItem;

            var properties = ctrl.GetSpaceProperties(selectedSpace.Id, selectedRevision.Id, token);
        }
    }
}
