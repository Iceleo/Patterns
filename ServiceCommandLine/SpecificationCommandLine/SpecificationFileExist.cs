using System;
using System.Linq.Expressions;
using System.Reflection;
using Patterns.SpecificationClassic;
//using UserServices.ICommandLineService;


/// <summary>
///  �������� ������������� �����
/// </summary>
public class SpecificationFileExist : SpecificationClassic<ICommandLineRun>
//SpecificationExpression<CommandLineSample>
{
	readonly string propName;
    public SpecificationFileExist(string NameProp)
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
	    bool rc = false;
        PropertyInfo pp = boss.GetType().GetProperty(propName);
	    string file = pp.GetValue( boss, null).ToString(); 

        //// �������� ���������� ��������� ������        
        if (!string.IsNullOrEmpty(file) && System.IO.File.Exists(file)) //���� ����
        {            
	        rc = true;
        }
        else
        {
            boss.AddError( propName, string.Format(
		        $" ���� ��� ��� ����� {file} - ��������."));
        }
        return	rc;
	}
}
