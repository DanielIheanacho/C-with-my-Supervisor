using System;
using System.Collections.Generic;
using RentalManager;
using System.Text;

ManagementSystem.Run();

namespace RentalManager   
{
    public static class ManagementSystem
    {


        private static List<Customer> registeredCustomers = new List<Customer>();
        private static List<Equipment> registeredEquipment = new List<Equipment>();
        private static string[][] equipmentOption =
        {
            new string[] { "Electric Drills",          "Standard",     "3000", "1000", "5000", "20000" },
            new string[] { "Welding Machine",         "Heavy",        "7000", "2000", "7500", "25000" },
            new string[] { "Generator",               "Heavy",        "10000", "2500", "10000", "40000" },
            new string[] { "Air Compressors",         "Heavy",        "8000", "2000", "10000", "35000" },
            new string[] { "Measuring Instrument",     "Precision",    "5000", "1500", "7500", "30000" },
            new string[] { "Ladders",                  "Standard",     "2500", "1000", "5000", "20000" },
            new string[] { "Power Tools",              "Specialised",  "5000", "1500", "7500", "25000" }
        };
        public static int PossibleCustomers
        {
            get
            {
                return registeredCustomers.Count;
            }
        }
        private static int possibleEquipment = equipmentOption.GetLength(0);
        public static int PossibleEquipment
        {
            get
            {   
                return possibleEquipment;
            }
        }
        private static int equipmentFinancialData = equipmentOption[0].Length;
        public static int EquipmentFinancialData
        {
            get
            {
                return equipmentFinancialData;
            }
        }


        public static string EquipmentOption(int x, int y)
        {
            return ManagementSystem.equipmentOption[x][y];
        }
        private static int[] equipmentCount = {0,0,0,0,0,0,0};

        public static void Run()
        {
            int menuOption;
            while(true)
            {
                Console.WriteLine("\n===== EQUIPMENT RENTAL SYSTEM =====\n");
                Console.WriteLine("       1. Register Equipment");       
                Console.WriteLine("       2. Register Customer");
                Console.WriteLine("         3. View Equipment");
                Console.WriteLine("         4. View Customers");
                Console.WriteLine("         5. Rent Equipment");
                Console.WriteLine("        6. Return Equipment");
                Console.WriteLine("      7. View Customer Rentals");
                Console.WriteLine("             0. Exit\n");
                Console.Write("Pick a number from the options: ");
                if( int.TryParse(Console.ReadLine(), out menuOption) && menuOption>=0 && menuOption<8 )
                {
                    switch (menuOption)
                    {
                        case 0:
                            return;
                        case 1:
                            Equipment newEquipment = new Equipment();
                            string? name = PickEquipment();
                            if(!string.IsNullOrEmpty(name))
                            {
                                newEquipment.Register(name);
                                registeredEquipment.Add(newEquipment);
                            }
                            break;
                        case 2 : 
                            Customer newCustomer = new Customer();
                            newCustomer.Register();
                            registeredCustomers.Add(newCustomer);
                            break;
                        case 3:
                            foreach(Equipment equipment in registeredEquipment)
                                {
                                    Console.WriteLine(equipment.UniqueID);
                                }
                            break;
                        case 4:
                            foreach(Customer customer in registeredCustomers)
                                {
                                    Console.WriteLine(customer.CustomerID);
                                    Console.WriteLine(customer.CustomerName);
                                    Console.WriteLine(customer.CustomerPhoneNumber);
                                    Console.WriteLine(customer.CustomerAddress); 
                                }
                            break;
                        case 5:
                            SelectCustomerAndEquipment();
                            break;
                        case 7:
                        
                            break;
                   }
               }
                else
                {
                Console.WriteLine("\nInvalid Option\n ");
                }
            }
        }

        private static void PrintEquipment()
        {
            for(int i = 0; i < PossibleEquipment; i++)
            {
                Console.WriteLine("{0}. {1}", i+1, EquipmentOption(i,0));
            }
        }

