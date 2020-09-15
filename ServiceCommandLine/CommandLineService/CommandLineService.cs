using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
using System.IO;
//using System.Threading.Tasks;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using Patterns.SpecificationClassic;
//using UserServices.ICommandLineService;
//using UserServices.SpecificationCommandLine;
//using UserServices.UserAttributedLib;

/// <summary>
/// ������ ������� �������  ��������� ������: ������, �����, ����������. 
/// ������ ����������� ������ ���������� �� ��������� ������.
/// ������� ������������ ������������� ����������� ��������, 
/// ������� ������������� ��������� �������������, ����������� ��� ����������� �������.
/// </summary>
public static class CommandLineService //: INotifyDataErrorInfo
{
    /// <summary>
    ///  ������� ��������� ��������� ������
    /// </summary>
    public const char const_PREF = '-';
    public const char sleshBack = '\\';
    /// <summary>
    ///  ������� ������� ��������� ������
    /// </summary>
    public const char slesh2 = '/';
    /// <summary>
    /// ����������� �������� ��������� ��������� ������ 
    /// </summary>
    public const char const_SPLIT = '='; //

    /// <summary>
    /// ������� ������ ������ ����������
    /// </summary>
    public static List<CommandLineSample> cmdlines = new List<CommandLineSample>();

    /// <summary>
    /// ������ ������������ ���������� �� ������ ������ ������� ��������� ������.
    /// </summary>
    public static List<SpecificationCommandLine<List<ICommandLineSample>>> ListSpecificationCommandLine = 
        new List<SpecificationCommandLine<List<ICommandLineSample>>>();

    /// <summary>
    /// �������� ������� ������ ���������� �� ��������� ������
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static CommandLineSample WithCommand(string name)
    {
    CommandLineSample cmdln = new CommandLineSample(name.Substring(1).TrimEnd());
        cmdlines.Add(cmdln);
        return cmdln;
    }

    /// <summary>
    ///  ������ ������. ������ ����������� ������ ���������� �� ��������� ������.
    ///  ��������� � ������ ���������� ������ � ��������� ������.
    /// </summary>
    /// <param name="args"></param>
    /// <param name="result">����� ������������ ������ �������, � �������� ��������</param>
    /// <returns>��������� �������</returns>
    public static bool ParseCommandLine(string[] args) //, out ICommandLineRun result)
    {
        bool rc = false;
        //result = null;        
        for (int ind = 0; ind < args.Length; ind++)// ������ ���� ����������
        {
            Dictionary<string, string> properties = new Dictionary<string, string>();
            List<string> Parameters = new List<string>();
            CommandLineSample @this = null;
            if (args[ind].StartsWith(slesh2)) 
            {// �������
                args[ind] = args[ind].Substring(1); // ��� �������
                @this = cmdlines.FirstOrDefault((s => s.CommandName == args[ind]));

                if (@this == null)  // �� ���� �������
                { //  TODO ��������� ������ � ��������� ������
                    continue;
                }
                //ICommandLineRun resultLast = null;
                ind++; // ����. ��������
                //int ParameterIndex = 0;
                bool IsParameter = false; //

                while( ind < args.Length) // ������ ����� � ���������� ��� �������
                {
                    if (args[ind].StartsWith(const_PREF)) // �������
                    { // �����
                        string parameterWithoutHyphen = args[ind].Substring(1); //��� �����������
                                                                                // ����������� �������� ��������� ��������� ������ 
                                                                                // �������� ������������ �����
                        string[] nameValue = args[ind].Substring(1).Split(const_SPLIT); // ����������� �������� ��������� ��������� ������ 
                        if (nameValue.Length == 0)
                        { // ������������ �����
                            break; //??? return rc;
                        }
                        string ValueOpt;
                        switch (nameValue.Length)
                        {
                            case 1: // �������� ������������ �����
                                ValueOpt = null;
                                break;
                            case 2:  // ������ ���������� �����
                                ValueOpt = nameValue[1];
                                break;
                            default:  // ������ ������������ �����. �� ����� ������ 2
                                ValueOpt = nameValue[1];
                                break;
                        }
                        properties.Add(nameValue[0], ValueOpt);
                        ind++; // ����. ��������
                    }
                    else if (args[ind].StartsWith(slesh2))
                    { // ��� �������.  
                        break; // �������. ���������� � ����� for
                    }
                    else
                    { // ��������������� �����. ������ ����������� �����. � ����� ������ ��� Help
                        //if (!IsParameter) { // ���� ��������� 1 ��������
                        IsParameter = true;
                        Parameters.Add(args[ind]);
                        ind++; // ����. ��������
                        break; // ��������� �������
                    }
                }
                // TODO �������� �� �������������
                // ���������� ������ � �������(CommandLineSample). ������� ����������� �����
                rc = @this.ParseCommandLine(properties, Parameters);//������� ����������� �����
                properties.Clear();
                Parameters.Clear();
            }
        }
        return rc;
    }

