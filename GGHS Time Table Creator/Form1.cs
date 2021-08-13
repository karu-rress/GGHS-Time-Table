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
        public static string code; // TotalCode
        Dictionary<string, string> subjectDict = new Dictionary<string, string>()
        {
            ["독서"] = "Reading",
            ["수학Ⅱ"] = "Mathematics",
            ["심화영어Ⅰ"] = "AdvancedEnglish",
            ["운동과 건강"] = "Sport",
            ["창의적 문제 해결 기법"] = "CreativeSolve",
            ["수학과제탐구"] = "MathResearch",
            ["창의적 체험활동"] = "Others",
            ["홈커밍"] = "HomeComing",

            ["과학선택"] = "Sciences",
            ["과학사"] = "ScienceHistory",
            ["생활과 과학"] = "LifeAndScience",

            ["전문1"] = "Specials1",
            ["전문2"] = "Specials2",
            ["국제경제"] = "GlobalEconomics",
            ["국제정치"] = "GlobalPolitics",
            ["비교문화"] = "CompareCulture",
            ["동양근대사"] = "EasternHistory",
            ["세계 역사와 문화"] = "HistoryAndCulture",
            ["현대정치철학의 이해"] = "PoliticsPhilosophy",
            ["세계 지역 연구"] = "RegionResearch",
            ["공간 정보와 공간 분석"] = "GISAnalyze",

            ["외국어"] = "Languages",
            ["일본어Ⅰ"] = "Japanese",
            ["스페인어Ⅰ"] = "Spanish",
            ["중국어Ⅰ"] = "Chinese",
        };

        string[,] array = new string[5, 7]; // Subject Array

        string[] subjects = new string[]
        {
            "독서",
            "수학Ⅱ",
            "심화영어Ⅰ",
            "운동과 건강",
            "창의적 문제 해결 기법",
            "수학과제탐구",
            "창의적 체험활동",
            "홈커밍",
            "과학선택",
            "전문1",
            "전문2",
            "외국어",
        };

        int @class = 0;

        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new object[]{1, 2, 3, 4, 5, 6, 7, 8});
            foreach (var comboBox in ComboBoxes())
            {
                comboBox.Items.AddRange(subjects);
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
            yield return comboBox27;
            yield return comboBox23;
            yield return comboBox32;
            yield return comboBox28;
            yield return comboBox24;
            yield return comboBox33;
            yield return comboBox29;
            yield return comboBox25;
            yield return comboBox34;
            yield return comboBox26;
            yield return comboBox30;
            yield return comboBox31;
            yield return comboBox35;
            yield return comboBox36;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // PASS
        }

        // ...Selected


        private bool IsSelectiveSubject(string subject) => subject switch
        {
            "과학선택" or "전문1" or "전문2" or "외국어" => true,
            _ => false,
        };

        private void comboBoxChange(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            @class = int.Parse(comboBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int comboBoxIndex = 0;
            

            for (int y = 0; y < 7; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    var subject = ComboBoxes().ElementAt(comboBoxIndex).Text;
                    array[x, y] = IsSelectiveSubject(subject) ? $"{subjectDict[subject]}.Selected" : $"CellName.{subjectDict[subject]}";
                    comboBoxIndex++;
                }
            }

            SetString();
            Form2 frm = new Form2();
            frm.Show();

            void SetString()
            {
                code = $@"
 // TimeTables.cs

public string[,] Class{comboBox1.Text} = new string[5, 7]
{{
    {{ Subjects.{ array[0, 0] }, Subjects.{ array[0, 1]}, Subjects.{array[0, 2]}, Subjects.{array[0, 3]}, Subjects.{array[0, 4]}, Subjects.{array[0, 5]}, Subjects.{array[0, 6]} }},
    {{ Subjects.{ array[1, 0] }, Subjects.{ array[1, 1]}, Subjects.{array[1, 2]}, Subjects.{array[1, 3]}, Subjects.{array[1, 4]}, Subjects.{array[1, 5]}, Subjects.{array[1, 6]} }},
    {{ Subjects.{ array[2, 0] }, Subjects.{ array[2, 1]}, Subjects.{array[2, 2]}, Subjects.{array[2, 3]}, Subjects.{array[2, 4]}, Subjects.{array[2, 5]}, Subjects.{array[2, 6]} }},
    {{ Subjects.{ array[3, 0] }, Subjects.{ array[3, 1]}, Subjects.{array[3, 2]}, Subjects.{array[3, 3]}, Subjects.{array[3, 4]}, Subjects.{array[3, 5]}, Subjects.{array[3, 6]} }},
    {{ Subjects.{ array[4, 0] }, Subjects.{ array[4, 1]}, Subjects.{array[4, 2]}, Subjects.{array[4, 3]}, Subjects.{array[4, 4]}, Subjects.{array[4, 5]}, Subjects.{array[4, 6]} }},
}}; ";
            }
        }
    }
}
