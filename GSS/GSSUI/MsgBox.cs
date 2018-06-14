using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GSSUI
{
    public class MsgBox
    {
        private MsgBox()
        {

        }
        public static DialogResult Show(string message)
        {
            AForm.FormMsgBox im = new AForm.FormMsgBox(message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            im.StartPosition = FormStartPosition.CenterScreen;
            return im.ShowDialog();
        }

        public static void Show(string title, string message)
        {
            AForm.FormMsgBox im = new AForm.FormMsgBox(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            im.StartPosition = FormStartPosition.CenterScreen;
            im.Show();
        }

        public static DialogResult Show(string message, string title, MessageBoxButtons butn, MessageBoxIcon icon)
        {
            AForm.FormMsgBox im = new AForm.FormMsgBox(message, title, butn, icon);
            im.StartPosition = FormStartPosition.CenterScreen;
            return im.ShowDialog();
        }

        public static DialogResult Show(IWin32Window form, string message)
        {
            AForm.FormMsgBox im = new AForm.FormMsgBox(message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            im.StartPosition = FormStartPosition.CenterParent;
            return im.ShowDialog(form);
        }

        public static DialogResult Show(IWin32Window form, string title, string message)
        {
            AForm.FormMsgBox im = new AForm.FormMsgBox(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            im.StartPosition = FormStartPosition.CenterParent;
            return im.ShowDialog(form);
        }

        public static DialogResult Show(IWin32Window form, string title, string message, MessageBoxIcon mbi)
        {
            AForm.FormMsgBox im = new AForm.FormMsgBox(message, title, MessageBoxButtons.OK, mbi);
            im.StartPosition = FormStartPosition.CenterParent;
            return im.ShowDialog(form);
        }

        public static DialogResult Show(IWin32Window form, string title, string message, MessageBoxButtons mbb)
        {
            AForm.FormMsgBox im = new AForm.FormMsgBox(message, title, mbb, MessageBoxIcon.Information);
            im.StartPosition = FormStartPosition.CenterParent;
            return im.ShowDialog(form);
        }

        public static DialogResult Show(IWin32Window form, string title, string message, MessageBoxButtons mbb, MessageBoxIcon mbi)
        {
            AForm.FormMsgBox im = new AForm.FormMsgBox(message, title, mbb, mbi);
            im.StartPosition = FormStartPosition.CenterParent;
            return im.ShowDialog(form);
        }
    }
}
