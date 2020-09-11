using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
using System.Reflection;
//using System.Threading.Tasks;

//    ������ ��������� ������ ������ ����������

/// <summary>
/// ������ ���������� ������ ���������� �� ��������� ������
/// ������� ������������ ������������� ����������� ��������, 
/// ������� ������������� ��������� ����������� ��� ����������� �������. 
/// </summary>
public class CommandLineSample : ICommandLineSample
{ // 
    /// <summary>
    /// ������ ��� ������������ ����� ������������ ������� ������.
    /// � �������� �elp - ����� �elpCommandLine
    /// </summary>
    public const string constSufficsClass = "CommandLine";

    public CommandLineSample(string commandName)
    {
        CommandName = commandName;
    }
    /// <summary>
    /// ��������� 
    /// </summary>
    public virtual void Run() => commandClass?.Run(); 

    /// <summary>
    /// �������
    /// </summary>
    public virtual void Help() =>commandClass?.Help();

    /// <summary>
    /// �������� ������� ������ ����������
    /// </summary>
    public virtual void RulesOfchallenge() =>commandClass?.RulesOfchallenge();


    /// <summary>
    /// ��������� �� ������� ������ ������ ������.
    /// True - ���������.
    /// </summary>
    public bool CommandLineOK { get; protected set; } = true;

    /// <summary>
    /// ���������� - ������� ������ ������ ������ �� ���������.
    /// </summary>
    public void SetCommandLineBad() => CommandLineOK = false;

    /// <summary>
    /// ��� �����������
    /// </summary>
    public string CommandName { get; set; } // => _commandName;

    /// <summary>
    /// ��� ������������ ������� ������ ������ � ������ ������������
    /// </summary>
    protected string NameClassRun { get; set; }

    /// <summary>
    /// ��� namespace ��� commandClass( ������ ���������� ������ �������). ���� �����
    /// </summary>
    //public string namespaceClass;

    /// <summary>
    /// ����� ���������� ������ ����������.
    /// </summary>
    protected ICommandLineRun commandClass { get; set; }

    /// <summary>
    /// True - ����� ���������� ������ ���������� ������.
    /// </summary>
    public bool IscommandClass => commandClass != null;
    
    /// <summary>
    /// ��������� �����������
    /// </summary>
    public Dictionary<string, string> Parameters { get; protected set; } = new Dictionary<string, string>();

    /// <summary>
    /// ������������ ����� �����������
    /// </summary>
    public Dictionary<string, string> MandatoryOptions { get; protected set; } = new Dictionary<string, string>();

    /// <summary>
    /// �� ������������ ����� �����������
    /// </summary>
    public Dictionary<string, string> OptionalOptions { get; protected set; } = new Dictionary<string, string>();
    /// <summary>
    /// ������� ������������ ����� �����������
    /// </summary>
    /// <param name="name">��� �����</param>
    /// <returns>��� �����</returns>
    public CommandLineSample MandatoryOption(string name)
    {
        MandatoryOptions.Add(name.Substring(1).TrimEnd() , null);
        return this;
    }

    /// <summary>
    /// ������� �� ������������ ����� �����������
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public CommandLineSample OptionalOption(string name)
    {
        OptionalOptions.Add(name.Substring(1).TrimEnd(), null);
        return this;
    }
    /// <summary>
    /// ������� �������� �����������
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public CommandLineSample ParameterCmdLine(string name)
    {
        Parameters.Add(name.Trim(), null);
        return this;
    }
    /// <summary>
    ///  ��� ������������ ���������� ������ ������ � ������ ������������
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public CommandLineSample PerformingClass(string name)
    //public CommandLineSample Withnamespace(string name)
    {
        this.NameClassRun = name; 
        return this;
    }

    /// <summary>
    /// ����� �������� �� ��������� � ���������  //��������� ��������
    /// </summary>
    public virtual Dictionary<string, string> PropertiesParamNotFound { get; set; }

