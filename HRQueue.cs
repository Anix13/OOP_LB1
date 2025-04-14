using OOP_LB1;
using System;
using System.Collections.Generic;

public delegate void HRQueueEventHandler(string message);

public class HRQueue
{
    private Queue<HRDepartment> departments = new Queue<HRDepartment>();
    public event HRQueueEventHandler ItemAdded;
    public event HRQueueEventHandler ItemUpdated;
    public event HRQueueEventHandler StateChanged;

    public void Enqueue(HRDepartment department)
    {
        departments.Enqueue(department);
        ItemAdded?.Invoke($"Добавлен отдел: {department.CompanyName} (Статус: {department.CurrentStatus})");
    }

    public void UpdateDepartment(HRDepartment department, int employees, double hours,
                               decimal rate, double tax, string address, string contact)
    {
        department.UpdateFields(employees, hours, rate, tax, address, contact);
        ItemUpdated?.Invoke($"Обновлен отдел: {department.CompanyName} (Статус: {department.CurrentStatus})");
    }



    public void ChangeDepartmentState(HRDepartment department, IHRState newState)
    {
        if (department == null || newState == null)
        {
            throw new ArgumentNullException("Department or state cannot be null");
        }

        if (!departments.Contains(department))
        {
            throw new InvalidOperationException("Department not found in queue");
        }

        department.SetState(newState);
        StateChanged?.Invoke($"Состояние отдела {department.CompanyName} изменено на {newState.GetStatus()}");
    }

    public IEnumerable<HRDepartment> GetDepartments() => departments;
}