namespace oop_3_assigment
{
    internal class Program
    {

        #region structs
        struct DeliveryAddress
        {
            private string city;
            private string street;
            private int buildingNumber;

            public string City
            {
                get { return city; }
                set { city = value; }
            }
            public string Street
            {
                get { return street; }
                set { street = value; }
            }
            public int BuildingNumber
            {
                get { return buildingNumber; }
                set { buildingNumber = value; }
            }
            public DeliveryAddress(string city, string street, int buildingNumber)
            {
                this.city = city;
                this.street = street;
                this.buildingNumber = buildingNumber;
            }
            public string GetFullAddress()
            {
                return $"{street}, {buildingNumber}, {city}";
            }

        }
        #endregion
        #region classes

        #region Driver class

        class Driver
        {
            public string Name { get; set; }

            public Driver(string name)
            {
                Name = name;
            }
        }

        #endregion
        #region shipment class



        class Shipment
        {
            private string trackingCode;
            private string description;
            private double weight;
            private decimal deliveryFee;
            private DeliveryAddress destination;


            public DeliveryAddress Destination
            {
                get { return destination; }
                set { destination = value; }
            }

            public string TrackingCode
            {
                get { return trackingCode; }
                private set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                        trackingCode = value;
                }
            }

            public string Description
            {
                get { return description; }
                set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                        description = value;
                }
            }

            public double Weight
            {
                get { return weight; }
                set
                {
                    if (value > 0)
                        weight = value;
                }
            }

            public decimal DeliveryFee
            {
                get { return deliveryFee; }
                private set
                {
                    if (value > 0)
                        deliveryFee = value;
                }
            }

            public virtual decimal EstimatedCost
            {
                get { return DeliveryFee + ((decimal)Weight * 5); }
            }

            public Shipment(string trackingCode)
            {
                this.trackingCode = null;
                this.description = null;
                this.weight = 0;
                this.deliveryFee = 0;
                this.destination = default;

                TrackingCode = trackingCode;
                Description = "Unknown";
                Weight = 1;
                DeliveryFee = 50;
            }

            public Shipment(
                string trackingCode,
                string description,
                double weight,
                decimal deliveryFee,
                DeliveryAddress destination)
            {
                this.trackingCode = null;
                this.description = null;
                this.weight = 0;
                this.deliveryFee = 0;
                this.destination = default;

                TrackingCode = trackingCode;
                Description = description;
                Weight = weight;
                DeliveryFee = deliveryFee;
                Destination = destination;
            }

            public void UpdateDeliveryFee(decimal newFee)
            {
                if (newFee > 0)
                    DeliveryFee = newFee;
            }

