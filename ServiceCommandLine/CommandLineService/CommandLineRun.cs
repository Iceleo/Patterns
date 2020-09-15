using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Patterns.SpecificationClassic;
// using UserServices.ICommandLineService;
// using UserServices.SpecificationCommandLine;
// using UserServices.UserAttributedLib;


public abstract partial class CommandLineRun : ICommandLineRun
{

    /// <summary>
    /// ��� ���������� 
    /// </summary>
    public virtual string AppName { get; set; }

    [DataMember]
    [Required(ErrorMessage = "Please enter a ame CommandLine")]
    [DisplayName("Name CommandLine")]
    [StringLength(128, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
    /// <summary>
    /// ��� ��������
    /// </summary>
    public virtual string CommandName { get; set; }

    /// <summary>
    /// ��������� ��������
    /// </summary>
    public Dictionary<string, string> Parameters { get; protected set; } = new Dictionary<string, string>();

    /// <summary>
    /// �������� �������� �� ��������� � ���������  //��������� ��������
    /// </summary>
    public virtual Dictionary<string, string> PropertiesNotFound { get; set; }

    public  CommandLineRun ()
	{
		PropertiesNotFound = new Dictionary<string, string>();
 	        AppName =  Environment.GetCommandLineArgs()[0]+ ".exe";
            	AppName = Path.GetFileName(AppName);
	}
    /// <summary>
    /// ��������� 
    /// </summary>
    public abstract void Run();

    /// <summary>
    /// �������
    /// </summary>
    public virtual void Help()
    {
        Console.WriteLine("The syntax for help: {0} /{1} CommandName", AppName, CommandName);
    }

    /// <summary>
    /// ������� ������
    /// </summary>
    public abstract void RulesOfchallenge();

    /// <summary>
    /// ��������������� �� ������� ������ ������ ������
    /// </summary>
    /// <returns>True - ������� ������ ������ ������ �������������</returns>
    public virtual bool IsSatisfiedBy(Dictionary<string, string> _propertiesNotFound)
    {
        bool rc = true;
        if (_mainSpecification == null) // �� ���������
            BuildMainSpecification();
        // �������� ������� ������ ������ ������
        if (_mainSpecification != null) //
            rc = _mainSpecification.IsSatisfiedBy(this);
        // ���. �������� ������� ��� ������ ������
        rc = (MakeAdditionalCheck() && rc) ? true : false;
        return rc;
    }

    /// <summary>
    /// C������ ���. ��������
    /// </summary>
    /// <returns>True - ������� ������ ������ ������ �������������</returns>/// 
    public virtual bool MakeAdditionalCheck() => true;
    

    #region MainSpecification

    /// <summary>
    /// ������� ������������ �������� ������� ������
    /// </summary>
    protected ISpecification<ICommandLineRun> _mainSpecification;
    public ISpecification<ICommandLineRun> GetMainSpecification =>_mainSpecification;
    /// <summary>
    /// ��������� ������� ������������ �������� ������� ������
    /// </summary>
    protected virtual void BuildMainSpecification() { }

    #endregion MainSpecification

}


