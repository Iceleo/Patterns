using System;
using System.Collections.Generic;
//using System.Linq.Expressions;
using System.Reflection;
using Digital_Patterns.SpecificationClassic;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// �������� ������������� �������� �� ������ ��������� ������ 
/// ������ �������� �������� ������� � ������ ������ ����� ��������� ������
/// </summary>
public class SpecificationFromAnnotations : SpecificationClassic<ICommandLineRun>
//SpecificationExpression<CommandLineSample>
{
    /// <summary>
    /// ��� �������� ��� �������� ������������� �� ������ ��������� ������ 
    /// </summary>
	readonly string propName;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="NameProp">��� �������� ��� ��������</param>
    public SpecificationFromAnnotations(string NameProp)
    {
        propName = NameProp;
    }

    /// <summary>
    /// �������� ������������� �����
    /// </summary>
    /// <param name="boss">����� ��������� ������</param>
    /// <returns>true - ���� ����������.</returns>
    public override bool IsSatisfiedBy(ICommandLineRun boss)
    { //
        PropertyInfo pp = boss.GetType().GetProperty(propName);
	    var value = pp.GetValue( boss, null).ToString();
            List<ValidationResult> results = new List<ValidationResult>(); //����������� � ��������� �������. 
            var vc = new ValidationContext(this, null, null) { MemberName = propName };
        bool rc = false;
        try
        {
            // Validator ��������� ���������, ���� �� � ������� ������, ��������� � ����������� ������, � ValidationContext. 
            rc = Validator.TryValidateProperty(value, vc, results);

            if (!rc) //
                boss.AddListErrors(propName, results.ConvertAll<string>(
                        (o => o.ErrorMessage)));
        }
        catch (System.ArgumentNullException e1)
        {//     value cannot be assigned to the property. -or- value is null.
            boss.AddError(propName, string.Format(
           $"Value cannot be assigned to the property. -or- value is null."));
        }
        catch (System.ArgumentException e2)
        //     The System.ComponentModel.DataAnnotations.ValidationContext.MemberName property
        //     of validationContext is not a valid property.)
        {
            boss.AddError(propName, string.Format(
                   $"Property {propName} is not a valid."));
        }
        return	rc;
	}
}
