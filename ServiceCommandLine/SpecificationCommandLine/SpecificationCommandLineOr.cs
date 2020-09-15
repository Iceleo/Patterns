using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Patterns.SpecificationClassic;
//using UserServices.ICommandLineService;
//using UserServices.CommandLineService;

/// <summary>
/// ���������� �/��� ��� ������  ���������� �� ��������� ������. 
/// ��� ���������� ����������� ������ ��� ������ �� �����
/// </summary>
public class SpecificationCommandLineOr : SpecificationCommandLine<List<ICommandLineSample>>
//SpecificationExpression<List<CommandLineSample>>
{
 
    public SpecificationCommandLineOr(ICommandLineSample _leftCommand, ICommandLineSample _rightCommand)
		: base (_leftCommand, _rightCommand, "|") { }
   public override bool IsSatisfiedBy(List<ICommandLineSample> cmdList)
	{ //
	  bool rc = (cmdList.Contains(leftCommand)) | (cmdList.Contains(rightCommand));
	  if ( !rc) //
		if (cmdList.Count > 0) // �������� ������� ����� ������
			rc = true;
		else // ������ ������
		_error = String.Format($"� ��������� ������ ����� ������� " +
		$"������� {leftCommand.CommandName} �/��� ������� {rightCommand.CommandName} .");
	return rc;
	}
}
