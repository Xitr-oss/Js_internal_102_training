using System;

namespace HospitalManagementSystem
{
    // BILLING DELEGATE
    public delegate double BillingStrategy(double amount);

    // EVENT DELEGATE
    public delegate void HospitalNotification(string message);

    // ABSTRACT PATIENT CLASS
    abstract class Patient
    {
        public string Name { get; set; }
        public double BaseAmount { get; set; }

        public abstract double CalculateBill();
    }

    // GENERAL PATIENT
    class GeneralPatient : Patient
    {
        public override double CalculateBill()
        {
            return BaseAmount;
        }
    }

    // ICU PATIENT
    class ICUPatient : Patient
    {
        public override double CalculateBill()
        {
            return BaseAmount + 5000; // ICU charge
        }
    }

    // HOSPITAL CLASS
    class Hospital
    {
        // EVENT
        public event HospitalNotification OnNotify;

        // TRIGGER NOTIFICATION
        public void TriggerNotification(string message)
        {
            OnNotify?.Invoke(message);
        }

        // APPLY BILLING STRATEGY
        public double ApplyBillingStrategy(double amount, BillingStrategy strategy)
        {
            return strategy(amount);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Hospital hospital = new Hospital();

            // EVENT SUBSCRIPTIONS (Departments)
            hospital.OnNotify += msg => Console.WriteLine("[Reception] " + msg);
            hospital.OnNotify += msg => Console.WriteLine("[Accounts] " + msg);
            hospital.OnNotify += msg => Console.WriteLine("[Medical Dept] " + msg);

            Console.Write("Enter Patient Name: ");
            string name = Console.ReadLine();

            Console.WriteLine("\nSelect Patient Type:");
            Console.WriteLine("1. General Patient");
            Console.WriteLine("2. ICU Patient");
            int choice = Convert.ToInt32(Console.ReadLine());

            Patient patient;

            if (choice == 1)
                patient = new GeneralPatient();
            else
                patient = new ICUPatient();

            patient.Name = name;

            // TRIGGER ADMISSION EVENT
            hospital.TriggerNotification($"Patient {patient.Name} admitted.");

            Console.Write("\nEnter Base Treatment Amount: ");
            patient.BaseAmount = Convert.ToDouble(Console.ReadLine());

            // CALCULATE BILL
            double billAmount = patient.CalculateBill();

            // BILLING STRATEGY (GST)
            BillingStrategy gstStrategy = amount => amount + (amount * 0.18);

            // APPLY STRATEGY
            double finalBill = hospital.ApplyBillingStrategy(billAmount, gstStrategy);

            // BILL GENERATED EVENT
            hospital.TriggerNotification($"Billing completed for {patient.Name}.");

            // DISPLAY BILL
            Console.WriteLine("\n----- FINAL BILL -----");
            Console.WriteLine("Patient Name : " + patient.Name);
            Console.WriteLine("Total Amount : "+ finalBill);

            // FINAL NOTIFICATION
            hospital.TriggerNotification("Patient ready for discharge.");

            Console.ReadLine();
        }
    }
}
