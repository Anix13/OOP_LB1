using OOP_LB1.Models;
using System;

public interface IHRState
{
    void Handle(HRDepartment department);
    string GetStatus();
    bool CanModify { get; }
    bool CanCalculateSalary { get; }
}

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
        if (department.Employees <= 0)
        {
            throw new InvalidOperationException("Необходимо указать количество сотрудников");
        }
    }

    public string GetStatus() => "Пассивный";
    public bool CanModify => false;
    public bool CanCalculateSalary => true;
}

// Добавим фабрику для создания состояний
public static class HRStateFactory
{
    public static IHRState CreateState(string stateType)
    {
        return stateType switch
        {
            "Active" => new ActiveState(),
            "Passive" => new PassiveState(),
            _ => throw new ArgumentException("Неизвестный тип состояния")
        };
    }
}