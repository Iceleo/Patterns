using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public abstract partial class CommandLineRun : ICommandLineRun//, System.ComponentModel.INotifyDataErrorInfo
{
    #region INotifyDataErrorInfo
        /// <summary>
        /// ������ �������� ��� ����� CommandLineRun.
        /// ��������� ��������, ������ ������
        /// </summary>
        public Dictionary<string, List<string>> Errors => _errors;
        /// <summary>
        /// ����� �� �������� ������ ��������.
        /// </summary>
        public virtual bool HasErrors => _errors.Count > 0;

        /// <summary>
        /// ������ �������� ��� ���������� �������� ��� ��� ���� ��������
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        public virtual IEnumerable GetErrors(string propName)
        {
            GetErrorsStart( this);
        // return null;
            IEnumerable Er;
            if (string.IsNullOrEmpty(propName))
            {
                Er = _errors.Values;
            }
            else //
            {
                Er = _errors.ContainsKey(propName) ? _errors[propName] : null;
            }
            GetErrorsEnd( Er, this);
            return Er;            
        }

        /// <summary>
        /// C������ ��������
        /// </summary>
        /// <param name="IsClear"> ����� �������������� �������� c����� ������ ��������</param>
        public virtual void MakeAdditionalCheck(bool IsClear = true)
        {
            if (IsClear) // ������ ������
                this.ClearErrors( true, null);
            AddListErrors(nameof(CommandName), GetErrorsFromAnnotations(nameof(CommandName), this.CommandName));
        }

        /// <summary>
        /// �������� ������ ��� ��������
        /// </summary>
        /// <param name="propName">��� ��������.</param>
        /// <param name="error">���������� ������.</param>
        public void AddError(string propName, string error)
        {
            AddListErrors(propName, new List<string> { error });
        }


    #region protected 
        /// <summary>
        /// ������ �������� ��� ����� CommandLineRun.
        /// ��������� ��������, ������ ������
        /// </summary>
        protected readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propName"></param>
        protected void OnErrorsChanged(string propName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propName));
        }

        /// <summary>
        /// �������� ������������� �� ������ ��������� ������ 
        /// ������ �������� ��������
        /// </summary>
        /// <typeparam name="T"> ��� ������������ �������� </typeparam>
        /// <param name="propName">��� ��������.</param>
        /// <param name="value">����������� ��������</param>
        /// <returns>���������� ������ �������� �������� ��������.</returns>
        protected List<string> GetErrorsFromAnnotations<TEntity>(string propName, TEntity value)
        {
            var results = new List<ValidationResult>(); //����������� � ��������� �������. 
            var vc = new ValidationContext(this, null, null) { MemberName = propName };
	// Validator ��������� ���������, ���� �� � ������� ������, ��������� � ����������� ������, � ValidationContext. 
            var isValid = Validator.TryValidateProperty(value, vc, results);
            return (isValid) ? null : results.ConvertAll( o => o.ErrorMessage);
        }
        /// <summary>
        /// �������� ������ ��� ��������
        /// </summary>
        /// <param name="propName">��� ��������.</param>
        /// <param name="errors">���������� ������.</param>
        /// AddListError
        public void AddListErrors(string propName, List<string> errors)
        {
            if (errors?.Count > 0  && !string.IsNullOrEmpty( propName))
            {
                bool changed = false;
                if (!_errors.ContainsKey(propName))
                {// �� ������������
                    _errors.Add(propName, new List<string>());
                    changed = true;
                }
                var erlist = _errors[propName];
                foreach (var err in errors)
                {
                    if (erlist.Contains(err)) // ��� ������������
                        continue;
                    erlist.Add(err);
                    changed = true;
                }
                if (changed)
                {
                    OnErrorsChanged(propName);
                }
            }
        }

        protected void ClearErrors(bool IsEvent = false, string propName = "")
        {
            if (String.IsNullOrEmpty(propName))
            {
                _errors.Clear();
            }
            else
            {
                    _errors.Remove(propName);
            }
            if (IsEvent) //
                OnErrorsChanged(propName);
        }
    #endregion protected 


    /// <summary>
    /// ������� ������, �������������� �������, ����� ������� ������������� ������.
    /// DataErrorsChangedEventArgs ������������� ������ ��� ������� System.ComponentModel.INotifyDataErrorInfo.ErrorsChanged.
    /// </summary>
    public event EventHandler<System.ComponentModel.DataErrorsChangedEventArgs> ErrorsChanged;
     //   System.ComponentModel.INotifyDataErrorInfo ErrorsChanged1;


        #region partial ��������� ������
        /// <summary>
        /// ��������� �����. ��������� ������� GetErrors.
        /// </summary>
        /// <param name="countS"> ���������� ��� ������</param>
        partial void GetErrorsStart(CommandLineRun countE);
        /// <summary>
        /// ��������� �����. �������� ������� GetErrors.
        /// </summary>
        /// <param name="_t"></param>
        /// <param name="countS"> ���������� ��� ������</param>
        partial void GetErrorsEnd(IEnumerable _t, CommandLineRun countE);

        #endregion partial

    #endregion INotifyDataErrorInfo
}


