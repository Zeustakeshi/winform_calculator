using System;
using System.Windows.Forms;

namespace test_app
{
    public partial class Form1 : Form
    {

        private double currentValue = 0.0;
        private double accumulate = 0.0;
        private string expression = "";
        private Operator lastOperator = Operator.NONE;
        private bool decimalMode = false;

        public Form1()
        {
            InitializeComponent();
        }



        private void operatorButtonClick(Button button, Operator o)
        {
            bool isFirst = lastOperator.Equals(Operator.NONE);

            accumulate = basicCaculator(accumulate, currentValue, lastOperator);
            if (isFirst)
            {
                expression = Convert.ToString(currentValue) + ParseOperator(o);
                LabelExpression.Text = expression;
            }
            else
            {
                expression = Convert.ToString(accumulate) + ParseOperator(o);
                LabelExpression.Text = expression;
            }

            currentValue = 0;
            decimalMode = false;
            ShowCurrentValue();
            lastOperator = o;

        }

        private double basicCaculator(double num1, double num2, Operator o)
        {
            switch (o)
            {
                case Operator.SUM: return num1 + num2;
                case Operator.SUB: return num1 - num2;
                case Operator.MUTILPLE: return num1 * num2;
                case Operator.DIV:
                    {
                        if (num2 == 0)
                        {
                            MessageBox.Show("Cannot divide by zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return num1 / num2;
                    }
                default: return num2;
            }
        }

        private void NumbericButtonClick(Button button)
        {
            if (decimalMode)
            {
                string currentValueString = Convert.ToString(currentValue);
                string[] tmp = currentValueString.Split('.');
                int numberOfDigits = 0;

                if (tmp.Length >= 2)
                {
                    numberOfDigits = tmp[1].Length;
                }

                string digits = "0.";

                for (int i = 0; i < numberOfDigits; ++i)
                {
                    digits += "0";
                }
                digits += button.Text;

                currentValue += Convert.ToDouble(digits);

            }
            else
            {
                currentValue = currentValue * 10 + Convert.ToDouble(button.Text);
            }
            ShowCurrentValue();

        }

        private void showResult()
        {
            if (accumulate == 0 && currentValue != 0)
            {
                accumulate = currentValue;
                LabelExpression.Text = Convert.ToString(accumulate);

            }
            if (lastOperator.Equals(Operator.NONE)) return;
            accumulate = basicCaculator(accumulate, currentValue, lastOperator);
            LabelExpression.Text = expression + Convert.ToString(currentValue) + "=";
            currentValue = accumulate;
            ShowCurrentValue();
            decimalMode = false;
            lastOperator = Operator.NONE;
        }




        private void ShowCurrentValue()
        {
            string currentValueString;
            bool isNegative = currentValue < 0;
            bool isDecimal = !currentValue.Equals(Math.Floor(currentValue));


            if (currentValue == 0.0) currentValueString = "0";
            else currentValueString = Convert.ToString(Math.Abs(currentValue));



            string sign = isNegative ? "-" : "";
            string displayText = isDecimal ? currentValueString : formatResult(currentValueString);
            LabelResult.Text = sign + displayText;
        }



        private void ClearAll()
        {
            currentValue = 0.0;
            accumulate = 0.0;
            LabelExpression.Text = "0";
            decimalMode = false;
            expression = "";
            ShowCurrentValue();
        }

        private void ClearEntry()
        {
            currentValue = 0.0;
            decimalMode = false;
            ShowCurrentValue();
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            NumbericButtonClick(Btn1);
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            NumbericButtonClick(Btn2);
        }


        private void Btn3_Click(object sender, EventArgs e)
        {
            NumbericButtonClick(Btn3);
        }

        private void Btn4_Click(object sender, EventArgs e)
        {
            NumbericButtonClick(Btn4);
        }

        private void Btn5_Click(object sender, EventArgs e)
        {
            NumbericButtonClick(Btn5);
        }

        private void Btn6_Click(object sender, EventArgs e)
        {
            NumbericButtonClick(Btn6);
        }

        private void Btn7_Click(object sender, EventArgs e)
        {
            NumbericButtonClick(Btn7);
        }

        private void Btn8_Click(object sender, EventArgs e)
        {
            NumbericButtonClick(Btn8);
        }

        private void Btn9_Click(object sender, EventArgs e)
        {
            NumbericButtonClick(Btn9);
        }

        private void Btn0_Click(object sender, EventArgs e)
        {
            if (currentValue != 0)
            {
                NumbericButtonClick(Btn0);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void buttonClearEntry_Click(object sender, EventArgs e)
        {
            ClearEntry();
        }

        private void BtnSum_Click(object sender, EventArgs e)
        {
            operatorButtonClick(BtnSum, Operator.SUM);
        }

        private void BtnSub_Click(object sender, EventArgs e)
        {
            operatorButtonClick(BtnSub, Operator.SUB);
        }

        private void BtnMulti_Click(object sender, EventArgs e)
        {
            operatorButtonClick(BtnMulti, Operator.MUTILPLE);
        }

        private void BtnDiv_Click(object sender, EventArgs e)
        {
            operatorButtonClick(BtnDiv, Operator.DIV);
        }

        private void BtnChangeSign_Click(object sender, EventArgs e)
        {
            if (currentValue == 0) return;
            currentValue *= -1;
            ShowCurrentValue();
        }

        private void BtnDecimal_Click(object sender, EventArgs e)
        {
      
            decimalMode = true;
            LabelResult.Text = LabelResult.Text + ".";
        }

        private void BtnPercent_Click(object sender, EventArgs e)
        {
            if (lastOperator == Operator.NONE)
            {
                LabelExpression.Text = Convert.ToString(currentValue) + "% =";
            }
            else
            {
                LabelExpression.Text = expression + Convert.ToString(currentValue) + "% =";

            }
            accumulate = basicCaculator(currentValue, 100, Operator.DIV);
            currentValue = accumulate;
            ShowCurrentValue();

        }

        private void BtnInverseX_Click(object sender, EventArgs e)
        {
            if (lastOperator == Operator.NONE)
            {
                LabelExpression.Text = "1/(" + Convert.ToString(currentValue) + ") =";
            }
            else
            {
                LabelExpression.Text = expression + "1 / (" + Convert.ToString(currentValue) + ") =";
            }
            accumulate = basicCaculator(1, currentValue, Operator.DIV);
            currentValue = accumulate;
            ShowCurrentValue();
        }

        private void BtnSquaredX_Click(object sender, EventArgs e)
        {
            if (lastOperator == Operator.NONE)
            {
                LabelExpression.Text = "sqr(" + Convert.ToString(currentValue) + ") =";
            }
            else
            {
                LabelExpression.Text = expression + "sqr(" + Convert.ToString(currentValue) + ") =";
            }
            accumulate = basicCaculator(currentValue, currentValue, Operator.MUTILPLE);
            currentValue = accumulate;
            ShowCurrentValue();
        }

        private void BtnSqrtX_Click(object sender, EventArgs e)
        {
            if (lastOperator == Operator.NONE)
            {
                LabelExpression.Text = "√(" + Convert.ToString(currentValue) + ") =";
            }
            else
            {
                LabelExpression.Text = expression + "√(" + Convert.ToString(currentValue) + ") =";
            }
            currentValue = Math.Sqrt(currentValue);
            ShowCurrentValue();
        }

        private void BtnResult_Click(object sender, EventArgs e)
        {
            showResult();
        }

        private void BtnBackSpace_Click(object sender, EventArgs e)
        {
            string currentValueString = Convert.ToString(currentValue);
            currentValueString = currentValueString.Substring(0, Math.Max(currentValueString.Length - 1, 0));
            if (currentValueString.Equals("") || currentValueString.Equals("-")) currentValueString = "0";
            currentValue = Convert.ToDouble(currentValueString);
            ShowCurrentValue();
        }



        private string ParseOperator(Operator o)
        {
            switch (o)
            {
                case Operator.SUM: return " + ";
                case Operator.SUB: return " - ";
                case Operator.MUTILPLE: return " × ";
                case Operator.DIV: return " ÷ ";
                default: return "";
            }
        }

        private enum Operator
        {
            SUM,
            SUB,
            MUTILPLE,
            DIV,
            NONE
        }

        private string formatResult(string value)
        {
            string tmp = "";
            string formatStr = "";
            for (int i = value.Length - 1, cnt = 0; i >= 0; --i, cnt++)
            {
                if (cnt != 0 && cnt % 3 == 0) tmp += ",";
                tmp += value[i];
            }

            for (int i = tmp.Length - 1; i >= 0; --i)
            {
                formatStr += tmp[i];
            }
            return formatStr;
        }

      
    }
}
