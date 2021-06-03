using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GGHS_Time_Table_Creator
{
    public partial class Form1 : Form
    {
        public static string code;
        Dictionary<string, string> subjectDict = new Dictionary<string, string>()
        {
            ["예시"] = "모습",
        };
        string[,] array = new string[5, 7];

        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new object[]{1, 2, 3, 4, 5, 6, 7, 8});
            foreach (var comboBox in ComboBoxes())
            {
                comboBox.Items.AddRange(new object[]
                {
                    "문학",
                    "비영",
                });
            }
        }

        IEnumerable <ComboBox> ComboBoxes()
        {
            yield return comboBox2; 
            yield return comboBox3; 
            yield return comboBox4; 
            yield return comboBox5;
            yield return comboBox6;
            yield return comboBox7;
            yield return comboBox8;
            yield return comboBox9;
            yield return comboBox10;
            yield return comboBox11;
            yield return comboBox12;
            yield return comboBox13;
            yield return comboBox14;
            yield return comboBox15;
            yield return comboBox16;
            yield return comboBox17;
            yield return comboBox18;
            yield return comboBox19;
            yield return comboBox20;
            yield return comboBox21;
            yield return comboBox22;
            yield return comboBox23;
            yield return comboBox24;
            yield return comboBox25;
            yield return comboBox26;
            yield return comboBox27;
            yield return comboBox28;
            yield return comboBox29;
            yield return comboBox30;
            yield return comboBox31;
            yield return comboBox32;
            yield return comboBox33;
            yield return comboBox34;
            yield return comboBox35;
            yield return comboBox36;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void GetComboBoxText()
        {
            foreach (var item in array)
            {

            }

        }

        void SetString()
        {
            code = $@"
 // TimeTables.cs

public static readonly string[,] Class{comboBox1.Text} = new string[5, 7]
{{
    {{ Subjects.{ array[0, 0] }, Subjects.{ array[0, 1]}, Subjects.{array[0, 2]}, Subjects.{array[0,3]}, Subjects.{array[0, 4]}, Subjects.{array[0, 5]}, Subjects.{array[0, 6]} }},
    {{ Subjects.{ array[1, 0] }, Subjects.{ array[1, 1]}, Subjects.{array[1, 2]}, Subjects.{array[1, 3]}, Subjects.{array[1, 4]}, Subjects.{array[1, 5]}, Subjects.{array[1, 6]} }},
    {{ Subjects.{ array[2, 0] }, Subjects.{ array[2, 1]}, Subjects.{array[2, 2]}, Subjects.{array[2, 3]}, Subjects.{array[2, 4]}, Subjects.{array[2, 5]}, Subjects.{array[2, 6]} }},
    {{ Subjects.{ array[3, 0] }, Subjects.{ array[3, 1]}, Subjects.{array[3, 2]}, Subjects.{array[3, 3]}, Subjects.{array[3, 4]}, Subjects.{array[3, 5]}, Subjects.{array[3, 6]} }},
    {{ Subjects.{ array[4, 0] }, Subjects.{ array[4, 1]}, Subjects.{array[4, 2]}, Subjects.{array[4, 3]}, Subjects.{array[4, 4]}, Subjects.{array[4, 5]}, Subjects.{array[4, 6]} }},
}}; ";
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in ComboBoxes())
            {
                item.SelectedIndex = -1;
            }
        }
    }
}