    /// <summary>
    /// ������ ������
    /// </summary>
    /// <param name="properties">����� ��������</param>
    /// <param name="ParametersCmd">��������� ��������</param>
    /// <param name="result">����� ���������� ������ �������</param>
    /// <returns>��������� �������</returns>
    public bool ParseCommandLine( Dictionary<string, string> properties,
            List<string> ParametersCmd)
    {
        bool rc = false;
        ICommandLineRun result;
        PropertiesParamNotFound = new Dictionary<string, string>();
        foreach (var property in properties)
        {
            string nameValue = property.Key;
            string ValueOpt = property.Value;
            if (this.MandatoryOptions.TryGetValue(nameValue, out string val1))
                this.MandatoryOptions[nameValue] = ValueOpt;
            else if (this.OptionalOptions.TryGetValue(nameValue, out string val2))
                this.OptionalOptions[nameValue] = ValueOpt;
            else // ������� ���
                PropertiesParamNotFound.Add( nameValue, ValueOpt);
        }

        foreach (string nameParam in ParametersCmd)
        {
            if (this.Parameters.TryGetValue(nameParam, out string val3))
                this.Parameters[nameParam] = string.Empty;
            else // ������� ���
                  PropertiesParamNotFound.Add(nameParam, string.Empty);
        }
        try
        {
            string classNameCmd = string.Empty;
            if ( string.IsNullOrEmpty(this.NameClassRun))
            {
                //��� ������������ ������� ������ ������ � ������ ������������
                // ��� ������� � �������� �elp - ����� �elpCommandLine:
                // ��� ������� � �������� parse - ����� parseCommandLine:
                // ������ ������������ ���������
                classNameCmd += this.GetType().Namespace + '.' 
                    +this.CommandName + constSufficsClass;                
            }
            else //
                classNameCmd = this.NameClassRun;
            
            Type typeclass = Type.GetType(classNameCmd); // 
            // � �������� ��� ���������:  
            result = (ICommandLineRun)Activator.CreateInstance(typeclass);            
            this.commandClass = result;
            result.CommandName = this.CommandName; // ����� ���������
            // �������� - ����� ��� ���������� ���������:
            foreach (var property in properties)
                if (!string.IsNullOrEmpty(property.Key))
                { //
                    PropertyInfo pp = typeclass.GetProperty(property.Key);
                    if (pp != null) //
                    {
                        CommandLineService.SetValue(pp, result, property.Value, out string error); // �������� �������� � ��������
                        pp.SetValue(result, property.Value); // �������� ���������� ����� � ��������
                    }    
                }

            // �������� - ��������� ��� ���������� ���������:
            foreach (string nameParam in ParametersCmd) //
                if ( !string.IsNullOrEmpty(nameParam)) //
                    typeclass.GetProperty(nameParam)?.SetValue(result, true);

            rc = true;
        }
        catch (System.ArgumentException exc1)
        { //     �������� type �� �������� �������� ���� RuntimeType. -���- �������� ���������
          //     type �������� �������� ������������� ����� (����� �������, �������� 
        }
        catch (System.NotSupportedException exc2)
        {//     ���, �������� ���������� type, �� ����� ���� System.Reflection.Emit.TypeBuilder.
         //     -���- �������� ����� System.TypedReference, System.ArgIterator, System.Void
         //     � System.RuntimeArgumentHandle ��� �������� ���� ����� �� ��������������.
         //     -���- ������, ���������� ��� type, �������� ������������ �������, ���������
         //     � ������� System.Reflection.Emit.AssemblyBuilderAccess.Save.
        }
        catch (System.Reflection.TargetInvocationException exc3)
        { //     ���������� ����������� ������� ����������.
        }
        catch (System.MethodAccessException exc4)
        {//     ���������� ��� �� ����� ���������� �� ����� ����� ������������.
        }
        catch (System.MemberAccessException exc5)
        { //     �� ������� ������� ��������� ������������ ������, ��� ���� ���� ��� ������
          //     ��� ������ ��������� ������� ��������.
        }
        catch (System.Runtime.InteropServices.InvalidComObjectException exc6)
        { //     COM-��� �� ��� ������� ����������� Overload:System.Type.GetTypeFromCLSID
          //     ��� Overload:System.Type.GetTypeFromProgID.
        }
        catch (System.Runtime.InteropServices.COMException exc8)
        { //     �������� type ������������ COM-������, �� ������������� ������, ������������
          //     ��� ��������� ����, �������� ������������, ��� ���������������� ����� ��
          //     ���������������.
        }
        return rc;
    }

    /// <summary>
    /// ��������������� �� ������� ������ ������ ����������
    /// </summary>
    /// <returns>True - ������� ������ ������ ������ �������������</returns>
    public bool IsSatisfiedBy()
    {
        bool rc = true;
        // d
        foreach (var property in MandatoryOptions.Where( val => val.Value == null))
        {// ������������ �� ��������� ��� ������
            ;//if 
        }
        rc = (bool) commandClass?.IsSatisfiedBy( PropertiesParamNotFound);
        return rc;
    }

  #region static
    /// <summary>
    /// ������� ������������ XOR ������������� ����������� ��������, 
    /// ������� ������������� ��������� ����������� ��� ����������� �������. 
    /// </summary>
    /// <param name="leftCommand"></param>
    /// <param name="rightCommand"></param>
    /// <returns></returns>
    public static CommandLineSample operator |(CommandLineSample leftCommand, CommandLineSample rightCommand)
    {
        // ������� ������������ ��� �������� ������������� ������ XOR
        SpecificationCommandLineOr ss = new SpecificationCommandLineOr(leftCommand, rightCommand);
        CommandLineService.ListSpecificationCommandLine.Add(ss);        
        return leftCommand; // rigthCommand;
    }

    /// <summary>
    /// ������� ������������ OR ������������� ����������� ��������, 
    /// ������� ������������� ��������� ����������� ��� ����������� �������. 
    /// </summary>
    /// <param name="leftCommand"></param>
    /// <param name="rigthCommand"></param>
    /// <returns></returns>
    public static CommandLineSample operator ^ (CommandLineSample leftCommand, CommandLineSample rightCommand)
    {
        // ������� ������������ ��� �������� ������������� ������ XOR
        SpecificationCommandLineXor ss = new SpecificationCommandLineXor(leftCommand, rightCommand);
        CommandLineService.ListSpecificationCommandLine.Add(ss);
        return leftCommand; // rigthCommand;
    }

    /// <summary>
    /// ������� ������������ And ������������� ����������� ��������, 
    /// ������� ������������� ��������� ����������� ��� ����������� �������. 
    /// </summary>
    /// <param name="leftCommand"></param>
    /// <param name="rightCommand"></param>
    /// <returns></returns>
    public static CommandLineSample operator & (CommandLineSample leftCommand, CommandLineSample rightCommand)
    {
        // ������� ������������ ��� �������� ������������� ������ XOR
        SpecificationCommandLineAnd ss = new SpecificationCommandLineAnd(leftCommand, rightCommand);
        CommandLineService.ListSpecificationCommandLine.Add(ss);
        return leftCommand; // rigthCommand;
    }

    #endregion static
}