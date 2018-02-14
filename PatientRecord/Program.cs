using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientRecord
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("********************");
            Console.WriteLine("Welcome to our dental clinic!");
            Console.WriteLine("********************");
            while (true)
            {
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Create a new patient record");
                Console.WriteLine("2. Invoice");
                Console.WriteLine("3. Apply payment");
                Console.WriteLine("4. Print all accounts");

                Console.Write("Select an option:");
                var option = Console.ReadLine();
                switch (option)
                {
                    case "0":
                        Console.WriteLine("Thank you for visiting!");
                        return;
                    case "1":
                        Console.WriteLine("First Name:");
                        var firstName = Console.ReadLine();

                        Console.WriteLine("Last Name:");
                        var lastName = Console.ReadLine();


                        Console.WriteLine("Email Address:");
                        var emailAddress = Console.ReadLine();

                        Console.WriteLine("Birth Date:");
                        var birthDate = Console.ReadLine();


                        var conditions = Enum.GetNames(typeof(Condition));
                        for (int i = 0; i < conditions.Length; i++)
                        {
                            Console.WriteLine($"{i + 1}.{conditions[i]}");
                        }
                        Console.Write("Select a condition:");
                        int condition;
                        if (!int.TryParse(Console.ReadLine(), out condition))
                        {
                            Console.WriteLine("Invalid condition! Try again!");
                            break;
                        }
                        if (condition > conditions.Length)
                        {
                            Console.WriteLine("Invalid condition! Try again!");
                            break;
                        }

                        Console.Write("Invoice:");
                        var balance = Convert.ToDecimal(Console.ReadLine());
                        try
                        {
                            var patientRecord = Clinic.CreatePatientRecord(firstName, lastName, emailAddress, birthDate, (Condition)(condition - 1), balance);
                            Console.WriteLine($"Chart Number:{patientRecord.ChartNumber}");
                            Console.WriteLine($"First Name:{patientRecord.FirstName}");
                            Console.WriteLine($"Last Name:{patientRecord.LastName}");
                            Console.WriteLine($"Birth Date:{patientRecord.BirthDate}");
                            Console.WriteLine($"Email Address:{patientRecord.EmailAddress}");
                            Console.WriteLine($"Date Created:{patientRecord.CreatedDate}");
                            Console.WriteLine($"Account Balance:{patientRecord.AccountBalance}");
                        }
                        catch (ArgumentNullException ax)
                        {
                            Console.WriteLine($"Oops!{ax.Message}");
                        }

                        catch (Exception)
                        {
                            Console.WriteLine("Something went wrong! Please try again");
                        }
                        finally
                        {
                            //clean up
                        }

                        break;
                    case "2":
                        PrintAllPatient();
                        Console.Write("Select an account number:");
                        var accountNumber = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Invoice amount:");
                        var invoiceAmount = Convert.ToDecimal(Console.ReadLine());
                        Clinic.Invoice(accountNumber, invoiceAmount);
                        break;
                    case "3":
                        PrintAllPatient();
                        Console.Write("Select an account number:");
                        accountNumber = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Payment amount:");
                        var paymentAmount = Convert.ToDecimal(Console.ReadLine());
                        Clinic.Pay(accountNumber, paymentAmount);
                        break;
                    case "4":
                        PrintAllPatient();
                        break;
                    default:
                        break;
                }
            }

        }

        private static void PrintAllPatient()
        {
            var patientRecords = Clinic.GetPatients();
            foreach (var patientRecord in patientRecords)
            {
                Console.WriteLine($"Chart Number:{patientRecord.ChartNumber}");
                Console.WriteLine($"First Name:{patientRecord.FirstName}");
                Console.WriteLine($"Last Name:{patientRecord.LastName}");
                Console.WriteLine($"Birth Date:{patientRecord.BirthDate}");
                Console.WriteLine($"Email Address:{patientRecord.EmailAddress}");
                Console.WriteLine($"Date Created:{patientRecord.CreatedDate}");
                Console.WriteLine($"Account Balance:{patientRecord.AccountBalance}");
            }
        }
    }
}

