using System;

namespace ErickExample.Entities
{
    internal class Person
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public DateTime BirthDate { get; private set; }

        public Person(int pId, string pName, DateTime pBirthDate)
        {
            Id = pId;
            Name = pName;
            BirthDate = pBirthDate;
        }
    }
}
