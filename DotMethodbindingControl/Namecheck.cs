using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Controls;

namespace DotMethodbindingControl
{
    public class Namecheck : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            var name = Convert.ToString(value);
            return name.Length > 4 ? new ValidationResult(false, "���ֳ��Ȳ��ܴ���4�����ȣ�") : ValidationResult.ValidResult;
            //throw new System.NotImplementedException();
        }
    }
}