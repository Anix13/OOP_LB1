using OOP_LB1;
using System;

public interface IHRState
{
    void Handle(HRDepartment department);
    string GetStatus();
    bool CanModify { get; }
    bool CanCalculateSalary { get; }

}

// Конкретные состояния
public class ActiveState : IHRState
{
    public void Handle(HRDepartment department)
    {
        // В активном состоянии можно выполнять все операции
    }

    public string GetStatus() => "Активный";
    public bool CanModify => true;
    public bool CanCalculateSalary => true;
}



public class PassiveState : IHRState
{
    public void Handle(HRDepartment department)
    {
        // В пассивном состоянии можно только просматривать данные и рассчитывать зарплату
        if (department.Employees <= 0)
        {
            throw new InvalidOperationException("Необходимо указать количество сотрудников");
        }
    }

    public string GetStatus() => "Пассивный";
    public bool CanModify => false;
    public bool CanCalculateSalary => true;
}