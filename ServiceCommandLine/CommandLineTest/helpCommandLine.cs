using System;
using System.Collections.Generic;
using System.Linq;
using Patterns.SpecificationClassic;
//using UserServices.ICommandLineService;
//using UserServices.CommandLineService;
//using UserServices.SpecificationCommandLine;
//using UserServices.UserAttributedLib;


    /// <summary>
    /// ��������� ��������� ������ Help.
    /// </summary>
    public class helpCommandLine : CommandLineRun
    {// Help ��������� ������ 
        /// <summary>
        /// ��� ��������, ������� �� ������� ����� ��������
        /// </summary>
        public string CommandNameHelp { get; set; }

        public helpCommandLine()    { }

        /// <summary>
        /// ��������� 
        /// </summary>
        public override void Run()
        {
            CommandLineSample cmdln = CommandLineService.cmdlines.FirstOrDefault((s => s.CommandName == this.CommandName));
            if ( cmdln != null)
            { // �������
                cmdln.Help();
            }   
            else // ������������ ������� help
            {
                Console.WriteLine("Error calling program {0}.  command {1} does not exist ", AppName, this.CommandNameHelp);
                RulesOfchallenge();
            }
        }

        /// <summary>
        /// ������� ������
        /// </summary>
        public override void RulesOfchallenge()
        { 
                Console.WriteLine("The syntax for calling: {0} /{1}  MandatoryProperties:  -NamePropertie=ValuePropertie", AppName, this.CommandName);
                Console.WriteLine("The syntax for help: {0} /help CommandName", AppName);
	    }
    }