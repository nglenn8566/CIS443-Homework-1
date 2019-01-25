
using System;
using System.Windows.Forms;
using System.Xml;
namespace CustomerMaintenance
{
    public static class Validator
    {
        private static string title = "Entry Error";
        public static string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }
        public static bool IsPresent(TextBox textBox)
        {
            if (textBox.Text == "")
            {
                MessageBox.Show(textBox.Tag + " is a required field.", Title);
                textBox.Focus();
                return false;
            }
            return true;
        }
        public static bool IsDecimal(TextBox textBox)
        {
            try
            {
                Convert.ToDecimal(textBox.Text);
                return true;
            }
            catch (FormatException)
            {
                MessageBox.Show(textBox.Tag + " must be a decimal number.",
               Title);
                textBox.Focus();
                return false;
            }
        }
        public static bool IsWithinRange(TextBox textBox, decimal min, decimal max)
        {
            decimal number = Convert.ToDecimal(textBox.Text);
            if (number < min || number > max)
            {
                MessageBox.Show(textBox.Tag + " must be between " + min
                + " and " + max + ".", Title);
                textBox.Focus();
                return false;
            }
            return true;
        }
        public static bool IsNotEmpty(XmlText input)
        {
            string date = DateTime.Today.ToShortDateString();
            XmlWriter xmlOut = XmlWriter.Create($"OutPayrollError{date}.xml");
            xmlOut.WriteStartElement("error");
            xmlOut.WriteString(input.Value);
            //check field value
            //if ()
            //{
            //return true;
            //}
            //else{
            //return false;
            //}
            return false;
        }
    }
}