    /// <summary>
    /// ����� ������ ���������� ���������� ��� ���������� ������ 
    /// ��������������� ������� ������ ������ �� ������������� ������������� 
    /// ����������� ������, ������������ ��������� �������������, ����������� ��� ����������� �������
    /// </summary>
    public static void Run() { // ???
        var cmdRun = CommandLineService.cmdlines.Where((cm1) => 
                (cm1.IscommandClass && cm1.CommandLineOK) ).ToList();
        foreach (ICommandLineSample cmdrun in cmdRun) //
             cmdrun?.Run();
    }
    /// <summary>
    /// ������� ������ ���������� 
    /// </summary>
    public static void RulesOfchallenge() 
    {
        if (cmdlines.Count == 0)
            Rulecall();
        else //
            foreach (CommandLineSample cmdln in cmdlines) //
                cmdln?.RulesOfchallenge();
    }

    /// <summary>
    /// ������� ������ ���������� 
    /// </summary>
    public static void Help()
    {
        if (cmdlines.Count == 0)
            Rulecall();
        else //
            foreach (CommandLineSample cmdln in cmdlines) //
            cmdln?.Help();
    }
    private static void Rulecall()
    {
        string AppName = Environment.GetCommandLineArgs()[0];
        AppName = Path.GetFileName(AppName);
        // � ����� ��������
        Console.WriteLine("Error calling program {0}.", AppName);
        Console.WriteLine("The syntax for help: {0} /help CommandName", AppName);
    }

    /// <summary>
    /// ��������������� �� ������� ������ ������ ������
    /// ��������� �� ������������� ������������� ����������� ������, 
    /// ������������ ��������� �������������, ����������� ��� ����������� �������.
    /// </summary>
    /// <returns>True - ������� ������ ������ ������ �������������</returns>
    public static bool IsSatisfiedByService()
    {
        bool rc = true;
        // ��������� ������ ���������� ��� ���������� ������
        List<ICommandLineSample> cmds = CommandLineService.cmdlines.Where(
            cm1 => (cm1.IscommandClass & cm1.CommandLineOK)).
            Select(cm2=>(ICommandLineSample)cm2).ToList(); //&& cm1.CommandLineOk
                                             
        //CommandLineService.ListSpecificationCommandLine.IsSatisfiedBy(cmd);
        foreach (SpecificationCommandLine<List<ICommandLineSample>> sp in ListSpecificationCommandLine)
            if (!sp.IsSatisfiedBy(cmds)) // �� ������ ��������
            {
                _errors.Add( sp.ToString() , sp.GetError());
                rc = false;
            }

        // �������� �������� ����� � ����������
        foreach (CommandLineSample cmd in cmds)
        {
            if (!cmd.IsSatisfiedBy()) // �� ������ ��������
                rc = false;
        }

        //cmds.Select(c => c.IsSatisfiedBy() ); ���������� ����������
        return rc;
    }

