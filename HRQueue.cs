using OOP_LB1;
using System;
using System.Collections.Generic;

public delegate void HRQueueEventHandler(string message);


public class HRQueue
{
    private Queue<HRDepartment> departments = new Queue<HRDepartment>();

    public event HRQueueEventHandler ItemAdded;
    public event HRQueueEventHandler ItemUpdated;

    public void Enqueue(HRDepartment department)
    {
        departments.Enqueue(department);
        ItemAdded?.Invoke($"Добавлен отдел: {department.CompanyName}");
    }

    public void UpdateDepartment(HRDepartment department, int employees, double hours, decimal rate, double tax, string address, string contact)
    {
        department.UpdateFields(employees, hours, rate, tax, address, contact);
        ItemUpdated?.Invoke($"Обновлен отдел: {department.CompanyName}");
    }

    public IEnumerable<HRDepartment> GetDepartments() => departments;


}


