using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotmail_Get_Oauth2
{
    class ThreadHelperClass
    {

        delegate void SetTextCallback(Form f, Control ctrl, string text);
        /// <summary>
        /// Set text property of various controls
        /// </summary>
        /// <param name="form">The calling form</param>
        /// <param name="ctrl"></param>
        /// <param name="text"></param>
        public static void SetText(Form form, Control ctrl, string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (ctrl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                form.Invoke(d, new object[] { form, ctrl, text });
            }
            else
            {
                ctrl.Text = text;
            }
        }


        delegate void AppendTextCallBack(RichTextBox textBox, string msg);

        public static void AppendText(RichTextBox textBox, string msg)
        {
            Action append = () =>
            {
                textBox.AppendText(msg);
            };
            

            if (textBox.InvokeRequired)
                textBox.BeginInvoke(append);
            else
                append();
        }
        public static void SetText(TextBox textBox, string msg)
        {
            Action append = () =>
            {
                textBox.Text = msg;
            };


            if (textBox.InvokeRequired)
                textBox.BeginInvoke(append);
            else
                append();
        }


        delegate void AppendLogCallBack(RichTextBox textBox, string msg);

        public static void AppendLogText(RichTextBox textBox, string msg)
        {
            Action append = () =>
            {   if (textBox.Text.Length > 5000)
                {
                    textBox.Clear();
                }
                textBox.AppendText(msg);


            };
            if (textBox.InvokeRequired)
                textBox.BeginInvoke(append);
            else
                append();
        }
    }
}
