namespace BT.Contacts.Application.Models
{
    public class ValidationMessages
    {
        public static string EitherNameComboMandatory = "Enter either firstname and lastname or businessname";
        public static string FirstAndLastNameMandatory = "Enter firstname and lastname";
        public static string GenericStringMandatory = "Enter {0}";
        public static string GenericIntegerMandatory = "{0} cannot be less than or equal to zero";
    }
}