    /// <summary>
    /// ������������� ��-��� �������� ��������� ������� ICommandLineRun
    /// </summary>
    /// <param name="pp"> ��-�� �������� ������������� ��-���</param>
    /// <param name="ObjectImp">������, ������� ���������</param>
    /// <param name="st2">��������������� ��-���</param>
    static public void SetValue<ICommandLineRun>(PropertyInfo pp, ICommandLineRun ObjectImp,
        string st2, out string error)
    {
        error = string.Empty;
        try
        {
            Type typrFld = pp.PropertyType;
            Type[] typrFld2 = pp.GetOptionalCustomModifiers();
            if (typrFld == Type.GetType("string")) // if (typrFld.ToString().ToLower() == "string")
            {
                pp.SetValue(ObjectImp, st2);
            }
            else if (typrFld == Type.GetType("int"))
            {
                if (int.TryParse(st2, out int iRes))
                    pp.SetValue(ObjectImp, iRes);
                else //
                    error = "������������ ������ ������.";
            }
            else if (typrFld == Type.GetType("double"))
            {
                if (double.TryParse(st2, out double dRes))
                    pp.SetValue(ObjectImp, dRes);
                else //
                    error = "������������ ������ ������.";
            }
            else if (typrFld == Type.GetType("bool"))
            {
                if (bool.TryParse(st2, out bool bRes))
                    pp.SetValue(ObjectImp, bRes);
                else //
                    error = "������������ ������ ������.";
            }
            else // 
                error = "���������������� ������ ������.";
        }
        catch (System.ArgumentException exp)
        // �� ������ ����� ������� set ��������. -���- value ���������� ������������� �
        // ��� System.Reflection.PropertyInfo.PropertyType.
        {
            error = exp.Message;
        }
        catch (System.Reflection.TargetException exp)
        //  ��� obj �� ������������� �������� ����, ��� �������� �������� ��������� ����������, �� obj ����� null.
        {
            error = exp.Message;
        }
        catch (System.MethodAccessException exp)
        //     �������� ������������ ������� ������� � �������� ��� ����������� ������ ������  ������.
        {
            error = exp.Message;
        }
        catch (System.Reflection.TargetInvocationException exp)
        //     ������ ��� ��������� �������� ��������.
        {
            error = exp.Message;
        }
    }


    #region INotifyDataErrorInfo
    /// <summary>
    /// ������ �������� ��� ����� CommandLineRun.
    /// ��������� ��������, ������ ������
    /// </summary>
    static readonly Dictionary<string, string> _errors = new Dictionary<string, string>();
    /// <summary>
    /// ������ �������� ��� ����� CommandLineRun.
    /// ��������� ��������, ������ ������
    /// </summary>
    public static Dictionary<string, string> Errors => _errors;
    /// <summary>
    /// ����� �� �������� ������ ��������.
    /// </summary>
    public static bool HasErrors => _errors.Count > 0;

    /// <summary>
    /// ������ �������� ��� ���������� �������� ��� ��� ���� ��������
    /// </summary>
    /// <param name="propName"></param>
    /// <returns></returns>
    public static Dictionary<string, string> GetErrors() => _errors;
    /// <summary>
    /// ������ �������� ��� ���������� �������� ��� ��� ���� ��������
    /// </summary>
    /// <param name="propName"></param>
    /// <returns></returns>
    public static IList<string> GetErrors(string propName)
    {
        IList<string> Er;
        if (string.IsNullOrEmpty(propName))
        {
            Er = _errors.Values.ToList();
        }
        else //
        {
            Er = _errors.ContainsKey(propName) ? new List<string> { _errors[propName] } : null;
        }
        return Er;
    }

    /// <summary>
    /// �������� ������ ��� ��������
    /// </summary>
    /// <param name="propName">��� ��������.</param>
    /// <param name="error">���������� ������.</param>
    public static void AddError(string propName, string error)
    {
        _errors.Add(propName,  error);
    }

    #endregion INotifyDataErrorInfo
}