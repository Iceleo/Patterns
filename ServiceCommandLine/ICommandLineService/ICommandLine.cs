using System;

 public interface ICommandLine
 {
    /// <summary>
    /// ��������� 
    /// </summary>
    void Run();

    /// <summary>
    /// �������
    /// </summary>
    void Help();

    /// <summary>
    /// ������� ������
    /// </summary>
    void RulesOfchallenge();	

    /// <summary>
    /// ��� ��������
    /// </summary>
    string CommandName { get; set;}
 }
