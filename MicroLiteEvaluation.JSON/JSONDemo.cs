using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;

using MicroLite;
using MicroLite.Configuration;
using MicroLite.Builder;

using MicroLite.TypeConverters;
using MicroLite.Listeners;

using JSON;

namespace MicroLiteEvaluationJSON
{
    class JSONDemo
    {
        public static ISessionFactory sessionFactory { get; private set; }

        static void Main(string[] args)
        {
            Configure
                .Extensions()
                .WithAttributeBasedMapping();

            TypeConverter.Converters.Add(new JSONTypeConverter<CustomerData>());

            Listener.Listeners.Add(new CustomerListener());

            sessionFactory = Configure
                .Fluently()
                .ForMsSqlConnection("MicroLiteDB")
                .CreateSessionFactory();

            try
            {
                Console.WriteLine("OpenSession");
                using (var session = sessionFactory.OpenSession())
                {

                    try
                    {
                        var customerToInsert = new Customer
                        {
                            Data = new CustomerData
                            {
                                GivenName = "John",
                                FamilyName = "Doe",
                                DateOfBirth = new DateTime(1987, 1, 9),
                                Address = new Address
                                {
                                    Line1 = "1 Oxford Street",
                                    Line2 = "",
                                    TownCity = "London",
                                    PostCode = "WC1 3OX"
                                }
                            }
                        };

                        session.Insert(customerToInsert);

                        Console.WriteLine(customerToInsert.Id);

                        //=======================================================

                        var customerToChange = session.Single<Customer>(customerToInsert.Id);

                        //customerToChange.GivenName += " Henry"; // will not work
                        customerToChange.Data.GivenName += " Henry";

                        Console.WriteLine("{0}", customerToChange.Data.GivenName);

                        session.Update(customerToChange);

                        //=======================================================

                        // We can query rapidly on any indexable field
                        // but we can also query on fields embedded in the JSON payload using a LIKE clause

                        var query = SqlBuilder.Select("CustomerID", "GivenName", "JSONData", "FamilyName")
                            .From(typeof(Customer))
                            .Where("CustomerID = @p0", 1)
                            .OrWhere("GivenName = @p1", "John Henry") // on indexable field
                            .OrWhere("JSONData LIKE @p2", "%\"DateOfBirth\":\"1987-01-09T00:00:00\"%") // in JSON payload
                            .OrderByDescending("FamilyName")
                            .ToSqlQuery();

                        var customers = session.Fetch<Customer>(query);
                        Console.WriteLine("SqlBuilder.SelectFrom Customers...");
                        foreach (var customer in customers)
                        {
                            Console.WriteLine("{0}\t{1}\t{2}", customer.Id, customer.GivenName, customer.FamilyName);
                        }
                        customers = null;

                        //=======================================================

                        var queryRaw = SqlBuilder.Select("JSONData")
                            .From(typeof(CustomerRaw))
                            .Where("CustomerID = @p0", 1)
                            .OrWhere("JSONData LIKE @p3", "%\"DateOfBirth\":\"1987-01-09T00:00:00\"%") // in JSON payload
                            .ToSqlQuery();

                        var customersRaw = session.Fetch<CustomerRaw>(queryRaw);
                        Console.WriteLine("SqlBuilder.SelectFrom CustomersRaw...");
                        foreach (var customerRaw in customersRaw)
                        {
                            Console.WriteLine("Raw Data: {0}", customerRaw.Data);
                        }
                        customersRaw = null;

                        //=======================================================

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            sessionFactory = null;

            Console.ReadKey();
        }
    }

/*
    class JSONDemo
    {
        public static ISessionFactory sessionFactory { get; private set; }

        static void Main(string[] args)
        {
            Configure
                .Extensions()
                .WithAttributeBasedMapping();

            TypeConverter.Converters.Add(new JSONTypeConverter<CustomerData>());

            Listener.Listeners.Add(new CustomerListener());

            sessionFactory = Configure
                .Fluently()
                .ForMsSqlConnection("MicroLiteDB")
                .CreateSessionFactory();

            try
            {
                Console.WriteLine("OpenSession");
                using (var session = sessionFactory.OpenSession())
                {

                    Console.WriteLine("BeginTransaction");
                    using (var transaction = session.BeginTransaction())
                    {
                        try
                        {
                            var customerToInsert = new Customer
                            {
                                Data = new CustomerData
                                {
                                    GivenName = "David",
                                    FamilyName = "Steele",
                                    DateOfBirth = new DateTime(1951,1,9),
                                    Address = new Address
                                    {
                                        Line1 = "11 Lynton Road",
                                        Line2 = "",
                                        TownCity = "Peterborough",
                                        PostCode = "PE1 3DU"
                                    }
                                },
                                GivenName = "Fred",
                                FamilyName = "Bloggs",
                                PostCode = "PE99 9DU" // projections ignored unless Listener is disabled
                            };

                            session.Insert(customerToInsert);

                            Console.WriteLine(customerToInsert.Id);

                            //=======================================================

                            var customerToChange = session.Single<Customer>(customerToInsert.Id);

                            Console.WriteLine("Before: {0}", customerToChange.Data.GivenName = "David Charles");
                            customerToChange.Data.GivenName += " changed";
                            Console.WriteLine(" After: {0}", customerToChange.Data.GivenName);

                            session.Update(customerToChange);

                            //=======================================================

                            var customerToDelete = new Customer
                            {
                                Id = customerToInsert.Id
                            };

                            //var deleted1 = session.Delete(customerToDelete);
                            //if (deleted1)
                            //    Console.WriteLine("Correctly deleted #1");
                            //else
                            //    Console.WriteLine("Should have deleted #1");

                            //var deleted2 = session.Delete(customerToDelete);
                            //if (deleted2)
                            //    Console.WriteLine("Should not have deleted #2");
                            //else
                            //    Console.WriteLine("Correctly failed to delete #2");

                            //=======================================================

                            Console.WriteLine("SqlBuilder.SelectFrom Customers (data only)...");
                            
//                            var query1 = SqlBuilder.Select("CustomerID", "JSONData")
                            var query1 = SqlBuilder.Select("JSONData")
                                .From(typeof(CustomerRaw))
                                .Where("CustomerID = @p0", 11)
                                .OrWhere("CustomerID = @p1", 13)
                                .OrWhere("JSONData LIKE @p3", "%\"DateOfBirth\":\"1950-12-08T00:00:00\"%") // in JSON data
                                .ToSqlQuery();

                            var customers1 = session.Fetch<CustomerRaw>(query1);
                            Console.WriteLine("SqlBuilder.SelectFrom Customers...");
                            foreach (var customer1 in customers1)
                            {
//                                Console.WriteLine("Raw Data: {0}\t{1}", customer1.Id, customer1.Data);
                                Console.WriteLine("Raw Data: {0}", customer1.Data);
                            }
                            customers1 = null;

                            //=======================================================

                            Console.WriteLine("SqlBuilder.SelectFrom Customers (data and projections)...");

                            var query2 = SqlBuilder.Select("CustomerID", "GivenName", "FamilyName", "JSONData", "PostCode")
                                .From(typeof(Customer))
                                .Where("CustomerID = @p0", 11)
                                .OrWhere("CustomerID = @p1", 13)
                                .OrWhere("GivenName = @p2", "Pauline") // on projected field
                                .OrWhere("JSONData LIKE @p3", "%\"DateOfBirth\":\"1950-12-08T00:00:00\"%") // in JSON data
                                .OrderByDescending("FamilyName")
                                .ToSqlQuery();

                            var customers2 = session.Fetch<Customer>(query2);
                            Console.WriteLine("SqlBuilder.SelectFrom Customers...");
                            foreach (var customer2 in customers2)
                            {
                                Console.WriteLine("  Data: {0}\t{1}\t{2}", customer2.Id, customer2.Data.GivenName, customer2.Data.FamilyName);
                                Console.WriteLine("Direct: {0}\t{1}\t{2}", customer2.Id, customer2.GivenName, customer2.FamilyName);
                            }
                            customers2 = null;

                            //=======================================================

                            transaction.Commit();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    } // using transaction
                } // using session
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            // sessionFactory.dispose; NO NEED - LET IT GO OUT OF SCOPE
            sessionFactory = null;

            Console.ReadKey();
        }
    }
*/
}
