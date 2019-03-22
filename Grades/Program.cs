using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace Grades
{
    class Program
    {
        static void Main(string[] args)
        {
            //SpeechSynthesizer synth = new SpeechSynthesizer();

            //synth.Speak("Hello! This is the grade book program");

            GradeBook book = new GradeBook();

            book.NameChanged += OnNameChanged;
            GetBookName(book);
            AddGrades(book);
            SaveGrades(book);
            WriteResults(book);
        }

        private static void WriteResults(GradeBook book)
        {
            GradeStatistics stats = book.ComputeStatistics();
            Console.WriteLine(book.Name);
            WriteResult("Average", stats.AverageGrade);
            WriteResult("Highest", stats.HighestGrade);
            WriteResult("Lowest", stats.LowestGrade);
            WriteResult(stats.Description, stats.LetterGrade);
        }

        private static void SaveGrades(GradeBook book)
        {
            using (StreamWriter outputFile = File.CreateText("grades.txt"))
            {
                book.WriteGrades(outputFile);
            }
        }

        private static void AddGrades(GradeBook book)
        {
            book.AddGrade(91);
            book.AddGrade(89.5f);
            book.AddGrade(75);
        }

        private static void GetBookName(GradeBook book)
        {
            try
            {
                Console.WriteLine("Please enter a Name for the Grade Book: ");
                book.Name = Console.ReadLine();
            }
            catch (ArgumentException err)
            {
                Console.WriteLine(err.Message);
            }
            catch (Exception err)
            {
                Console.WriteLine("Oops, Something went wrong.");
                Console.WriteLine(err);
            }
        }

        static void WriteResult(string description, float result)
        {
            Console.WriteLine($"{description}: {result:F2}", description, result);
        }
        static void WriteResult(string description, string result)
        {
            Console.WriteLine($"{description}: {result:F2}", description, result);
        }


        static void OnNameChanged(object sender, NameChangeEventArgs args)
        {
            Console.WriteLine($"Grade book changing name from {args.ExistingName} to {args.NewName}.");
        }
    }
}