        private static int optionIndex;

        public static int OptionIndex
        {
            get
            {
                return optionIndex;
            }
        }

        private static string? PickEquipment()
        {            
            Console.WriteLine("What kind of equipment is it?");
            PrintEquipment();
            Console.Write("Pick an option from above: ");
            while(true)
            {
                if (!int.TryParse(Console.ReadLine(), out optionIndex))
                {
                    Console.Write("Please enter a number from the option: ");
                    continue;
                }
                
                if(optionIndex < 0 || optionIndex > PossibleEquipment)
                {
                    Console.Write("Invalid option");
                    continue;
                }
                
                if(optionIndex == 0)
                {
                    return null;
                }
                EquipmentCount = ++EquipmentCount ;

                return EquipmentOption(optionIndex - 1,0); 
            }
        }
        public static int EquipmentCount
        {
            get
            {
                return equipmentCount[OptionIndex - 1];
            }

            set
            {
                equipmentCount[OptionIndex - 1] = value;
            }
        } 

        private static void SelectCustomerAndEquipment()
        {
            int customerSelectionOption;
            Customer currentCustomer;
            List<Equipment> equipmentsToRent = new List<Equipment>();
            string equipmentName;
            int equipmentIndex;
            Console.WriteLine("Select the customer making the rent");
            if(!(registeredCustomers.Count == 0))
            {
                for(int i = 0; i < registeredCustomers.Count;i++)
                {
                    Console.WriteLine( "{0}. {1}",i + 1, registeredCustomers[i].CustomerID);
                }

                customerSelectionOption = SelectOption(PossibleCustomers);

                if( customerSelectionOption == 0)
                {
                    return;
                }
                else
                {
                currentCustomer = registeredCustomers[customerSelectionOption - 1];
                }

                while(true)
                {    
                    Console.WriteLine("Select an equipment to be rented");

                    for(int j = 0; j < PossibleEquipment; j++)
                    {
                        Console.WriteLine("{0}. {1}", j + 1, EquipmentOption(j,0) );  
                    }
                    Console.WriteLine("0. Exit\n");

                    equipmentIndex = SelectOption(PossibleEquipment);
                    if(!(equipmentIndex == 0))
                    {
                        equipmentName = EquipmentOption(equipmentIndex - 1, 0);
                    }
                    else
                    {
                         return;
                    }   

                    if(!(registeredEquipment.Count == 0))
                    {
                        for(int k = 0; k < registeredEquipment.Count; k++)
                        {
                            if ((registeredEquipment[k].Name == equipmentName) && (registeredEquipment[k].status == Equipment.EquipmentStatus.Available) )
                            {
                               equipmentsToRent.Add(registeredEquipment[k]);
                               break; 
                            }        
                        }
                    }     

                    Console.WriteLine("Do you want to add another equipment?");
                    if(!(YesOrNo() == 1))
                    {
                        currentCustomer.CustomersRentedEquipment = equipmentsToRent;
                        break;  
                    }     
                }    
            } 
            else
            {
                Console.WriteLine("There is no Registered Customer");
            }      
        }

        private static int SelectOption(int limit)
        {
            Console.Write("Pick an option from above: ");
            while(true)
            {
                if (!int.TryParse(Console.ReadLine(), out optionIndex))
                {
                    Console.Write("Please enter a number from the option: ");
                    continue;
                }
                
                if(optionIndex < 0 || optionIndex > limit)
                {
                    Console.Write("Invalid option");
                    continue;
                }
                
                if(optionIndex == 0)
                {
                    return default(int);
                }
                return optionIndex; 
            } 
        }

        private static int YesOrNo()
        {
            int result ;
            string? input;
            choose:
            Console.WriteLine("\nY/ Yes or N/ No");
            input = Console.ReadLine();
            if(input == "Y" || input == "y" )
            {
                result = 1;
            }
            else if (input == "N" || input == "n")
            {
                result = 0;
            }
            else
            {
                Console.WriteLine("\nInvalid Option");
                goto choose;
            }
            return result;
        }

    }
           
