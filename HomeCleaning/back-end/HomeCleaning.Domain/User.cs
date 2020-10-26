

using System;
using System.Collections.Generic;
using Framework.Domain;


namespace HomeCleaning.Domain
{
    public class User : IEntity<Guid>
    {
        public User(Guid id, string username)
        {
            Username = username;
            Id = id;
        }

        //public User(Guid id,
        //    string username, 
        //    string firstName,
        //    string lastName, 
        //    string nationalCode, 
        //    string cellphone, 
        //    string email)
        //{
        //    Username = username;
        //    FirstName = firstName;
        //    LastName = lastName;
        //    NationalCode = nationalCode;
        //    Cellphone = cellphone;
        //    Email = email;
        //    Id = id;
        //}

        public Guid Id { get; private set; }

        public string Username { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string NationalCode { get; private set; }

        public string Cellphone { get; private set; }

        public string Email { get; private set; }

        public string Name => FirstName + " " + LastName;

        public void FirstNameSetter(string firstName)
        {
            FirstName = firstName;
        }

        public void LastnameSetter(string lastName)
        {
            LastName = lastName;
        }

        public void NationalCodeSetter(string nationalCode)
        {
            NationalCode = nationalCode;
        }

        public void CellphoneSetter(string cellphone)
        {
            Cellphone = cellphone;
        }

        public void EmailSetter(string email)
        {
            Email = email;
        }

    }
}