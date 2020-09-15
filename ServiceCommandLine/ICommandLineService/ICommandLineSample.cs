using System;
using System.Collections.Generic;

public interface ICommandLineSample : ICommandLine
 {
    /// <summary>
    /// ������ ������
    /// </summary>
    /// <param name="properties">�������� ��������</param>
    /// <param name="ParametersCmd">��������� ��������</param>
    /// <returns>��������� �������</returns>
    bool ParseCommandLine(Dictionary<string, string> properties,
            List<string> ParametersCmd);
    /// <summary>
    /// ��������������� �� ������� ������ ������ ������
    /// </summary>
    /// <returns>True - ������� ������ ������ ������ �������������</returns>
    bool IsSatisfiedBy();

    /// <summary>
    /// ���������� - ������� ������ ������ ������ �� ���������.
    /// </summary>
    void SetCommandLineBad();

 }
