using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Digital_Patterns.SpecificationClassic;

/// <summary>
/// ���������� � ��� ������  ���������� �� ��������� ������. 
/// ��� ���������� ������ ������ ����������� ��� ������ 
/// </summary>
public class SpecificationCommandLineAnd : SpecificationCommandLine<List<CommandLineSample>>
//SpecificationExpression<CommandLineSample>
{
    public SpecificationCommandLineAnd(CommandLineSample _leftCommand, CommandLineSample _rightCommand) 
		: base (_leftCommand, _rightCommand, "&") { }
    public override bool IsSatisfiedBy(List<CommandLineSample> cmdList)
    { //
        bool rc = false;
		if (cmdList.Contains(leftCommand))
			if (cmdList.Contains(rightCommand)) {
				rc = true;
			}
			else { //leftCommand ������ ���������
				_error = String.Format($"������� {leftCommand.CommandName} " +
					$"�� ����� ����������� ��� ������� {rightCommand.CommandName}.");
				leftCommand.SetCommandLineBad();
			}
		else if (cmdList.Contains(rightCommand)) 
		{ //rightCommand ������ ���������
				rightCommand.SetCommandLineBad();
				_error = String.Format($"������� {rightCommand.CommandName} " +
				$"�� ����� ����������� ��� {leftCommand.CommandName}.");
		}
		else // �� ������������ ���. 
			if (cmdList.Count > 0) // �������� ������� ����� ������
				rc = true;
			else // ������ ������
				_error = String.Format($"� ��������� ������ ����� ������� " +
				$"������� {leftCommand.CommandName} � ������� {rightCommand.CommandName} .");

		return	rc;
	//(cmd.leftCommand.IscommandClass & rightCommand.IscommandClass);
    }
}
