using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Patterns.SpecificationClassic;
//using UserServices.ICommandLineService;

/// <summary>
/// ���������� ���������� ��� ��� ������  ���������� �� ��������� ������. 
/// ��� ���������� ����������� ������ ��� ������ �� �����
/// </summary>
public class SpecificationCommandLineXor : SpecificationCommandLine<List<ICommandLineSample>>
//SpecificationExpression<CommandLineSample>
{
    public SpecificationCommandLineXor(ICommandLineSample _leftCommand, ICommandLineSample _rightCommand)
		: base (_leftCommand, _rightCommand, "^") { }
/* 
    public override Expression<Func<CommandLineSample, bool>> ToExpression()
    {
        return movie => movie.MpaaRating <= _rating;
    }
*/
    public override bool IsSatisfiedBy(List<ICommandLineSample>  cmdList)
	{ //
	//return (cmd.Contains(leftCommand)) ^ (cmd.Contains(rightCommand));
        bool rc = false;
		if (cmdList.Contains(leftCommand))
			if (!cmdList.Contains(rightCommand)) //leftCommand ���������
				rc = true;	
			else { 
				_error = String.Format($"������� {leftCommand.CommandName} " +
					$"�� ����� ����������� � �������� {rightCommand.CommandName}.");
				rightCommand.SetCommandLineBad(); //rightCommand ������ ���������
	       		//leftCommand.SetCommandLineBad(); //leftCommand ������ ���������
		   }
		else //
			if (cmdList.Contains(rightCommand))  //rightCommand ���������
				rc = true;	
			else // �� ������������ ���. 
				if (cmdList.Count > 0) // �������� ������� ����� ������
					rc = true;
				else // ������ ������
				_error = String.Format($"� ��������� ������ ����� ������� " +
				$"������� {leftCommand.CommandName} ��� ������� {rightCommand.CommandName} .");

		return	rc;
	}
}
