using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Digital_Patterns.SpecificationClassic;

/// <summary>
/// ���������� �/��� ��� ������  ���������� �� ��������� ������. 
/// ��� ���������� ����������� ������ ��� ������ �� �����
/// </summary>
public class SpecificationCommandLineOr : SpecificationCommandLine<List<CommandLineSample>>
//SpecificationExpression<List<CommandLineSample>>
{
 
    public SpecificationCommandLineOr(CommandLineSample _leftCommand, CommandLineSample _rightCommand)
		: base (_leftCommand, _rightCommand, "|") { }
   public override bool IsSatisfiedBy(List<CommandLineSample> cmdList)
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
