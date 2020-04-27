using GymRegistrator.Model;
using System;
using System.Collections.Generic;

namespace GymRegistrator.UI.Wrapper
{
    public class GymClientWrapper : ModelWrapper<GymClient>
    {
        public GymClientWrapper(GymClient model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string FirstName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string LastName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Email
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(FirstName):
                    if (string.Equals(FirstName, " "))
                    {
                        yield return "Please enter a valid name";
                    }
                    break;
            }
        }
    }
}