    public class Customer
    {
        private static int customerCount = 0;
        private string customerID = string.Empty;
        private string name = string.Empty;
        private string phoneNumber = string.Empty;
        private string address = string.Empty;
        private List<Equipment> rentedEquipment = new List<Equipment>();
        private int dailyCount = 0 ;

        public List<Equipment> CustomersRentedEquipment
        {
            get
            {
                return this.rentedEquipment;
            }
            set
            {
                this.rentedEquipment = value;
            }
        }

        public int DailyCount
        {
            get
            {
                return this.dailyCount;
            }
            set
            {
                this.dailyCount = value;
            }
        }

        public Customer()
        {

        }

        public void Register()
        {   
            Console.Write("Insert Customer's Name: ");
            this.name = Console.ReadLine();
            this.customerID = $"Customer{++customerCount:D4}";
            Console.Write("Insert Customer's Phone Number: ");
            this.phoneNumber = Console.ReadLine();
            Console.Write("Insert Customer's Address:");
            this.address = Console.ReadLine();
        }

        public static int CustomerCount
        {
            get
            {
                return customerCount;
            }
        }
        public string CustomerName
        {
            get
            {
                return this.name;
            }
        }
        public string CustomerID
        {
            get
            {
                return customerID;
            }
        }
        public string CustomerPhoneNumber
        {
            get
            {
                return this.phoneNumber;
            }   
        }
        public string CustomerAddress
        {
            get
            {
                return this.address;
            }
        }
    }

    public class Equipment
    {
        public enum EquipmentCategory
        {
            Standard,
            Heavy,
            Precision,
            Specialised
        }
        
        public enum ConditionStatus
        {
            Rented,
            InMaintenance,
            Ready
        }

        public enum EquipmentStatus
        {
            Available,
            Unavailable
        } 

        private int categoryIndex = 1;
        private string uniqueID = string.Empty;
        private string name = string.Empty;
        private EquipmentCategory category = EquipmentCategory.Standard;
        private ConditionStatus currentCondition = ConditionStatus.Ready;

        public EquipmentStatus status = EquipmentStatus.Available;

        public Equipment()
        {

        }

        public void Register(string name) 
        {
        
            this.uniqueID = CreateUniqueID(name);
            this.name = name;
            this.currentCondition = ConditionStatus.Ready;
            this.status = EquipmentStatus.Available;

            for(int i = 0; i < ManagementSystem.PossibleEquipment; i++ )
            {
                if(this.name == ManagementSystem.EquipmentOption(i,0))
                {
                    Enum.TryParse(ManagementSystem.EquipmentOption(i,categoryIndex), out this.category);
                    return;
                }
                continue;
            }     
        }

        public string Name
        {
            get
            {
            return this.name;
            }
        }

        public ConditionStatus CurrentCondition
        {
            get
            {
               return this.currentCondition;
            }
            set
            {
                this.currentCondition = value;

                if(currentCondition == ConditionStatus.Rented || currentCondition == ConditionStatus.InMaintenance)
                {
                this.status = EquipmentStatus.Unavailable;
                }
                else
                {
                    this.status = EquipmentStatus.Available;

                }
            }
        }

        public string UniqueID
        {
            get
            {
                return uniqueID;
            }
        }

        private string CreateUniqueID(String equipmentName )
        {
            StringBuilder newID = new StringBuilder();
            string[] equipmentNameElement = equipmentName.Split(" ");
            foreach(string name in equipmentNameElement)
            {   
                newID.Append(name[0]);
  
            }

            string iD = $"{newID.ToString()}{ManagementSystem.EquipmentCount:D3}";

            return iD;
        }
    
    
    }

    public static class RentEquipment
    {
            
    }
    
    public class CostProcessing
    {

    }
}
