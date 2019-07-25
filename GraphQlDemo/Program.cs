using System;
using GraphQL;
using GraphQL.Types;

namespace GraphQlDemo
{
    class Program
    {
        public class Person
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public DateTime DateCreated { get; set; }
        }

        public class Contact
        {
            public long Id { get; set; }
            public string AddressLine1 { get; set; }
            public string PostCode { get; set; }
        }

        public class PersonType : ObjectGraphType<Person>
        {
            public PersonType()
            {
                Field(x => x.Id).Description("The Id of Person");
                Field(x => x.Name).Description("Name of Person");
                Field(x => x.DateCreated).Description("DOB of person");
            }
        }

        public class ContactType : ObjectGraphType<Contact>
        {
            public ContactType()
            {
                Field(x => x.Id).Description("The Id of Person");
                Field(x => x.AddressLine1).Description("Address");
                Field(x => x.PostCode).Description("PostCode");
            }
        }

        public class PersonQuery : ObjectGraphType
        {
            public PersonQuery()
            {
                Field<PersonType>("person",
                    resolve: c => new Person {Id = 1, Name = "Kiran", DateCreated = DateTime.Now.Date});
                Field<ContactType>("contact",
                    resolve: c => new Contact {Id = 1, AddressLine1 = "Wayside Crescent", PostCode = "PE7 8HX"});
            }
        }

        static void Main(string[] args)
        {
            var schema = new Schema {Query = new PersonQuery()};

            var json = schema.Execute(_ =>
                _.Query = "{person {id name dateCreated } contact {id addressLine1 postCode}}");

            Console.WriteLine(json);
            Console.Read();
        }
    }
}
