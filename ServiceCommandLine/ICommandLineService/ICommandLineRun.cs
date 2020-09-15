using System;
using System.Collections.Generic;
using System.ComponentModel;

public interface ICommandLineRun : ICommandLine, INotifyDataErrorInfo
 {
    /// <summary>
    /// ��� ���������� 
    /// </summary>
    string AppName { get; set; }
    /// <summary>
    /// �������� �������� �� ��������� � ���������  //��������� ��������
    /// </summary>
    Dictionary<string, string> PropertiesNotFound { get; set; }

    /// <summary>
    /// ��������������� �� ������� ������ ������ ������
    /// </summary>
    /// <returns>True - ������� ������ ������ ������ �������������</returns>
    bool IsSatisfiedBy(Dictionary<string, string> PropertiesNotFound);

        /// <summary>
        /// �������� ������ ��� ��������
        /// </summary>
        /// <param name="propName">��� ��������.</param>
        /// <param name="error">���������� ������.</param>
    void AddError(string propName, string error);

    /// <summary>
    /// �������� ������ ��� ��������
    /// </summary>
    /// <param name="propName">��� ��������.</param>
    /// <param name="errorList">���������� ������.</param>
    void AddListErrors(string propName, List<string> errorList);

}
