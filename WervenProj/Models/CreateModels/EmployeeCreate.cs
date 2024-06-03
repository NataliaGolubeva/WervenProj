using System.Runtime.CompilerServices;

namespace WervenProj.Models.CreateModels
{
    public class EmployeeCreate
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int RoleId { get; set; }

        public static bool Validate(EmployeeCreate obj)
        {
            if (obj == null 
                || obj.Name.Length < 3 
                || obj.Name.Length > 50
                || obj.RoleId < 1
                || obj.RoleId > 4) { return false; }
            else return true; }
    }
}
