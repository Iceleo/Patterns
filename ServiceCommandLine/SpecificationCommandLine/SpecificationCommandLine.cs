using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Digital_Patterns.SpecificationClassic;

/// <summary>
///  ������������ ������� ��������� ��������� ������ ������ ����������
/// </summary>
public abstract class SpecificationCommandLine<T> : SpecificationClassic<T>
//SpecificationExpression<CommandLineSample>
{
	/// <summary>
	/// ����� ���������� ������������ 
	/// </summary>
	protected readonly CommandLineSample leftCommand ;
	/// <summary>
	/// ������ ���������� ������������ 
	/// </summary>
	protected readonly CommandLineSample rightCommand;

	/// <summary>
	/// ������ �������� ������������
	/// </summary>
	protected string _error;
	public string GetError()=>  _error;

	/// <summary>
	/// ����������� ������������ ������� ���������
	/// </summary>
	String operation;
	public SpecificationCommandLine(CommandLineSample _leftCommand, 
		CommandLineSample _rightCommand, string _operation )
    {
		leftCommand  = _leftCommand ;
		rightCommand = _rightCommand;
		operation = _operation;
	}
	
	//
	public override String ToString() => $"SpecificationCommandLine /{leftCommand.CommandName} " +
		$"{operation} /{rightCommand.CommandName}.";
}
