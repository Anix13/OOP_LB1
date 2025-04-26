using System.Collections.Generic;

namespace OOP_LB1.Models
{
    public class HRQueue
    {
        private readonly Queue<HRDepartment> _departments = new Queue<HRDepartment>();

        public void Enqueue(HRDepartment department) => _departments.Enqueue(department);
        public IEnumerable<HRDepartment> GetDepartments() => _departments;

    }
}