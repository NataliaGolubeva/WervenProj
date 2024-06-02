using System.Runtime.CompilerServices;

namespace WervenProj.Models.CreateModels
{
    public class EmployeeCreate
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int RoleNr { get; set; }

        public static bool Validate(EmployeeCreate obj)
        {
            if (obj == null 
                || obj.Name.Length < 3 
                || obj.Name.Length > 50
                || obj.RoleNr < 0
                || obj.RoleNr > 3) { return false; }
            else return true; }
    }
}
