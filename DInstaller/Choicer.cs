using dnlib.DotNet;
using dnlib.DotNet.Writer;
using DInstaller.Protection.Anti;
using DInstaller.Protection.INT;
using DInstaller.Protection.Renamer;
using DInstaller.Protection.String;

namespace DInstaller
{
    public partial class Choicer : Form
    {
        public static MethodDef? Init;
        public static MethodDef? Init2;
        private ModuleDefMD Md { get; set; } = null!;
        private string _directoryName = string.Empty;
        public Choicer()
        {
            InitializeComponent();
        }

        private static void AppendMsg(RichTextBox rtb, Color color, string text, bool autoTime)
        {
            rtb.BeginInvoke(new ThreadStart(() =>
            {
                lock (rtb)
                {
                    rtb.Focus();
                    if (rtb.TextLength > 100000)
                    {
                        rtb.Clear();
                    }

                    using var temp = new RichTextBox();
                    temp.SelectionColor = color;
                    if (autoTime)
                        temp.AppendText(DateTime.Now.ToString("HH:mm:ss"));
                    temp.AppendText(text);
                    rtb.Select(rtb.Rtf.Length, 0);
                    rtb.SelectedRtf = temp.Rtf;
                }
            }));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
                
        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                var array = (Array)e.Data?.GetData(DataFormats.FileDrop)!;
                var text = array.GetValue(0)!.ToString();
                var num = text!.LastIndexOf(".", StringComparison.Ordinal);
                if (num == -1)
                    return;
                var text2 = text[num..];
                text2 = text2.ToLower();
                if (string.Compare(text2, ".exe", StringComparison.Ordinal) != 0 && string.Compare(text2, ".dll", StringComparison.Ordinal) != 0)
                {
                    return;
                }

                Activate();
                textBox1.Text = text;
                var num2 = text.LastIndexOf("\\", StringComparison.Ordinal);
                if (num2 != -1)
                {
                    _directoryName = text.Remove(num2, text.Length - num2);
                }

                if (_directoryName.Length == 2)
                {
                    _directoryName += "\\";
                }
            }
            catch
            {
                /* ignored */
            }
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e) => e.Effect = e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;

        private void button1_Click(object sender, EventArgs e)
        {
            Md = ModuleDefMD.Load(textBox1.Text);
            foreach (Action func in _proc)
            {
                func();
            }
            var text2 = Path.GetDirectoryName(textBox1.Text);
            if (text2 != null && !text2.EndsWith("\\"))
                text2 += "\\";
            var path = $"{text2}{Path.GetFileNameWithoutExtension(textBox1.Text)}_protected{Path.GetExtension(textBox1.Text)}";

            var opts = new ModuleWriterOptions(Md)
            {
                Logger = DummyLogger.NoThrowInstance
            };
            Md.Write(path, opts);

            AppendMsg(richTextBox1, Color.Red, $"Save: {path}", true);
        }
        
        private readonly List<Action> _proc = new();

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                _proc.Add(ProcessStringEncrypt);
                listBox1.Items.Add("-> String Encrypt");
            }
            else
            {
                _proc.Remove(ProcessStringEncrypt);
                listBox1.Items.Remove("-> String Encrypt");
            }
        }

        private void ProcessStringEncrypt()
        {
            StringEncPhase.Execute(Md);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                _proc.Add(ProcessIntConfusion);
                listBox1.Items.Add("-> IntConfusion");
            }
            else
            {
                _proc.Remove(ProcessIntConfusion);
                listBox1.Items.Remove("-> IntConfusion");
            }
        }

        private void ProcessIntConfusion()
        {
            AddIntPhase.Execute2(Md);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                _proc.Add(ProcessRenamer);
                listBox1.Items.Add("-> Renamer");
            }
            else
            {
                _proc.Remove(ProcessRenamer);
                listBox1.Items.Remove("-> Renamer");
            }
        }

        private void ProcessRenamer()
        {
            RenamerPhase.ExecuteClassRenaming(Md);
            RenamerPhase.ExecuteFieldRenaming(Md);
            RenamerPhase.ExecuteMethodRenaming(Md);
            RenamerPhase.ExecuteModuleRenaming(Md);
            RenamerPhase.ExecuteNamespaceRenaming(Md);
            RenamerPhase.ExecutePropertiesRenaming(Md);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                _proc.Add(ProcessAntiDebug);
                listBox1.Items.Add("-> Anti Debug");
            }
            else
            {
                _proc.Remove(ProcessAntiDebug);
                listBox1.Items.Remove("-> Anti Debug");
            }
        }

        private void ProcessAntiDebug()
        {
            AntiDebug.Execute(Md);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                _proc.Add(ProcessAntiDump);
                listBox1.Items.Add("-> Anti Dump");
            }
            else
            {
                _proc.Remove(ProcessAntiDump);
                listBox1.Items.Remove("-> Anti Dump");
            }
        }

        private void ProcessAntiDump()
        {
            AntiDump.Execute(Md);
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                _proc.Add(ProcessAntiTamper);
                listBox1.Items.Add("-> Anti Tamper");
            }
            else
            {
                _proc.Remove(ProcessAntiTamper);
                listBox1.Items.Remove("-> Anti Tamper");
            }
        }

        private void ProcessAntiTamper()
        {
            AntiTamper.Execute(Md);
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                _proc.Add(ProcessAntimanything);
                listBox1.Items.Add("-> Anti manything");
            }
            else
            {
                _proc.Remove(ProcessAntimanything);
                listBox1.Items.Remove("-> Anti manything");
            }
        }

        private void ProcessAntimanything()
        {
            Antimanything.Execute(Md);
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked)
            {
                MessageBox.Show("Перевірка ліцензійного ключа успішно вбудована", Convert.ToString(MessageBoxButtons.OK));
            }
        }
    }
}