            public virtual void PrintShipment()
            {
                Console.WriteLine("Tracking Code: " + TrackingCode);
                Console.WriteLine("Description: " + Description);
                Console.WriteLine("Weight: " + Weight);
                Console.WriteLine("Delivery Fee: " + DeliveryFee);
                Console.WriteLine("Destination: " + Destination.GetFullAddress());
                Console.WriteLine("Estimated Cost: " + EstimatedCost);
            }
            public void UpdateWeight(double newWeight)
            {
                Weight = newWeight;
            }
            public void UpdateWeight(double newWeight, double extraPackingWeight)
            {
                Weight = newWeight + extraPackingWeight;
            }

        }
        #endregion

        #region StandardShipment class
        class StandardShipment : Shipment
        {
            public StandardShipment(string trackingCode, string description, double weight,
                decimal deliveryFee, DeliveryAddress destination) : base(trackingCode, description, weight, deliveryFee, destination)
            {
            }
            public override void PrintShipment()
            {
                Console.WriteLine("Standard Shipment");
                Console.WriteLine();
                Console.WriteLine("Tracking Code : " + TrackingCode);
                Console.WriteLine("Description   : " + Description);
                Console.WriteLine("Weight        : " + Weight + " KG");
                Console.WriteLine("Delivery Fee  : " + DeliveryFee + " EGP");
                Console.WriteLine("Estimated Cost: " + EstimatedCost + " EGP");
            }

        }

        #endregion

        #region ExpressShipment
        class ExpressShipment : Shipment
        {
            private decimal extraFee;

            public decimal ExtraFee
            {
                get { return extraFee; }
                set
                {
                    if (value >= 0)
                        extraFee = value;
                }
            }

            public override decimal EstimatedCost
            {
                get
                {
                    return DeliveryFee + ((decimal)Weight * 5) + ExtraFee;
                }
            }

            public ExpressShipment(
                string trackingCode,
                string description,
                double weight,
                decimal deliveryFee,
                DeliveryAddress destination,
                decimal extraFee)
                : base(trackingCode, description, weight, deliveryFee, destination)
            {
                ExtraFee = extraFee;
            }

            public override void PrintShipment()
            {
                Console.WriteLine("Express Shipment");
                Console.WriteLine();
                Console.WriteLine("Tracking Code : " + TrackingCode);
                Console.WriteLine("Description   : " + Description);
                Console.WriteLine("Weight        : " + Weight + " KG");
                Console.WriteLine("Delivery Fee  : " + DeliveryFee + " EGP");
                Console.WriteLine("Extra Fee     : " + ExtraFee + " EGP");
                Console.WriteLine("Estimated Cost: " + EstimatedCost + " EGP");
            }
        }
        #endregion

        #region InternationalShipment

        class InternationalShipment : Shipment
        {
            private string destinationCountry;
            private decimal customsFee;

            public string DestinationCountry
            {
                get { return destinationCountry; }
                set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                        destinationCountry = value;
                }
            }

            public decimal CustomsFee
            {
                get { return customsFee; }
                set
                {
                    if (value >= 0)
                        customsFee = value;
                }
            }

            public override decimal EstimatedCost
            {
                get
                {
                    return DeliveryFee + ((decimal)Weight * 5) + CustomsFee;
                }
            }

            public InternationalShipment(
                string trackingCode,
                string description,
                double weight,
                decimal deliveryFee,
                DeliveryAddress destination,
                string destinationCountry,
                decimal customsFee)
                : base(trackingCode, description, weight, deliveryFee, destination)
            {
                DestinationCountry = destinationCountry;
                CustomsFee = customsFee;
            }

            public override void PrintShipment()
            {
                Console.WriteLine("International Shipment");
                Console.WriteLine();
                Console.WriteLine("Tracking Code        : " + TrackingCode);
                Console.WriteLine("Description          : " + Description);
                Console.WriteLine("Weight               : " + Weight + " KG");
                Console.WriteLine("Delivery Fee         : " + DeliveryFee + " EGP");
                Console.WriteLine("Destination Country  : " + DestinationCountry);
                Console.WriteLine("Customs Fee          : " + CustomsFee + " EGP");
                Console.WriteLine("Estimated Cost       : " + EstimatedCost + " EGP");

            }
        }
        #endregion

        #region DeliveryCenter
        static class DeliveryHelper
        {
            public static void PrintShipmentDetails(Shipment shipment)
            {
                shipment.PrintShipment();
            }
        }

        class DeliveryCenter
        {
            private Shipment[] shipments;

            public string CenterName { get; set; }
            public Driver Driver { get; set; }

            public Shipment[] Shipments
            {
                get { return shipments; }
            }

            public DeliveryCenter(string centerName)
            {
                CenterName = centerName;
                shipments = new Shipment[20];
            }

            public void AddShipment(Shipment shipment)
            {
                for (int i = 0; i < shipments.Length; i++)
                {
                    if (shipments[i] == null)
                    {
                        shipments[i] = shipment;
                        return;
                    }
                }
            }
            public Shipment this[string trackingCode]
            {
                get
                {
                    for (int i = 0; i < shipments.Length; i++)
                    {
                        if (shipments[i] != null &&
                            shipments[i].TrackingCode == trackingCode)
                        {
                            return shipments[i];
                        }
                    }

                    return null;
                }
            }

            public bool RemoveShipment(string trackingCode)
            {
                for (int i = 0; i < shipments.Length; i++)
                {
                    if (shipments[i] != null &&
                        shipments[i].TrackingCode == trackingCode)
                    {
                        shipments[i] = null;
                        return true;
                    }
                }

                return false;
            }

            public void PrintAllShipments()
            {
                for (int i = 0; i < shipments.Length; i++)
                {
                    if (shipments[i] != null)
                    {
                        shipments[i].PrintShipment();
                        Console.WriteLine("===============");
                    }
                }
            }



        }
        #endregion

        #endregion

        static void Main(string[] args)
        {
            #region Theoretical Questions

            #region Q1
            //a)  What is the difference between Method Overloading and Method Overriding?
            /*
             method overloading: more than one  method with the same name but different parameters 
            method overriding: method in base class and another class inherite it
            and make different implementation for the same method
             */

            //b)  What is the difference between Static Binding and Dynamic Binding?
            /*
             static binding: method calling in the compile time
            dynamic binding: method calling in the run time

             */
            #endregion




            #endregion

            #region practical Qs
            Driver driver = new Driver("Ahmed Mohamed");

            DeliveryCenter center = new DeliveryCenter("giza Center");

            center.Driver = driver;
            StandardShipment standard = new StandardShipment(
            "SH001", "Laptop", 3, 80,
            new DeliveryAddress("Cairo", "Tahrir St", 10));

            ExpressShipment express = new ExpressShipment(
                "SH002", "Mobile Phone", 2, 60,
                new DeliveryAddress("Giza", "Pyramids St", 25), 30);
            
            InternationalShipment international = new InternationalShipment(
                "SH003", "Television", 8, 120,
                new DeliveryAddress("Berlin", "Main St", 5),
                "Germany", 100);


            center.AddShipment(standard);
            center.AddShipment(express);
            center.AddShipment(international);

            Console.WriteLine("==========================================");
            Console.WriteLine("Delivery Center");
            Console.WriteLine("==========================================");
            Console.WriteLine();
            Console.WriteLine("Driver : " + center.Driver.Name);
            Console.WriteLine();
            Console.WriteLine("------------------------------------------");
            center.PrintAllShipments();
            Console.WriteLine("==========================================");


            Console.WriteLine();
            Console.WriteLine("Printing Using DeliveryHelper...");
            Console.WriteLine();
            DeliveryHelper.PrintShipmentDetails(standard);
            Console.WriteLine("Standard Shipment Printed Successfully.");
            Console.WriteLine();
            DeliveryHelper.PrintShipmentDetails(express);
            Console.WriteLine("Express Shipment Printed Successfully.");
            Console.WriteLine();
            DeliveryHelper.PrintShipmentDetails(international);
            Console.WriteLine("International Shipment Printed Successfully.");
            Console.WriteLine("==========================================");

            Console.WriteLine();
            Console.WriteLine("Updating Weight...");
            Console.WriteLine();
            Console.WriteLine("Original Weight : " + standard.Weight + " KG");
            standard.UpdateWeight(5);
            Console.WriteLine("Updated Weight : " + standard.Weight + " KG");
            standard.UpdateWeight(5, 0.5);
            Console.WriteLine("Updated Weight After Packing : " + standard.Weight + " KG");
            Console.WriteLine("==========================================");



            Console.WriteLine();
            Console.WriteLine("Printing Using Shipment[]...");
            Console.WriteLine();
            Shipment[] allShipments = new Shipment[]
                    {
            standard,
            express,
            international
                    };

            foreach (Shipment s in allShipments)
            {
                s.PrintShipment();
                Console.WriteLine("------------------------------------------");
            }
            Console.WriteLine("==========================================");







            #endregion
        }
    }
}
