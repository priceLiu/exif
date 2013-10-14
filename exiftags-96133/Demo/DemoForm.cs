using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Danielyan.Exif;

namespace Demo
{
    public partial class DemoForm : Form
    {
        public DemoForm()
        {
            InitializeComponent();
        }

        private ExifTags _exif;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPEG Files (*.jpg)|*.jpg|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(ofd.FileName);
                pictureBox.Image = img;
                _exif = new ExifTags(img);

                listExif.Items.Clear();
                
                listExif.BeginUpdate();

                foreach (ExifTag tag in _exif.Values)
                    AddTagToList(tag);

                listExif.EndUpdate();

                comboFields.Items.Clear();
                foreach (string name in ExifTags.SupportedTagNames)
                    comboFields.Items.Add(name);
            }
        }

        private void AddTagToList(ExifTag tag)
        {
            ListViewItem item = listExif.Items.Add(tag.Id.ToString());
            item.SubItems.Add(tag.FieldName);
            item.SubItems.Add(tag.Description);
            item.SubItems.Add(tag.Value);
        }

        private void btnLookup_Click(object sender, EventArgs e)
        {
            if (_exif == null)
            {
                MessageBox.Show("Image not loaded");
                return;
            }

            if (comboFields.Text == "")
            {
                MessageBox.Show("Choose field to show");
                return;
            }

            try
            {
                ExifTag tag = _exif[comboFields.Text];

                MessageBox.Show(String.Format("Name: {0}\nDescription: {1}\nValue: {2}",
                    tag.FieldName,
                    tag.Description,
                    tag.Value));               
            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show("Field not found: " + comboFields.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
