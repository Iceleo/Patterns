using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;
using Patterns.SpecificationClassic;
//using UserServices.ICommandLineService;
//using UserServices.CommandLineService;
//using UserServices.SpecificationCommandLine;
//using UserServices.UserAttributedLib;


    /// <summary>
    /// ����� ������ ����������� ����������. 
    /// ��������� ��������� ������.
    /// ��������� ������ �� ����������������� ����� 
    /// </summary>
    [Serializable, XmlRoot(Namespace = "http://www.MyCompany.com")]
    public class parseCommandLine : CommandLineRun
    {
        // � ��������� ������  " .. parse 
        //            public string Variant { get; set; }          // ����  ��������� ������

        /// <summary>
        /// 
        /// </summary>
        private static parseCommandLine _parseCommand;
        public static parseCommandLine CountCommand { get { return _parseCommand; } } // ������ ��� �������

        [XmlAttribute]
        public override string CommandName { get; set; }
        [XmlAttribute]
        public override string AppName { get; set; }// ��� ���������� 

        /// <summary>
        /// ������� parse
        /// </summary>
        public bool ParseCommand { get; set; }
        [XmlAttribute]
        [SpecificationCommandLineAttribute ("1", "pathconfig")]
        public string pathconfig { get; set; }  //���� � ����������������� �����
        [XmlAttribute]
        public string pathbase { get; set; }    //���� � �������� ����� �������
        [XmlAttribute]
        public string targethost { get; set; }  //������� �����

        // ��������������/����������� ������ ��/� �����.
        /// <summary>
        /// �������� Load Serialize data
        /// </summary>
        public bool LoadData { get; set; } = false;
        /// <summary>
        /// �������� Save Serialize data
        /// </summary
        public bool SaveData { get; set; } = false;
        public string FileSerialize { get; set; }

        // �� ������������
        [XmlAttribute]
        protected string strproject;       // {project} - �������� ���������� ������� 
        [XmlAttribute]
        protected string strhtml_files;    // {html_files} - ���������� ������������ HTML �������
        [XmlAttribute]
        protected string strstatic_files;  // {static_files} - ���������� ������������ ��������� ��������
        [XmlAttribute]
        protected int depthtransitions;   // depthtransitions ������� ��������� �� ���������� ��������  
        protected string _shemas;  // ����� ����������� � ����� 
        /// <summary>
        ///  ������� ��������� �� ���������� ��������.  depthtransitions
        /// </summary>
        public int Depthtransitions { get { return depthtransitions; } } //
        /// <summary>
        /// ����� ������������ ��� �������� ������. �� ������������.
        /// </summary>
        protected string _loginhost;            // ����� ������������ ��� �������� ������
        /// <summary>
        /// ������ ������������ ��� �������� ������. �� ������������.
        /// </summary>
        protected string _passwhost;
        //[NonSerialized]
        //        public static Logger          logger     { get { return _logger;   }}     // �����
        //        private static Logger _logger; // �����
        public parseCommandLine()
        {
            _parseCommand = this;
        }


        /// <summary>
        /// ��������� 
        /// </summary>
        public override void Run()
        {
            Console.WriteLine($"{AppName} ����� ������� /parse -pathbase={pathbase}  -pathconfig={pathconfig} -targethost={targethost} .");
        }

        /// <summary>
        /// �������
        /// </summary>
        public override void Help()
        {
            // � ������ Console
            RulesOfchallenge();
        }
        /// <summary>
        /// ������� ������
        /// </summary>
        public override void RulesOfchallenge()
        {
            //Console.WriteLine(" ��������� ������ ������� parse :");
            //Console.WriteLine("The syntax for calling parse command:");
            Console.WriteLine($"{AppName} /parse -pathbase=pathprogram  -pathconfig=pathlocal -targethost=host ");
            Console.WriteLine("��������, {0} � �������� parse ����� ���������: ", AppName);
            Console.WriteLine(AppName,
               @" /parse -pathbase=C:\work\my_exe\ -pathconfig=C:\work\my_parse\ -targethost=en.wikipedia.org");
            Console.WriteLine("���������  -pathconfig, -targethost ������ ���� ������� �������������.");
            Console.WriteLine("���������� � -pathbase ��� ��������� ���������, ���� ���� ����������.");
        }

        /// <summary>
        /// ��������������� �� ������� ������ ������ ������
        /// </summary>
        /// <returns>True - ������� ������ ������ ������ �������������</returns>
        public virtual bool Initial()
        {
            bool rc = true;
            return rc;
        }
        /// <summary>
        /// ��������� ������� ������������ �������� ������� ������
        /// </summary>
        protected override void BuildMainSpecification()
        {
            this.ClearErrors(); // ������� ������
            // ��������� �������� ������������ ��� ��������
            string nameProp = nameof(pathconfig);
            SpecificationClassic<ICommandLineRun> pathconfigSp = (SpecificationClassic<ICommandLineRun>)  
              (new SpecificationFromAnnotations(nameProp)). //�������� �� ������ ��������� ������
                    And( new SpecificationDirectoryExist(nameProp));//�������� ������������� ����������
            nameProp = nameof(pathbase);
            SpecificationClassic<ICommandLineRun> pathbaseSp = (SpecificationClassic<ICommandLineRun>)
              (new SpecificationFromAnnotations(nameProp)).//�������� �� ������ ��������� ������
                    And(new SpecificationDirectoryExist(nameProp));//�������� ������������� ����������

            //SpecificationClassic<ICommandLineRun> FileSerializeSp;
            if (this.LoadData) //����� ��������� �� �����
            {
                _mainSpecification = new SpecificationFileExist(nameof(FileSerialize));
            }
            else
            {
                _mainSpecification = pathconfigSp.And(pathbaseSp);//
                if (this.SaveData) //����� ���������
                {
                    _mainSpecification = _mainSpecification.
                        And(new SpecificationFileExist(nameof(FileSerialize)));
                }

            }

        }
    }