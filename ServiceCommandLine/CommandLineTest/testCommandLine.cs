using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;
using Patterns.SpecificationClassic;
using UserServices.ICommandLineService;
using UserServices.CommandLineService;
using UserServices.SpecificationCommandLine;
using UserServices.UserAttributedLib;


    /// <summary>
    /// ����� ������ ����������� ����������. 
    /// ��������� ��������� ������.
    /// ��������� ������ �� ����������������� ����� 
    /// </summary>
    [Serializable, XmlRoot(Namespace = "http://www.MyCompany.com")]
    public class testCommandLine : CommandLineRun
    {
        // � ��������� ������  " .. test 
       
        /// <summary>
        /// 
        /// </summary>
        private static testCommandLine _testCommand;       
        public static testCommandLine countCommand =>_testCommand; // ������ ��� �������

        [XmlAttribute]
        public override string CommandName { get; set; }

        [XmlAttribute]
        public string testconfig { get; set; }  //���� � ����������������� �����
        [XmlAttribute]
        public string testbase { get; set; }    //���� � �������� ����� �������
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
        protected string _shemas;  // ����� ����������� � ����� 
        public testCommandLine()
        {
            _testCommand = this;
        }


        /// <summary>
        /// ��������� 
        /// </summary>
        public override void Run()
        {// � ������ Console
            Console.WriteLine($"{AppName} ����� ������� /test -testbase={testbase}  -testconfig={testconfig} -targethost={targethost} .");
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
        {  // � ������ Console
            //Console.WriteLine(" ��������� ������ ������� test :");
            //Console.WriteLine("The syntax for calling test command:");
            Console.WriteLine($"{AppName} /test -testbase=pathprogram  -testconfig=pathlocal -targethost=host ");
            Console.WriteLine("��������, {0} � �������� test ����� ���������: ", AppName);
            Console.WriteLine(AppName,
               @" /test -testbase=C:\work\my_exe\ -testconfig=C:\work\my_test\ -targethost=en.wikipedia.org");
            Console.WriteLine("���������  -testconfig, -targethost ������ ���� ������� �������������.");
            Console.WriteLine("���������� � -testbase ��� ��������� ���������, ���� ���� ����������.");
        }
  
        /// <summary>
        /// ��������� ������� ������������ �������� ������� ������
        /// </summary>
        protected override void BuildMainSpecification()
        {
            this.ClearErrors(); // ������� ������
            // ��������� �������� ������������ ��� ��������
            string nameProp = nameof(testconfig);
            SpecificationClassic<ICommandLineRun> pathconfigSp = (SpecificationClassic<ICommandLineRun>)
              (new SpecificationFromAnnotations(nameProp)). //�������� �� ������ ��������� ������
                    And(new SpecificationDirectoryExist(nameProp));//�������� ������������� ����������
            nameProp = nameof(testbase);
            SpecificationClassic<ICommandLineRun> pathbaseSp = (SpecificationClassic<ICommandLineRun>)
              (new SpecificationFromAnnotations(nameProp)).//�������� �� ������ ��������� ������
                    And(new SpecificationDirectoryExist(nameProp));//�������� ������������� ����������

            _mainSpecification = pathconfigSp.And(pathbaseSp);//
            if (this.LoadData && this.SaveData) //����� ���������/��������� �� �����
            {
                    _mainSpecification = _mainSpecification.
                        And(new SpecificationFileExist(nameof(FileSerialize)));
            }
        }
    }
}