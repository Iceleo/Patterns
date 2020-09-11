using System;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Reflection;
using Digital_Patterns.SpecificationClassic;

/// <summary>
///  �������� ������������� ����������
/// </summary>
public class SpecificationDirectoryExist : SpecificationClassic<ICommandLineRun>
//SpecificationExpression<CommandLineSample>
{
	readonly string propName;
    public SpecificationDirectoryExist(string NameProp)//, string propName)
    {
        propName = NameProp;
    }

    public override bool IsSatisfiedBy(ICommandLineRun boss)
    { //
	    bool rc = false;
        PropertyInfo pp = boss.GetType().GetProperty(propName);
        string directory = pp.GetValue( boss, null).ToString();
//// �������� ���������� ��������� ������
       if (directory[directory.Length-1] != CommandLineService.sleshBack) // ��� ������������ �����
           directory += CommandLineService.sleshBack;

      if ( !string.IsNullOrEmpty(directory) && 
            System.IO.Directory.Exists(directory)) //  "���������� {directory} ����������.";
	        rc = true;
       else
       {
         boss.AddError( propName,  string.Format(
             $" ���� ���������� {directory} - ��������."));
        } 
	  return	rc;
	}
}
