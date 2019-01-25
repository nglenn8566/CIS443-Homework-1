using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace CIS443_Homework_1
{
    public partial class Form1 : Form
    {
        XmlDocument doc = new XmlDocument();
        string firstName;
        string lastName;
        double hours;
        decimal payRate;
        decimal sumAnnualEarned;
        string maritalStatus;
        int numAllowances;
        decimal grossPay;
        decimal stateTax;
        decimal ficaTax;
        decimal fedHoldingTax;
        decimal netPay;
        const decimal overTime = 1.5m;
        const decimal miTaxRate = 0.0425m;
        const decimal ssRate = 0.062m;
        const decimal medicareRate = 0.0145m;
        const decimal maxSS = 118500m;
        const decimal allowanceMulti = 67.31m;

        public Form1()
        {
            InitializeComponent();
           
        }
        

        private void btnCalculate_Click(object sender, EventArgs e)
        {
           doc.Load("InPayroll.xml");
            //xml reader is another option
            XmlNode root = doc.FirstChild;

            //Display the contents of the child nodes.
            if (root.HasChildNodes)
            {
                
                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    if (root.ChildNodes[i].HasChildNodes)
                    {
                       
                        for (int y = 0; y < root.ChildNodes[i].ChildNodes.Count; y++)
                        {
                            if(root.ChildNodes[i].ChildNodes[y].Name == "firstName")
                            {
                                firstName = root.ChildNodes[i].ChildNodes[y].InnerXml;
                            }
                            else if (root.ChildNodes[i].ChildNodes[y].Name == "lastName")
                            {
                                lastName = root.ChildNodes[i].ChildNodes[y].InnerXml;

                            }
                            else if (root.ChildNodes[i].ChildNodes[y].Name == "hours")
                            {
                                hours = Convert.ToDouble(root.ChildNodes[i].ChildNodes[y].InnerXml);

                            }
                            else if (root.ChildNodes[i].ChildNodes[y].Name == "payRate")
                            {
                                payRate = Convert.ToDecimal(root.ChildNodes[i].ChildNodes[y].InnerXml);
                            }
                            else if (root.ChildNodes[i].ChildNodes[y].Name == "sumAnnualEarned")
                            {
                                sumAnnualEarned = Convert.ToDecimal(root.ChildNodes[i].ChildNodes[y].InnerXml);
                            }
                            else if (root.ChildNodes[i].ChildNodes[y].Name == "maritalStatus")
                            {
                                maritalStatus = root.ChildNodes[i].ChildNodes[y].InnerXml;

                            }
                            else if (root.ChildNodes[i].ChildNodes[y].Name == "numAllowances")
                            {
                                numAllowances = Convert.ToInt16(root.ChildNodes[i].ChildNodes[y].InnerXml);
                            }
                        }
                        //rtbReport.AppendText()
                        grossPay = calcGrossPay(Convert.ToDecimal(hours), payRate);
                        stateTax = calcStateTax(grossPay);
                        ficaTax = calcSsTax(grossPay) + calcMedicareTex(grossPay);
                        fedHoldingTax = calcFedTax(numAllowances, maritalStatus, grossPay);
                        netPay = grossPay - stateTax - ficaTax - fedHoldingTax;
                        Console.WriteLine("net pay " +netPay);
                        Console.WriteLine("gross pay " + grossPay);
                        Console.WriteLine("fica tax " +ficaTax);
                        Console.WriteLine("fed holding tax " +fedHoldingTax);
                        Console.WriteLine("State tax " +stateTax);

                    }

                }
            }
        }
  
        private Decimal calcGrossPay(Decimal hours, Decimal rate)
        {
            if (hours > 40)
            {
                return (40 * rate) + ((hours - 40) * overTime * rate);
            }
            else
            {
                return hours * rate;
            }
        }
        
        private Decimal calcStateTax(Decimal grPay)
        {
            return 0.0425m * grPay;
        }
        
        private Decimal calcSsTax(Decimal totEarned)
        {
            if (totEarned>=maxSS)
            {
                return maxSS * ssRate;
            }
            else
            {
                return totEarned * ssRate;
            }
        }

        private Decimal calcMedicareTex(Decimal grPay)
        {
            return medicareRate * grPay;
        }

        private Decimal calcFedTax(int allowance, string mStatus, decimal grPay)
        {
            decimal awi = Convert.ToDecimal(allowance) * allowanceMulti;
    
            if(mStatus == "Single")
            {
                if (awi > 43 && awi <= 222)
                {
                    return grPay * .1m;
                }
                else if (awi > 222 && awi < 767)
                {
                    return 17.90m+(grPay * .15m);
                }
                else if (awi > 767 && awi < 1796)
                {
                    return 99.65m + (grPay * .25m);
                }
                else if (awi > 1796 && awi < 3700)
                {
                    return 356.90m + (grPay * .28m);
                }
                else if (awi > 3700 && awi < 7992)
                {
                    return 890.02m + (grPay * .33m);
                }
                else if (awi > 7922 && awi < 8025)
                {
                    return 2306.38m + (grPay * .35m);
                }
                else if (awi > 8025)
                {
                    return 2317.93m + (grPay * .396m);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                if (awi > 164 && awi <= 521)
                {
                    return grPay * .1m;
                }
                else if (awi > 521 && awi < 1613)
                {
                    return 35.70m + (grPay * .15m);
                }
                else if (awi > 1613 && awi < 3086)
                {
                    return 199.5m + (grPay * .25m);
                }
                else if (awi > 3086 && awi < 4615)
                {
                    return 567.75m + (grPay * .28m);
                }
                else if (awi > 4615 && awi < 8113)
                {
                    return 995.87m + (grPay * .33m);
                }
                else if (awi > 8113 && awi < 9144)
                {
                    return 2150.21m + (grPay * .35m);
                }
                else if (awi > 9144)
                {
                    return 2511.06m + (grPay * .396m);
                }
                else
                {
                    return 0;
                }
            }
            

        }
    }
}
