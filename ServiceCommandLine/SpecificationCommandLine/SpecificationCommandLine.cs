using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Patterns.SpecificationClassic;
//using UserServices.ICommandLineService;

/// <summary>
///  ������������ ������� ��������� ��������� ������ ������ ����������
/// </summary>
public abstract class SpecificationCommandLine<T> : SpecificationClassic<T>
//SpecificationExpression<CommandLineSample>
{
	/// <summary>
	/// ����� ���������� ������������ 
	/// </summary>
	protected readonly ICommandLineSample leftCommand ;
	/// <summary>
	/// ������ ���������� ������������ 
	/// </summary>
	protected readonly ICommandLineSample rightCommand;

	/// <summary>
	/// ������ �������� ������������
	/// </summary>
	protected string _error;
	public string GetError()=>  _error;

	/// <summary>
	/// ����������� ������������ ������� ���������
	/// </summary>
	String operation;
	public SpecificationCommandLine(ICommandLineSample _leftCommand, 
		ICommandLineSample _rightCommand, string _operation )
    {
		leftCommand  = _leftCommand ;
		rightCommand = _rightCommand;
		operation = _operation;
	}
	
	//
	public override String ToString() => $"SpecificationCommandLine /{leftCommand.CommandName} " +
		$"{operation} /{rightCommand.CommandName}.";
}
