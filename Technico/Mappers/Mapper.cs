using System.Data;
using System.Net;
using Technico.Dtos;
using Technico.Models;

namespace Technico.Mappers;

public interface IMapper
{
    User MapToUser(UserCreateDTO user);
    UserFullDTO MapToUserFullDTO(User user);
    UserSimpleDTO MapToUserSimpleDTO(User user);
    PropertyDTO MapToPropertyDTO(Property property);
    RepairDTO MapToRepairDTO(Repair repair);
}

public class Mapper : IMapper
{
    public User MapToUser(UserCreateDTO user)
    {
        return new User
        {
            VATNumber = user.VATNumber,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            Address = user.Address,
            Password = user.Password,
            PhoneNumber = user.PhoneNumber
        };
    }
    public UserFullDTO MapToUserFullDTO(User user)
    {
        return new UserFullDTO
        {
            Id = user.Id,
            VATNumber = user.VATNumber,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            Address = user.Address,
            Password = user.Password,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role,
            Properties = user.Properties?.Select(MapToPropertyDTO).ToList()
        };
    }

    public UserSimpleDTO MapToUserSimpleDTO(User user)
    {
        return new UserSimpleDTO
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
        };
    }

    public PropertyDTO MapToPropertyDTO(Property property)
    {
        if (property == null)
            return null;

        return new PropertyDTO
        {
            PropertyId = property.PropertyId,
            Address = property.Address,
            YearOfConstruction = property.YearOfConstruction,
            OwnerID = property.OwnerID,
            Repairs = property.Repairs?.Select(MapToRepairDTO).ToList()
        };
    }

    public RepairDTO MapToRepairDTO(Repair repair)
    {
        if (repair == null)
            return null;

        return new RepairDTO
        {
            Id = repair.Id,
            ScheduledDate = repair.ScheduledDate,
            Type = repair.Type,
            CurrentStatus = repair.CurrentStatus,
            Cost = repair.Cost,
            Description = repair.Description,
            Address = repair.Address,
            PropertyId = repair.PropertyId
        };
    }
}